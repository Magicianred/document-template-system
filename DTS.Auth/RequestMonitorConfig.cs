using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Auth
{
    public class RequestMonitorConfig
    {
        public int LoginAttempts { get; set; }
        public int LoginBanTime { get; set; }
        public int MaxRequestsPerSec { get; set; }
        public int IpBanTime { get; set; }
    }
}
