using System.ComponentModel.DataAnnotations;

namespace MiniMart.Domain.DTOs.User
{
    public class RegisterRequest
    {
        public RegisterRequest()
        {

        }
        [Required]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        public string PhoneNumber { get; set; }
    }
}
