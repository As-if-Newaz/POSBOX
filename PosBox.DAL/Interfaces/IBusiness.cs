using PosBox.DAL.Entity_Framework.Table_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PosBox.DAL.Entity_Framework.Table_Models.Enums;

namespace PosBox.DAL.Interfaces
{
    public interface IBusiness : IRepository<Business>
    {
        bool Create(Business obj, out string errorMsg);
        DateTime GetLastLogin(int businessId);
        Business? Authenticate(string BusinessUserName, string password, out string errorMsg);
        IEnumerable<Business> SearchBusiness(string searchTerm);
        bool UpdateBusinessStatus(int Id, UserStatus status);
        bool UpdatePreferences(int Id, Language language, Theme theme);
        Business? GetByBusinessByUserName(string uName);

    }
}
