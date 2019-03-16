using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Auth.Helpers
{
    interface IRequestMonitor
    {
        bool VerifyLoginLimitAttempts(string login);
        bool VerifyRequestRateLimit(string ip);
    }
}
