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
        public BusinessRepository(ApplicationDBContext db) : base(db)
        {
        }
        public Business? Authenticate(string email, string password, out string errorMsg)
        {
            throw new NotImplementedException();
        }

        public DateTime GetLastLogin(int businessId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Business> SearchBusiness(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
