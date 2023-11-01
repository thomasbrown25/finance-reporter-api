using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using finance_reporter_api.Data;
using finance_reporter_api.Dtos.User;
using finance_reporter_api.Models;
using finance_reporter_api.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace finance_reporter_api.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Logger.ILogger _logger;

        public AuthService(
            DataContext context,
            IConfiguration configuration,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            Logger.ILogger logger
        )
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<ServiceResponse<LoadUserDto>> Register(User user, string password)
        {
            var response = new ServiceResponse<LoadUserDto>();
            response.Data = new LoadUserDto();

            try
            {
                if (await UserExists(user.Email))
                {
                    response.Message = "A user with that email already exists.";
                    return response;
                }

                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                _context.Users.Add(user);

                UserSettings userSettings = new UserSettings();
                userSettings.UserId = user.Id;

                // before we save user, we create and return the jwt token
                response.Data.JWTToken = CreateToken(user);

                _context.UserSettings.Add(userSettings);

                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<LoadUserDto>(user);

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<LoadUserDto>> Login(string email, string password)
        {
            var response = new ServiceResponse<LoadUserDto>();

            try
            {
                response.Data = new LoadUserDto();

                var user = await _context.Users.FirstOrDefaultAsync(
                    u => u.Email.ToLower().Equals(email.ToLower())
                );

                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User not found.";
                }
                else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                {
                    response.Success = false;
                    response.Message = "Wrong password.";
                }
                else
                {
                    response.Data.JWTToken = CreateToken(user);
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<LoadUserDto>> LoadUser()
        {
            ServiceResponse<LoadUserDto> response = new ServiceResponse<LoadUserDto>();

            try
            {
                User user = Helper.GetCurrentUser(_context, _httpContextAccessor);

                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User not found.";
                    return response;
                }

                response.Data = _mapper.Map<LoadUserDto>(user);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<string>> DeleteUser()
        {
            ServiceResponse<string> response = new ServiceResponse<string>();

            try
            {
                User user = Helper.GetCurrentUser(_context, _httpContextAccessor);


                // var dbTransactions = await _context.Transactions
                //                    .Where(c => c.UserId == user.Id)
                //                    .OrderByDescending(c => c.Date)
                //                    .ToListAsync();

                // _context.RemoveRange(dbTransactions);
                _context.Remove(user);

                _context.SaveChangesAsync();

                response.Data = $"Deleted user {user.FirstName} {user.LastName}";
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<bool> UserExists(string email)
        {
            try
            {
                if (await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower()))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
            }

            return false;
        }

        private void CreatePasswordHash(
            string password,
            out byte[] passwordHash,
            out byte[] passwordSalt
        )
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Name, user.Email)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(
                    _configuration["SecretKey"]
                )
            );

            SigningCredentials creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha512Signature
            );

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    Double.Parse(_configuration["JWTTokenExpiration"])
                ),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
