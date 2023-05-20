namespace MiniMart.Domain.DTOs.Users
{
    public class LoginResponse
    {
        public LoginResponse()
        {

        }
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
