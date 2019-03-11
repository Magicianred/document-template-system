using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Helpers
{
    interface IHashAble
    {
        string Hash(string s);
        bool Verify(string submited, string hashed);
    }
}
