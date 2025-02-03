using System.ComponentModel.DataAnnotations;

namespace CompanyAPI.DTOs
{
    public class CreateUserDto
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Role { get; set; }
        public bool IsActive { get; set; }
    }

}
