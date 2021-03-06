﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DTS.Auth.Helpers
{
    public interface IHashPassword
    {
        string Hash(string s);
        bool Verify(string submited, string hashed);
    }
}
