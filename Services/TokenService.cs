using System;
using System.Text;
using Shop.models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Shop.Services
{
    
    public static class TokenService{

        public static string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var Key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor{
               
               Subject = new ClaimsIdentity(new Claim[]{
                   new Claim(ClaimTypes.Name, user.Username.ToString()),
                   new Claim(ClaimTypes.Role, user.Role.ToString())
               }),
               Expires = DateTime.UtcNow.AddHours(2),
               SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}