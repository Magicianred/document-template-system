using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Auth.Helpers
{
    public class RequestMonitor : IRequestMonitor
    {
        public bool VerifyLoginLimitAttempts(string login)
        {
            throw new NotImplementedException();
        }

        public bool VerifyRequestRateLimit(string ip)
        {
            throw new NotImplementedException();
        }
    }
}
