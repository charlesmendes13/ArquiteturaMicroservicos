using Identity.Domain.Interfaces.Repositories;
using Identity.Domain.Models;
using Identity.Infraestructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Infraestructure.Repositories
{
    public class AccessTokenRepository : IAccessTokenRepository
    {
        private readonly string _secret;
        private readonly string _iss;
        private readonly string _aud;

        public AccessTokenRepository(IOptions<AccessTokenConfiguration> audienceOptions)
        {
            _secret = audienceOptions.Value.Secret;
            _iss = audienceOptions.Value.Iss;
            _aud = audienceOptions.Value.Aud;
        }

        public async Task<AccessToken> CreateTokenByEmailAsync(string email)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secret));

            var jwt = new JwtSecurityToken(
                issuer: _iss,
                audience: _aud,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(2)),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return await Task.FromResult(new AccessToken
            {
                Token = encodedJwt,
                Expires = jwt.ValidFrom
            });
        }
    }
}
