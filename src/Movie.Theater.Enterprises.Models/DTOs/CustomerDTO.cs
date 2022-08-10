using Movie.Theater.Enterprises.Models.Entities;
using Movie.Theater.Enterprises.Models.Misc;

namespace Movie.Theater.Enterprises.Models.DTOs
{
    /// <summary>
    /// A Data Transfer Object(DTO) to handle transfering data to a user entity
    /// </summary>
    public class CustomerDTO
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public Role Role { get; private set; } = Role.Customer;

        public string Password { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public bool RecieveTextPromotions { get; set; } = false;

        public bool RecieveEmailPromotions { get; set; } = false;

        public Customer TransferToCustomerEntity(byte[] passwordHash, byte[] passwordSalt)
        {
            return new Customer()
            {
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash,
                PhoneNumber = this.PhoneNumber,
                RecieveTextPromotions = this.RecieveTextPromotions,
                RecieveEmailPromotions = this.RecieveEmailPromotions
            };
        }
    }
}
