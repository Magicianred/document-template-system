using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Auth.Helpers
{
    public interface ICredentialsRestrictionValidation
    {
        bool VerifyPassword(string login, string password);
    }
}
