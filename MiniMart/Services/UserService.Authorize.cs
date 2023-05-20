using Microsoft.IdentityModel.Tokens;
using MiniMart.Domain.DTOs.Users;
using MiniMart.Domain.Entities;
using MiniMart.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MiniMart.API.Services
{
    public partial class UserService
    {
        public string CreateToken(User user)
        {
            var roles = "";
            if (user.Client != null)
                roles += " Client";
            if (user.Staff != null)
                roles += " Staff";
            if (user.Manager != null)
                roles += " Manager";
            var claims = new List<Claim>()
            {
                new Claim("_user_id", user.Id.ToString()),
                new Claim("_name", user.Name),
                new Claim("_email", user.Email),
                new Claim("_phone_number", user.PhoneNumber ?? ""),
                new Claim("_roles", roles),
            };
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSetting.SecretKey));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(10),
                SigningCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<User> Login(LoginRequest loginRequest)
        {

            var user = await _userRepository.GetAsync(s => s.Email == loginRequest.UserName.ToLower());
            if (user == null) return null;

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginRequest.Password));

            for (var i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != user.PasswordHash[i]) return null;
            }
            return user;
        }
    }
}
