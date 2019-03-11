using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Helpers
{
    public class TokenHelper : ITokenHelper
    {
        private string secret;
        private double _tokenLiveLenght;

        public TokenHelper(string secret)
        {
            this.secret = secret;
        }

        public SecurityToken GetNewToken(int userId, string userRole)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = GetSecurityTokenDescriptor(userId, userRole, key, _tokenLiveLenght);
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return token;
        }

        private SecurityTokenDescriptor GetSecurityTokenDescriptor(int userId, string userRole, byte[] key, double _tokenLiveLength)
        {
            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId.ToString()),
                    new Claim(ClaimTypes.Role, userRole)
                }),

                Expires = DateTime.Now.AddHours(_tokenLiveLenght),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
        }

        public SecurityToken parseToken(string token)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(token);
        }
    }
}
