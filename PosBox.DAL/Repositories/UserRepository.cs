using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PosBox.DAL.Entity_Framework;
using PosBox.DAL.Entity_Framework.Table_Models;
using PosBox.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PosBox.DAL.Entity_Framework.Table_Models.Enums;

namespace PosBox.DAL.Repositories
{
    internal class UserRepository : Repository<User>, IUser
    {
        private readonly PasswordHasher<string> pw;

        public UserRepository(ApplicationDBContext db) : base(db)
        {
            pw = new PasswordHasher<string>();
        }

        public User? Authenticate(string email, string password, out string errorMsg)
        {
            errorMsg = string.Empty;
            var user = db.Users.Where(u => u.Email == email).FirstOrDefault();
            if (user != null)
            {
                var passVerification = pw.VerifyHashedPassword("", user.Password, password);
                if (passVerification == PasswordVerificationResult.Success)
                {
                    return user;
                }
                errorMsg = "Wrong Password";
                return null;
            }
            errorMsg = "User not found!";
            return null;
        }

        public bool Create(User obj, out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                var hashedPassword = pw.HashPassword("", obj.Password);
                obj.Password = hashedPassword;
                dbSet.Add(obj);
                if (db.SaveChanges() > 0) return true;
                return false;
            }
            catch (DbUpdateException ex)
            {
                errorMsg = "Email is already registered!";
                return false;
            }
            catch (Exception ex)
            {
                errorMsg = "Internal server error";
                return false;
            }
        }

        public User? GetByEmail(string email)
        {
            return dbSet.AsNoTracking().FirstOrDefault(u => u.Email == email);
        }

        public DateTime GetLastLogin(int userId)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                throw new InvalidOperationException("User not found.");
            if (!user.LastLogin.HasValue)
                throw new InvalidOperationException("LastLogin is not set for this user.");
            return user.LastLogin.Value;
        }

        public IEnumerable<User> SearchUsers(string searchTerm)
        {
            return dbSet.Where(u => u.UserName.Contains(searchTerm) || u.Email.Contains(searchTerm))
                       .AsNoTracking()
                       .ToList();
        }

        public bool UpdateUserStatus(int userId, UserStatus status)
        {
            var user = Get(userId);
            if (user != null)
            {
                user.UserStatus = status;
                return Update(user);
            }
            return false;
        }

        public bool UpdateUserRole(int userId, UserRole role)
        {
            var user = Get(userId);
            if (user != null)
            {
                user.UserRole = role;
                return Update(user);
            }
            return false;
        }

        public bool UpdatePreferences(int userId, Language language, Theme theme)
        {
            var user = Get(userId);
            if (user != null)
            {
                user.PreferredLanguage = language;
                user.PreferredTheme = theme;
                return Update(user);
            }
            return false;
        }

        public bool UserBusinessAccess(int userId, int businessId)
        {
            var user = Get(userId);
            if (user.UserRole.Equals(UserRole.Admin))
            {
                return true;
            }
            else if (user.UserRole.Equals(UserRole.Manager))
            {
                if ((db.BusinessAccesses.FirstOrDefault(ba => ba.UserId == userId && ba.BusinessId == businessId) != null))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
