using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Auth
{
    public class RequestMonitorConfig
    {
        public int LoginAttempts { get; set; }
        public int BanTime { get; set; }
    }
}
