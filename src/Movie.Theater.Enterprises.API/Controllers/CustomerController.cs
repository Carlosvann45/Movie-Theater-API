using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movie.Theater.Enterprises.API.Jwt;
using Movie.Theater.Enterprises.Models.DTOs;
using Movie.Theater.Enterprises.Models.Misc;
using Movie.Theater.Enterprises.Providers.Interfaces;

namespace Movie.Theater.Enterprises.API.Controllers
{
    /// <summary>
    /// Customer controller to proccess request to the user database
    /// </summary>
    [ApiController]
    [Route(Constants.CUSTOMER_ENDPOINT)]
    [Produces(Constants.APP_PRODUCES)]
    [Consumes(Constants.APP_CONSUMES)]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerProvider customerProvider;
        private readonly ILogger<CustomerController> logger;
        private readonly IMapper mapper;

        public CustomerController(ICustomerProvider customerProvider, ILogger<CustomerController> logger, IMapper mapper)
        {
            this.customerProvider = customerProvider;
            this.logger = logger;
            this.mapper = mapper;
        }

        /// <summary>
        /// If token is valid gets a customer by email
        /// </summary>
        /// <param name="bearerToken">token to get emai</param>
        /// <param name="email">email for customer</param>
        /// <returns>a customer based from email</returns>
        [JwtAuthorizationFilter(roles: Role.Customer)]
        [HttpGet(Constants.EMAIL)]
        public async Task<ActionResult<CustomerDTO>> GetCustomerByEmail([FromHeader(Name = "Authorization")] string bearerToken, string email)
        {
            logger.LogInformation(Constants.LOG_GET_CUSTOMER_EMAIL);

            var customer = await customerProvider.GetCustomerWithTokenAsync(bearerToken, email);

            var customerDTO = mapper.Map<CustomerDTO>(customer);

            return Ok(customerDTO);
        }

        /// <summary>
        /// Logs in customer with customer credentials
        /// </summary>
        /// <param name="login">login credentials</param>
        /// <returns>returns Jwt response</returns>
        [HttpPost(Constants.LOGIN_ENDPOINT)]
        public async Task<ActionResult<JwtResponseDTO>> LoginInCustomerAsync([FromBody] LoginRequestDTO login)
        {
            logger.LogInformation(Constants.LOG_LOG_IN_CUSTOMER);

            var authorizationTokens = await customerProvider.LogInCustomerAsync(login.Email, login.Password);

            return Ok(authorizationTokens);
        }

        /// <summary>
        /// Refreshs access token with refresher token if its valid
        /// </summary>
        /// <param name="refreshToken">refresher token to validate</param>
        /// <returns>Jwt resposne wih new access token</returns>
        [HttpGet(Constants.REFRESH_ENDPOINT)]
        public async Task<ActionResult<JwtResponseDTO>> RefreshCustomerToken([FromHeader(Name = "Authorization")] string refreshToken)
        {
            logger.LogInformation(Constants.LOG_REFRESH_CUSTOMER_TOKEN);

            var authorizationTokens = await customerProvider.RefreshCustomerToken(refreshToken);

            return Ok(authorizationTokens);
        }

        /// <summary>
        /// Regsiters an account with the API
        /// </summary>
        /// <param name="customerToCreate">customer to register</param>
        /// <returns>new customer</returns>
        [HttpPost(Constants.REGISTER_ENDPOINT)]
        public async Task<ActionResult<CustomerDTO>> RegisterCustomerAsync([FromBody] CustomerDTO customerToCreate)
        {
            logger.LogInformation(Constants.LOG_REGISTER_CUSTOMER);

            var createdCustomer = await customerProvider.RegisterCustomerAsync(customerToCreate);
            var customerDto = mapper.Map<CustomerDTO>(createdCustomer);

            return Created(Constants.CUSTOMER_ENDPOINT, customerDto);
        }


    }
}
