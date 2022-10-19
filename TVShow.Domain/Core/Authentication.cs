using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TVShow.Domain.Core
{
    public static class Authentication
    {
        public static Token GenereteToken(IdentityUser<Guid> user)
        {
            Claim[] claimUser = new Claim[]
            {
                new Claim("username", user.UserName),
                new Claim("id", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("0asdjas09djsa09djasdjsadajsd09asjd09sajcnzxn"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claimUser,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddHours(1)
             );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new Token(tokenString);
        }
    }
}
