using ChatAPI.DateTime;
using ChatAPI.DateTime.Abstractions;
using ChatAPI.Domain.Entities;
using ChatAPI.Tokens.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChatAPI.Tokens
{

    public class TokenProvider() : ITokenProvider
    {
        private readonly IDateTimeProvider dateTimeProvider = new DateTimeProvider();

        public string Issuer { get => "MY_JWT_ISSUER"; }
        public string Audience { get => "MY_JWT_AUDIENCE"; }
        public string Key { get => "MY_VERY_SECRET_AND_HARD_TO_GUESS_JWT_KEY_0123456789"; }

        public string CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)

                ]),
                Expires = dateTimeProvider.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = Issuer,
                Audience = Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
