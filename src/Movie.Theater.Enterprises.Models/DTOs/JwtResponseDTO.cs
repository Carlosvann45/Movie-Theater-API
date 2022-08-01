namespace Movie.Theater.Enterprises.Models.DTOs
{
    public class JwtResponseDTO
    {
        public string AccessToken { get; set; } = string.Empty;

        public string RefresherToken { get; set; } = string.Empty;
    }
}
