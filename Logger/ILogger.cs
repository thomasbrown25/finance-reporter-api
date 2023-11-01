using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace finance_reporter_api.Logger
{
    public interface ILogger
    {
        void LogTrace(string message);
        void LogException(Exception? exception);
        void LogDataExchange(string messageSource, string messageTarget, string methodCall, string messagePayload);
    }
}