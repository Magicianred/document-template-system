using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoggerService
{
    class DefaultLogger : ILoggerService
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        public void Debug(string log)
        {
            throw new NotImplementedException();
        }

        public void Error(string log)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string log)
        {
            throw new NotImplementedException();
        }

        public void Info(string log)
        {
            throw new NotImplementedException();
        }

        public void Trace(string log)
        {
            throw new NotImplementedException();
        }

        public void Warn(string log)
        {
            throw new NotImplementedException();
        }
    }
}
