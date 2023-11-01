using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace finance_reporter_api.Models
{
    public class LogException
    {
        public int Id { get; set; }
        public DateTime LogDate { get; set; } = DateTime.Now.ToLocalTime();
        public string? ExceptionMessage { get; set; }
        public string? ExceptionStackTrace { get; set; }
        public string? InnerExceptionMessage { get; set; }
        public string? InnerExceptionStackTrace { get; set; }
        public string? ExceptionType { get; set; }
        public string? CallingMethod { get; set; }
        public string? CallingAssemblyInfo { get; set; }
    }
}