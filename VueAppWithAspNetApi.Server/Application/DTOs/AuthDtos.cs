using System.ComponentModel.DataAnnotations;

namespace VueAppWithAspNetApi.Server.Application.DTOs
{
    public class UserLoginDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [MinLength(6)]
        public required string Password { get; set; }
    }

    public class UserRegisterDto
    {
        [Required]
        [MinLength(3)]
        public required string Username { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [MinLength(6)]
        public required string Password { get; set; }
    }

    public class UserResponseDto
    {
        public Guid Id { get; set; }
        
        [Required]
        public required string Username { get; set; }
        
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        
        [Required]
        public required string Token { get; set; }
    }
} 