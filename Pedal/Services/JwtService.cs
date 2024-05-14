using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Pedal.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pedal.Services
{
    public class JwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public string IssueNew(Car car)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, car.Id),
                new Claim(ClaimTypes.Email, car.Email),
                new Claim(ClaimTypes.GivenName, car.Brand + car.Model)
            };
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public bool IsExpired(string token)
        {
            var handler = new JsonWebTokenHandler();
            SecurityToken securityToken = handler.ReadToken(token);
            var now = DateTime.UtcNow;

            if(securityToken == null) 
            {
                throw new SecurityTokenException("Token is not in the expected format");
            }

            return (now > securityToken.ValidTo);
        }

        //public async Task<bool> IsValidAsync(string token)
        //{
        //    var handler = new JsonWebTokenHandler();
        //    var validationParameters = new TokenValidationParameters
        //    {
        //        ValidIssuer = _config["Jwt:Issuer"],
        //        ValidAudience = _config["Jwt:Audience"],
        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!))
        //    };
        //    TokenValidationResult result = await handler.ValidateTokenAsync(token, validationParameters);

        //    if (IsExpired(token))
        //    {
        //        throw new SecurityTokenException("Token has expired.");
        //    }

        //    return (result.IsValid);
        //}

        public async Task<TokenValidationResult> IsValidAsync(string token)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

            var handler  = new JsonWebTokenHandler();
            var validationResult = await handler.ValidateTokenAsync(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidIssuer = _config["Jwt:Issuer"],
                ValidAudience = _config["Jwt:Audience"],
                IssuerSigningKey = securityKey,
            });

                
            if (validationResult.Exception != null)
            {
                var res = validationResult.Exception;
                throw validationResult.Exception;
            }

            return validationResult;
            
        }


    }
}
