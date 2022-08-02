using System.ComponentModel.DataAnnotations;

namespace Movie.Theater.Enterprises.Models.DTOs
{
    public class LoginRequestDTO
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
