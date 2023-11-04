using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finance_reporter_api.Models;
using finance_reporter_api.Data;

namespace finance_reporter_api.Services.LoggerService
{
    public class Logger : ILogger
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Logger(DataContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public void LogTrace(string message)
        {
            LogTrace log = new();

            log.Message = message;

            _context.LogTrace.Add(log);
            _context.SaveChanges();
        }

        public void LogException(Exception? exception)
        {
            LogException log = new();

            log.ExceptionMessage = exception.Message;
            log.ExceptionStackTrace = exception.StackTrace;
            log.InnerExceptionMessage = exception.InnerException?.Message;
            log.InnerExceptionStackTrace = exception.InnerException?.StackTrace;

            _context.LogException.Add(log);
            _context.SaveChanges();
        }

        public void LogDataExchange(string messageSource, string messageTarget, string methodCall, string messagePayload)
        {
            LogDataExchange log = new();

            log.MessageSource = messageSource;
            log.MessageTarget = messageTarget;
            log.MethodCall = methodCall;
            log.MessagePayload = messagePayload;

            _context.LogDataExchange.Add(log);
            _context.SaveChanges();

        }
    }
}