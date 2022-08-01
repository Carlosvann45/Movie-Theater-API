using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Movie.Theater.Enterprises.Models.DTOs;
using Movie.Theater.Enterprises.Models.Entities;
using Movie.Theater.Enterprises.Providers.Interfaces;
using Movie.Theater.Enterprises.Repos.Interfaces;
using Movie.Theater.Enterprises.Utilities.ExceptionHandler;
using Movie.Theater.Enterprises.Utilities.Jwt;
using System.Security.Cryptography;

namespace Movie.Theater.Enterprises.Providers.Providers
{
    /// <summary>
    /// The implementation of service logic for users to communicate with the user repository
    /// </summary>
    public class CustomerProvider : ICustomerProvider
    {
        private readonly ILogger<CustomerProvider> logger;
        private readonly ICustomerRepository customerRepo;
        private readonly IConfiguration config;

        public CustomerProvider(ILogger<CustomerProvider> logger, ICustomerRepository customerRepo, IConfiguration config)
        {
            this.logger = logger;
            this.customerRepo = customerRepo;
            this.config = config;
        }

        /// <summary>
        /// verifys a customers account for login based from a username and password
        /// </summary>
        /// <param name="email">username for login </param>
        /// <param name="password">password for login </param>
        /// <returns>JWT token</returns>
        /// <exception cref="ServiceUnavailableException">throws an exception when the database has a problem</exception>
        /// <exception cref="NotFoundException">throws exception if the email doesn't exist in database</exception>
        /// <exception cref="UnauthorizedException">throws exception if the sign in credentials fail</exception>
        public async Task<JwtResponseDTO> LogInCustomerAsync(string email, string password)
        {
            bool passwordVerified;
            Customer? existingCustomer;

            try
            {
                existingCustomer = await customerRepo.GetCustomerByEmailAsync(email);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                throw new ServiceUnavailableException(Constants.SERVER_UNAVAILABLE_MESS);
            }

            if (existingCustomer == null) throw new NotFoundException(Constants.CUSTOMER_EMAIL_NOTFOUND);

            passwordVerified = VerifyPasswordHash(password, existingCustomer);

            if (!passwordVerified) throw new UnauthorizedException(Constants.CUSTOMER_UNAUTHORIZED);

            string secret = config.GetSection("AppSettings:Token").Value;

            JwtResponseDTO jwtResponseDTO = new JwtResponseDTO();

            jwtResponseDTO.AccessToken = existingCustomer.CreateAccessToken(secret);
            jwtResponseDTO.RefresherToken = existingCustomer.CreatRefresherToken(secret);

            return jwtResponseDTO;
        }

        /// <summary>
        /// creates customer in repository  if email doesnt exist
        /// </summary>
        /// <param name="customerToCreate">customer to create</param>
        /// <returns>created customer</returns>
        /// <exception cref="ServiceUnavailableException">throws an exception when the database has a problem</exception>
        /// <exception cref="ConflictException">throws exception if customer email exist in database</exception>
        public async Task<Customer> RegisterCustomerAsync(CustomerDTO customerToCreate)
        {
            bool existingEmail;

            try
            {
                existingEmail = await customerRepo.CustomerEmailExistAsync(customerToCreate.Email);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                throw new ServiceUnavailableException(Constants.SERVER_UNAVAILABLE_MESS);
            }

            if (existingEmail) throw new ConflictException(Constants.CUSTOMER_EMAIL_CONFLICT);

            CreatePasswordHash(customerToCreate.Password, out byte[] passwordHash, out byte[] passwordSalt);

            try
            {
                return await customerRepo.RegisterCustomerAsync(customerToCreate.TransferToCustomerEntity(passwordHash, passwordSalt));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                throw new ServiceUnavailableException(Constants.SERVER_UNAVAILABLE_MESS);
            }

        }

        /// <summary>
        /// Creates a Password hash and salt key bassed from a new customers password
        /// </summary>
        /// <param name="password">password to hash</param>
        /// <param name="passwordHash">out variable for the generated passwordHash key</param>
        /// <param name="passwordSalt">out variable for the fenerated passwordSalt key</param>
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// Verifys that password of a customer by comparing the password provided with 
        /// the existing users passwordSalt and passwordHash keys 
        /// </summary>
        /// <param name="password">password to check</param>
        /// <param name="customerToVerify">customer to verify passwordHash key</param>
        /// <returns>boolean values based from two passwordHash keys</returns>
        private bool VerifyPasswordHash(string password, Customer customerToVerify)
        {
            using (var hmac = new HMACSHA512(customerToVerify.PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(customerToVerify.PasswordHash);
            }
        }
    }
}
