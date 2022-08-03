using Movie.Theater.Enterprises.Models.Misc;
using Newtonsoft.Json;

namespace Movie.Theater.Enterprises.Models.Entities
{
    /// <summary>
    /// User entity to represent a user in the database
    /// </summary>
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public Role Role { get; set; } = Role.Customer;

        public byte[] PasswordHash { get; set; } = new byte[32];

        public byte[] PasswordSalt { get; set; } = new byte[32];

        public string PhoneNumber { get; set; } = string.Empty;

        public bool RecieveTextPromotions { get; set; } = false;

        public bool RecieveEmailPromotions { get; set; } = false;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static IEqualityComparer<Customer> ProductComparer { get; } = new ProductEqualityComparer();

        private sealed class ProductEqualityComparer : IEqualityComparer<Customer>
        {
            public bool Equals(Customer? x, Customer? y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.FirstName == y.FirstName
                    && x.LastName == y.LastName
                    && x.Email == y.Email
                    && x.PasswordHash == y.PasswordHash
                    && x.PasswordSalt == y.PasswordSalt
                    && x.PhoneNumber == y.PhoneNumber
                    && x.RecieveTextPromotions == y.RecieveTextPromotions
                    && x.RecieveEmailPromotions == y.RecieveEmailPromotions;
            }

            public int GetHashCode(Customer obj)
            {
                var hashCode = new HashCode();
                hashCode.Add(obj.FirstName);
                hashCode.Add(obj.LastName);
                hashCode.Add(obj.Email);
                hashCode.Add(obj.PasswordSalt);
                hashCode.Add(obj.PasswordHash);
                hashCode.Add(obj.PhoneNumber);
                hashCode.Add(obj.RecieveTextPromotions);
                hashCode.Add(obj.RecieveEmailPromotions);
                return hashCode.ToHashCode();
            }
        }

    }
}
