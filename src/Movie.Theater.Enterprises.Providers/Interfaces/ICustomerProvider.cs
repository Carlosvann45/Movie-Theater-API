using Movie.Theater.Enterprises.Models.DTOs;
using Movie.Theater.Enterprises.Models.Entities;

namespace Movie.Theater.Enterprises.Providers.Interfaces
{
    /// <summary>
    /// This interface provides a abstraction layer for user provider
    /// </summary>
    public interface ICustomerProvider
    {
        /// <inheritdoc cref="Providers.CustomerProvider.GetCustomerWithTokenAsync" />
        Task<Customer?> GetCustomerWithTokenAsync(string bearerToken, string email);

        /// <inheritdoc cref="Providers.CustomerProvider.GetCustomerByEmailAsync" />
        Task<Customer?> GetCustomerByEmailAsync(string email);

        /// <inheritdoc cref="Providers.CustomerProvider.LogInCustomerAsync" />
        Task<JwtResponseDTO> LogInCustomerAsync(string email, string password);

        /// <inheritdoc cref="Providers.CustomerProvider.RefreshCustomerToken" />
        Task<JwtResponseDTO> RefreshCustomerToken(string refresherToken);

        /// <inheritdoc cref="Providers.CustomerProvider.RegisterCustomerAsync" />
        Task<Customer> RegisterCustomerAsync(CustomerDTO customer);

    }
}
