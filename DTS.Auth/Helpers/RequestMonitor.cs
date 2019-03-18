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
        private Timer _ipBanTimer;
        private Dictionary<string, int> _loggins;
        private ConcurrentDictionary<string, int> _ipAddresses;
        private ConcurrentDictionary<string, int> _bannedLogins;
        private Stack<string> _bannedIp;

        private int _maxLoginAttempts;
        private int _maxRequestsPerSec;
        private int _loginBanTime;
        private int _ipBanTime;

        public RequestMonitor(RequestMonitorConfig monitorConfig)
        {
            this._maxLoginAttempts = monitorConfig.LoginAttempts;
            this._loginBanTime = monitorConfig.LoginBanTime;
            this._maxRequestsPerSec = monitorConfig.MaxRequestsPerSec;
            this._ipBanTime = monitorConfig.IpBanTime;

            this._loggins = new Dictionary<string, int>();
            this._ipAddresses = new ConcurrentDictionary<string, int>();
            this._bannedLogins = new ConcurrentDictionary<string, int>();
            this._bannedIp = new Stack<string>();

            this._reductionTimer = CreateTimer();
            this._ipBanTimer = CreateIpBanTimer();
        }

        public bool IsReachedLoginAttemptsLimit(string login)
        {
            return IsReachedLimit(login, _maxLoginAttempts, _loginBanTime, _loggins, _bannedLogins);
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
            if (_bannedIp.Contains(ip))
            {
                return true;
            }

            if (!_ipAddresses.ContainsKey(ip))
            {
                _ipAddresses.TryAdd(ip, 1);
                return false;
            }
            else if (_ipAddresses[ip] == _maxRequestsPerSec)
            {
                _bannedIp.Push(ip);
                _ipAddresses.TryRemove(ip, out var i);
                return true;
            }
            else
            {
                _ipAddresses[ip]++;
                return false;
            }
        }

        private bool IsReachedLimit(string value, int maxValue, int banTime, Dictionary<string, int> values, ConcurrentDictionary<string, int> banned)
        {
            if (banned.ContainsKey(value))
            {
                return true;
            }

            if (values.ContainsKey(value))
            {
                values[value]++;
                if (values[value] < maxValue)
                {
                    return false;
                }
                else
                {
                    banned.TryAdd(value, banTime);
                    values.Remove(value);
                    return true;
                }
            }
            else
            {
                values.Add(value, 1);
                return false;
            }
        }

        private Timer CreateTimer()
        {
            Timer timer = GetTimer(REDUCTION_INTERVAL);
            timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
            return timer;
        }

        private Timer CreateIpBanTimer()
        {
            Timer timer = GetTimer(_ipBanTime);
            timer.Elapsed += delegate { if (_bannedIp.Count != 0) { _bannedIp.Pop(); } };
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
                if (_bannedLogins[key] <= 0)
                    _bannedLogins.TryRemove(key, out var i);
            }

            foreach (var key in _ipAddresses.Keys)
            {
                _ipAddresses[key]--;
                if (_ipAddresses[key] <= 0)
                    _ipAddresses.TryRemove(key, out var i);
            }

        }
    }
}
