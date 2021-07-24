using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Homuai.Application.Services.Token
{
    public class TokenController
    {
        private const string EmailAlias = "eml";
        private readonly double _expirationTimeMinutes;
        private readonly string _signingKey;

        public TokenController(double expirationTimeMinutes, string signingKey)
        {
            _expirationTimeMinutes = expirationTimeMinutes;
            _signingKey = signingKey;
        }

        public string Generate(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>
            {
                new Claim(EmailAlias, email)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(SimetricKey(), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }

        public string User(string token)
        {
            var tokenContent = ValidateToken(token);
            return tokenContent.FindFirst(EmailAlias).Value;
        }

        private SymmetricSecurityKey SimetricKey()
        {
            var symmetricKey = Convert.FromBase64String(_signingKey);
            return new SymmetricSecurityKey(symmetricKey);
        }
        private ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters()
            {
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = SimetricKey(),
                ClockSkew = new TimeSpan(0)
            };
            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
            return principal;
        }
    }
}
