using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finance_reporter_api.Dtos.User;
using finance_reporter_api.Dtos.User;
using finance_reporter_api.Models;
using finance_reporter_api.Utils;

namespace finance_reporter_api.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<LoadUserDto>> Register(User user, string password);
        Task<ServiceResponse<LoadUserDto>> Login(string email, string password);
        Task<bool> UserExists(string email);
        Task<ServiceResponse<LoadUserDto>> LoadUser();
        Task<ServiceResponse<string>> DeleteUser();
        Task<ServiceResponse<SettingsDto>> GetSettings();
        Task<ServiceResponse<SaveSettingsDto>> SaveSettings(SaveSettingsDto newSettings);
    }
}