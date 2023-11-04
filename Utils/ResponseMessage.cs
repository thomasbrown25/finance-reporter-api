using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace finance_reporter_api.Utils
{
    public class ResponseMessage
    {
        // User
        public const string UserRegisterSuccess = "Registered user successfully";
        public const string UserRegisterFailed = "Failed to register user";
        public const string UserLoginSuccess = "Login user successfully";
        public const string UserLoginFailed = "Failed to login user";
        public const string UserSettingGetSuccess = "Retrieved user settings successfully";
        public const string UserSettingGetFailed = "Failed to retrive user settings";
        public const string UserSettingSaveSuccess = "Saved user settings successfully";
        public const string UserSettingSaveFailed = "Failed to save user settings";

        // Credit Report API
        public const string CreditReportGetSuccess = "Retrieved credit report successfully";
        public const string CreditReportGetFailed = "Failed to retrieve credit report";
    }
}