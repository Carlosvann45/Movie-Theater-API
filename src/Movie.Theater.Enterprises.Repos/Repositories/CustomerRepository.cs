using Microsoft.EntityFrameworkCore;
using Movie.Theater.Enterprises.Models.Entities;
using Movie.Theater.Enterprises.Repos.Context;
using Movie.Theater.Enterprises.Repos.Interfaces;

namespace Movie.Theater.Enterprises.Repos.Repositories
{
    /// <summary>
    /// The implementation of repository methods to access database
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMovieTheaterDb db;

        public CustomerRepository(IMovieTheaterDb db)
        {
            this.db = db;
        }

        /// <summary>
        /// Gets a user by a username 
        /// </summary>
        /// <param name="username">username to search for</param>
        /// <returns>user or null if user is found</returns>
        public async Task<Customer?> GetCustomerByEmailAsync(string email)
        {
            return await db.Customers.FirstOrDefaultAsync(customer => customer.Email == email);
        }

        /// <summary>
        /// Checks to see if a email already exist for a user
        /// </summary>
        /// <param name="email">email to check</param>
        /// <returns>boolean value dependent on if a user is found</returns>
        public async Task<bool> CustomerEmailExistAsync(string email)
        {
            return await db.Customers.FirstOrDefaultAsync(customer => customer.Email == email) != null;
        }

        /// <summary>
        /// saves user to database
        /// </summary>
        /// <param name="user">user to save</param>
        /// <returns>saved user</returns>
        public async Task<Customer> RegisterCustomerAsync(Customer customer)
        {
            db.Customers.Add(customer);
            await db.SaveChangesAsync();

            return customer;

        }
    }
}
