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
    internal class UserRepository : Repository<User>, IUser
    {
        public UserRepository(ApplicationDBContext db) : base(db)
        {

        }
        public User? Authenticate(string email, string password, out string errorMsg)
        {
            throw new NotImplementedException();
        }

        public bool Create(User obj, out string errorMsg)
        {
            throw new NotImplementedException();
        }

        public User? GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public DateTime GetLastLogin(int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> SearchUsers(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
