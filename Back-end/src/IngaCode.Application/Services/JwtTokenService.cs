using IngaCode.Application.Configuration;
using IngaCode.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IngaCode.Application.Services
{
    public static class JwtClaimTypes
    {
        public const string UserId = "userId";
        public const string UserName = "userName";
    }

    public class JwtTokenService
    {
        public static object GenerateToken(User user)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes(Key.Secret);

                var tokenConfig = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(JwtClaimTypes.UserId, user.Id.ToString()),
                        new Claim(JwtClaimTypes.UserName, user.UserName)
                    }),
                    Expires = DateTime.UtcNow.AddHours(3),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenConfig);
                var tokenString = tokenHandler.WriteToken(token);

                return new
                {
                    token = tokenString
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao gerar o token", ex);
            }
        }
    }
}
