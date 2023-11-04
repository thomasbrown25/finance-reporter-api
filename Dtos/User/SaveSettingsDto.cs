using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace finance_reporter_api.Dtos.User
{
    public class SaveSettingsDto
    {
        public string? FontSize { get; set; }
        public string? Language { get; set; }
        public string? Messages { get; set; }
        public bool DarkMode { get; set; }
        public bool SidenavMini { get; set; }
        public bool NavbarFixed { get; set; }
        public string? SidenavType { get; set; }
        public string? BackgroundColor { get; set; }
    }
}