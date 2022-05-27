using Movie.Theater.Enterprises.Models.Entities;

namespace Movie.Theater.Enterprises.Repos.Interfaces
{
    /// <summary>
    /// This interface provides a abstraction layer for users repositories
    /// </summary>

    public interface ICustomerRepository
    {
        /// <inheritdoc cref="Repositories.CustomerRepository.GetCustomerByEmailAsync" />
        Task<Customer?> GetCustomerByEmailAsync(string email);

        /// <inheritdoc cref="Repositories.CustomerRepository.CustomerEmailExistAsync" />
        Task<bool> CustomerEmailExistAsync(string email);

        /// <inheritdoc cref="Repositories.CustomerRepository.RegisterCustomerAsync" />
        Task<Customer> RegisterCustomerAsync(Customer user);
    }
}
