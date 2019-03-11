using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;

namespace Auth.Helpers
{
    public class BCryptHash : IHashPassword
    {
        public string Hash(string s)
        {
            return BCrypt.Net.BCrypt.HashPassword(s);
        }

        public bool Verify(string submited, string hashed)
        {
            return BCrypt.Net.BCrypt.Verify(submited, hashed);
        }
    }
}
