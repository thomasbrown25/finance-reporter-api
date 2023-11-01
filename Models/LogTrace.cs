using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace finance_reporter_api.Models
{
    public class LogTrace
    {
        public int Id { get; set; }
        public DateTime LogDate { get; set; } = DateTime.Now.ToLocalTime();
        public string? Message { get; set; }
    }
}