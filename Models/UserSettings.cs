using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace finance_reporter_api.Models
{
    public class UserSettings
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FontSize { get; set; } = "16px";
        public string? Language { get; set; } = "English";
        public string? Messages { get; set; }
        public bool DarkMode { get; set; } = true;
        public bool SidenavMini { get; set; } = false;
        public bool NavbarFixed { get; set; } = true;
        public string? SidenavType { get; set; } = "dark";
        public string BackgroundColor { get; set; } = "primary";
    }
}
