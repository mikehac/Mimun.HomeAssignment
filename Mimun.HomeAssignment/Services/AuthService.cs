using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mimun.HomeAssignment.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mimun.HomeAssignment.Services
{
    public class AuthService : IAuthService
    {
        private readonly JWT _jwt;

        public AuthService(IOptions<JWT> jwt)
        {
            _jwt = jwt.Value;
        }

        public string GetToken(string idNumber)
        {
            var claims = new List<Claim>
            {
                        new Claim("Id", Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, idNumber),
                        new Claim(JwtRegisteredClaimNames.Email, _jwt.FakeEmail),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwt.Key));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512Signature);
            var tokeOptions = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signinCredentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }
    }
}
