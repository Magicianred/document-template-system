using System;
using System.Collections.Generic;
using System.Text;

namespace LoggerService
{
    interface ILoggerService
    {
        void Fatal(string log);
        void Error(string log);
        void Warn(string log);
        void Info(string log);
        void Debug(string log);
        void Trace(string log);
    }
}
