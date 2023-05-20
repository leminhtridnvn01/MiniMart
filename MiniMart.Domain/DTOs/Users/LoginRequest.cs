using System.ComponentModel.DataAnnotations;

namespace MiniMart.Domain.DTOs.Users
{
    public class LoginRequest
    {
        public LoginRequest() { }
        [Required]
        [StringLength(32)]
        public string UserName { get; set; }

        [Required]
        [StringLength(32)]
        public string Password { get; set; }
    }
}
