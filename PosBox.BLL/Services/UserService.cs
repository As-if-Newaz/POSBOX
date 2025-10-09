using AutoMapper;
using PosBox.BLL.DTOs;
using PosBox.DAL;
using PosBox.DAL.Entity_Framework.Table_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static PosBox.DAL.Entity_Framework.Table_Models.Enums;

namespace PosBox.BLL.Services
{
    public class UserService
    {
        private readonly DataAccess DA;

        public UserService(DataAccess dataAccess)
        {
            DA = dataAccess;
        }

        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>().ReverseMap();

            });
            return new Mapper(config);
        }

        public bool Register(UserDTO obj, out string errorMsg)
        {
            errorMsg = string.Empty;
            
            // Don't override the role if already set
            if (obj.UserRole == 0)
            {
                obj.UserRole = UserRole.Manager; // Default to Manager
            }
            
            // Only set language/theme if not already set
            if (obj.PreferredLanguage == null)
            {
                obj.PreferredLanguage = Language.English;
            }
            
            if (obj.PreferredTheme == null)
            {
                obj.PreferredTheme = Theme.Light;
            }
            
            obj.CreatedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
            obj.UserStatus = UserStatus.Inactive;
            
            // Generate verification code
            byte[] bytes = new byte[4];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }
            int code = Math.Abs(BitConverter.ToInt32(bytes, 0)) % 1000000;
            obj.EmailVerificationCode = code.ToString("D6");
            obj.EmailVerificationExpiry = DateTime.UtcNow.AddMinutes(5);
            
            try
            {
                var data = GetMapper().Map<User>(obj);
                return DA.UserData().Create(data, out errorMsg);
            }
            catch (Exception ex)
            {
                errorMsg = $"Error during user creation: {ex.Message}";
                return false;
            }
        }

        public UserDTO? GetById(int userId)
        {
            var data = DA.UserData().Get(userId);
            if (data == null) return null;
            return GetMapper().Map<UserDTO>(data);
        }

        public IEnumerable<UserDTO> GetAll()
        {
            var data = DA.UserData().GetAll();
            return GetMapper().Map<List<UserDTO>>(data);
        }

        public UserDTO? GetUserByEmail(string email)
        {
            var user = DA.UserData().GetByEmail(email);
            if (user == null)
            {
                return null;
            }
            return GetMapper().Map<UserDTO>(user);
        }

        public bool BlockUsers(List<int> userIds)
        {
            foreach (var userId in userIds)
            {
                if (!UpdateUserStatus(userId, UserStatus.Blocked))
                {
                    return false;
                }
            }
            return true;
        }

        public bool UnblockUsers(List<int> userIds)
        {
            foreach (var userId in userIds)
            {
                if (!UpdateUserStatus(userId, UserStatus.Active))
                {
                    return false;
                }
            }
            return true;
        }

        public bool ActivateUsers(List<int> userIds)
        {
            foreach (var userId in userIds)
            {
                if (!UpdateUserStatus(userId, UserStatus.Active))
                {
                    return false;
                }
            }
            return true;
        }

        public bool InactivateUsers(List<int> userIds)
        {
            foreach (var userId in userIds)
            {
                if (!UpdateUserStatus(userId, UserStatus.Inactive))
                {
                    return false;
                }
            }
            return true;
        }

        public bool DeleteUsers(List<int> userIds)
        {
            foreach (var userId in userIds)
            {
                var user = DA.UserData().Get(userId);
                if (user == null)
                {
                    return false;
                }
                if (!DA.UserData().Delete(user.Id))
                {
                    return false;
                }
            }
            return true;
        }

        public bool UpdateUser(UserDTO obj)
        {
            var user = DA.UserData().Get(obj.Id);
            if (user == null)
            {
                return false;
            }
            user.UserName = obj.UserName;
            user.Email = obj.Email;
            user.UserStatus = obj.UserStatus;
            user.EmailVerificationCode = obj.EmailVerificationCode;
            user.EmailVerificationExpiry = obj.EmailVerificationExpiry;
            return DA.UserData().Update(user);
        }

        public bool UpdateUserStatus(int userId, UserStatus status)
        {
            return DA.UserData().UpdateUserStatus(userId, status);
        }

        public bool UpdateUserRole(List<int> userIds, UserRole role)
        {
            foreach (var userId in userIds)
            {
                var user = DA.UserData().Get(userId);
                if (user == null)
                {
                    return false;
                }
                if (!DA.UserData().UpdateUserRole(userId, role))
                {
                    return false;
                }
            }
            return true;
        }

        public bool UpdatePreferences(int userId, UserPreferencesDTO preferences)
        {
            return DA.UserData().UpdatePreferences(userId, preferences.PreferredLanguage, preferences.PreferredTheme);
        }
        public List<UserDTO> SearchUsers(string searchTerm)
        {
            var users = DA.UserData().SearchUsers(searchTerm);
            return GetMapper().Map<List<UserDTO>>(users);
        }

        public bool UserBusinessAccess(int userId, int businessId)
        {
            return DA.UserData().UserBusinessAccess(userId, businessId);
        }
    }
}
