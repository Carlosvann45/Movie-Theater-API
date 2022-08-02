using Movie.Theater.Enterprises.Models.Entities;

namespace Movie.Theater.Enterprises.Utilities.Jwt
{
    public interface IJwtUtility
    {
        string GetEmailFromToken(string bearerToken);

        bool ValidateToken(string bearerToken, string secret);

        string CreateAccessToken(Customer customer);

        string CreatRefresherToken(Customer customer);
    }
}
