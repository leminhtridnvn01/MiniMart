using Microsoft.AspNetCore.Mvc;
using MiniMart.API.Services;
using MiniMart.Domain.DTOs.User;

namespace MiniMart.API.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController()
        {
            
        }

        [HttpPost("Register")]
        public async Task<ActionResult<string>> RegisterAsync([FromServices] UserService _userService, RegisterRequest registerDto)
        {
            try
            {
                var newUser = await _userService.CreateUser(registerDto);
                if (newUser == null)
                {
                    return BadRequest("Can not register now!");
                }
                return Ok("Welcome " + newUser.Username + " !!!" + "\n" + "Token: " + newUser.Token);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
