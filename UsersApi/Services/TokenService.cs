using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsersApi.Models;

namespace UsersApi.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }

    public class TokenService : ITokenService
    {
        public string GenerateToken(User user)
        {
            var claims = new Claim[]
            {
                new Claim("username", user.UserName),
                new Claim("id", user.Id),
                new Claim(ClaimTypes.DateOfBirth, user.DateofBirth.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("233eyhwe8ih89214he8eih28e2h3823h823"));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
               expires: DateTime.Now.AddMinutes(10),
               claims: claims,
               signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

