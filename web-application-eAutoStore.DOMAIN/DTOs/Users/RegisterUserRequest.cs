using System.ComponentModel.DataAnnotations;

namespace web_application_eAutoStore.DTOs.Users
{
    public class RegisterUserRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
