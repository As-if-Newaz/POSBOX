using PosBox.DAL.Entity_Framework.Table_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosBox.DAL.Interfaces
{
    public interface IBusiness : IRepository<Business>
    {
        DateTime GetLastLogin(int businessId);
        Business? Authenticate(string email, string password, out string errorMsg);
        IEnumerable<Business> SearchBusiness(string searchTerm);
    }
}
