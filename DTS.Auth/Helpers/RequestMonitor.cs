using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace DTS.Auth.Helpers
{
    public class RequestMonitor : IRequestMonitor
    {
        private const int REDUCTION_INTERVAL = 1000; // 1 sec

        private Timer _reductionTimer;

        private Dictionary<string, int> _loggins;
        private ConcurrentDictionary<string, int> _bannedLogins;

        private int _maxLoginAttempts;
        private int _banTime;

        public RequestMonitor(RequestMonitorConfig monitorConfig)
        {
            this._reductionTimer = CreateTimer();

            this._maxLoginAttempts = monitorConfig.LoginAttempts;
            this._banTime = monitorConfig.BanTime;

            this._loggins = new Dictionary<string, int>();
            this._bannedLogins = new ConcurrentDictionary<string, int>();
        }

        public bool IsReachedLoginAttemptsLimit(string login)
        {
            if (_bannedLogins.ContainsKey(login))
            {
                return true;
            }

            if (_loggins.ContainsKey(login))
            {
                _loggins[login]++;
                if (_loggins[login] < _maxLoginAttempts)
                {
                    return false;
                }
                else
                {
                    _bannedLogins.TryAdd(login, _banTime);
                    _loggins.Remove(login);
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

        private Timer CreateTimer()
        {
            Timer timer = GetTimer(REDUCTION_INTERVAL);
            timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
            return timer;
        }

        private Timer GetTimer(int interval)
        {
            Timer timer = new Timer();
            timer.Interval = interval;
            timer.Start();

            return timer;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            foreach (var key in _bannedLogins.Keys)
            {
                _bannedLogins[key]--;
                if (_bannedLogins[key] == 0)
                    _bannedLogins.TryRemove(key, out var i);
            }
        }
    }
}
