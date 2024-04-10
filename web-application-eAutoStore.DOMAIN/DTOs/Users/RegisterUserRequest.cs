using System.ComponentModel.DataAnnotations;

namespace web_application_eAutoStore.DOMAIN.DTOs.Users
{
    public class RegisterUserRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(20)]
        public string Password { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(20)]
        public string ConfirmPassword { get; set; }
    }
}
