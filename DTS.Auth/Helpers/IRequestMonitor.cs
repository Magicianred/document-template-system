using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Auth.Helpers
{
    public interface IRequestMonitor
    {
        bool IsReachedLoginAttemptsLimit(string login);
        void ResetLoginAttempts(string login);
        bool VerifyRequestRateLimit(string ip);
    }
}
