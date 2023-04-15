using Microsoft.IdentityModel.Tokens;
using MiniMart.Domain.Entities;
using MiniMart.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("PhoneNumber", user.PhoneNumber ?? ""),
                new Claim("Roles", roles),
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
    }
}
