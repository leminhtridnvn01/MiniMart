using Microsoft.AspNetCore.Mvc;

namespace MiniMart.API.Controllers
{
    [ApiController]
    public class UserController
    {
        public UserController() { }
        [HttpGet("current-user")]
        public bool Get()
        {
            return true;
        }
    }
}
