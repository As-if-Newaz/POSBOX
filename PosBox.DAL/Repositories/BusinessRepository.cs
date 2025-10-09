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

namespace PosBox.DAL.Repositories
{
    internal class BusinessRepository : Repository<Business>, IBusiness
    {
        private readonly PasswordHasher<string> pw;
        public BusinessRepository(ApplicationDBContext db) : base(db)
        {
            pw = new PasswordHasher<string>();
        }
        public Business? Authenticate(string BusinessUserName, string password, out string errorMsg)
        {
            errorMsg = string.Empty;
            var business = db.Businesses.Where(u => u.BusinessUserName == BusinessUserName).FirstOrDefault();
            if (business != null)
            {
                var passVerification = pw.VerifyHashedPassword("", business.Password, password);
                if (passVerification == PasswordVerificationResult.Success)
                {
                    return business;
                }
                errorMsg = "Wrong Password";
                return null;
            }
            errorMsg = "Business not found!";
            return null;
        }

        public bool Create(Business obj, out string errorMsg)
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
                errorMsg = "Business UserName is already registered!";
                return false;
            }
            catch (Exception ex)
            {
                errorMsg = "Internal server error";
                return false;
            }
        }

        public Business? GetByBusinessByUserName(string uName)
        {
            return dbSet.AsNoTracking().FirstOrDefault(u => u.BusinessUserName == uName);
        }

        public DateTime GetLastLogin(int businessId)
        {
            var business = db.Businesses.FirstOrDefault(u => u.Id == businessId);
            if (business == null)
                throw new InvalidOperationException("business not found.");
            if (!business.LastLogin.HasValue)
                throw new InvalidOperationException("LastLogin is not set for this business.");
            return business.LastLogin.Value;
        }

        public IEnumerable<Business> SearchBusiness(string searchTerm)
        {
            return dbSet.Where(u => u.Name.Contains(searchTerm) || u.BusinessUserName.Contains(searchTerm) || u.Address.Contains(searchTerm))
                       .AsNoTracking()
                       .ToList();
        }

        public bool UpdatePreferences(int Id, Enums.Language language, Enums.Theme theme)
        {
            var business = Get(Id);
            if (business != null)
            {
                business.PreferredLanguage = language;
                business.PreferredTheme = theme;
                return Update(business);
            }
            return false;
        }

        public bool UpdateBusinessStatus(int Id, Enums.UserStatus status)
        {
            var business = Get(Id);
            if (business != null)
            {
                business.BusinessStatus = status;
                return Update(business);
            }
            return false;
        }
    }
}
