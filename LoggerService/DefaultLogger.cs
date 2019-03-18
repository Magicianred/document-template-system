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
            logger.Debug(log);
        }

        public void Error(string log)
        {
            logger.Error(log);
        }

        public void Fatal(string log)
        {
            logger.Fatal(log);
        }

        public void Info(string log)
        {
            logger.Info(log);
        }

        public void Trace(string log)
        {
            logger.Trace(log);
        }

        public void Warn(string log)
        {
            logger.Warn(log);
        }
    }
}
