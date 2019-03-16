using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DTS.Auth.Helpers
{
    public class DefaultRestriction : ICredentialsRestrictionValidation
    {
        private Regex passwordRestriction = new Regex("^(?=.*[a - z])(?=.*[A - Z])(?=.*[0 - 9])(?=.*[!@#$%^&*])(?=.{8,})");

        public bool VerifyPassword(string login, string password)
        {
            if (login.Equals(password))
            {
                return false;
            }
            if (!passwordRestriction.IsMatch(password))
            {
                return false;
            }
            return true;
        }
    }
}
