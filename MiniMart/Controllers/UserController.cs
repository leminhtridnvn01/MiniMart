using Microsoft.AspNetCore.Mvc;

namespace MiniMart.API.Controllers
{
    public class UserController : BaseController
    {
        public UserController() { }
        [HttpGet("current-user")]
        public bool Get()
        {
            return true;
        }
    }
}
