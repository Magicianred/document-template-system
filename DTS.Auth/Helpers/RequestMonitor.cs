using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Auth.Helpers
{
    public class RequestMonitor : IRequestMonitor
    {
        private Dictionary<string, int> _loggins;
        private int _maxLoginAttempts;

        public RequestMonitor(int loginAttempts)
        {
            this._maxLoginAttempts = loginAttempts;
            this._loggins = new Dictionary<string, int>();
        }

        public bool IsReachedLoginAttemptsLimit(string login)
        {
            if (_loggins.ContainsKey(login))
            {
                _loggins[login]++;
                if (_loggins[login] < _maxLoginAttempts)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                _loggins.Add(login, 1);
                return false;
            }
        }

        public void ResetLoginAttempts(string login)
        {
            if (_loggins.ContainsKey(login))
            {
                _loggins.Remove(login);
            }
        }

        public bool VerifyRequestRateLimit(string ip)
        {
            throw new NotImplementedException();
        }


    }
}
