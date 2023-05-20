using Microsoft.AspNetCore.Mvc;
using MiniMart.API.Services;
using MiniMart.Domain.DTOs.User;
using MiniMart.Domain.DTOs.Users;

namespace MiniMart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
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

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromServices] UserService _userService, LoginRequest loginRequest)
        {
            try
            {
                var user = await _userService.Login(loginRequest);
                if (await _userService.Login(loginRequest) == null)
                {
                    return Unauthorized("Invalid Username or Password");
                }
                return Ok(new LoginResponse
                {
                    Username = loginRequest.UserName,
                    Token = _userService.CreateToken(user)
                });
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }
    }
}
