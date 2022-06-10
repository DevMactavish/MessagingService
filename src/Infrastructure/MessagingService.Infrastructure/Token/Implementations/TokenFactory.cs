using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MessagingService.Infrastructure.Token.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace MessagingService.Infrastructure.Token.Implementations
{
    public class TokenFactory:ITokenFactory
    {
        public (string, DateTime) GenerateToken(string id, string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes("MySecretKeyforapp12");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("userId", id),
                    new Claim("username", userName),
                    new Claim(ClaimTypes.Name,userName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            
            return (jwt,token.ValidTo);
        }
    }
}