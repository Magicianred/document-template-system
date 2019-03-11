using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Helpers
{
    interface IHashString
    {
        string HashWithSalt(string s, string salt);
    }
}
