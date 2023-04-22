using System.ComponentModel.DataAnnotations;

namespace MiniMart.Domain.DTOs.User
{
    public class RegisterRequest
    {
        public RegisterRequest()
        {

        }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
