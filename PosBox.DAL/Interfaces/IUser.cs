using PosBox.DAL.Entity_Framework.Table_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosBox.DAL.Interfaces
{
    public interface IUser : IRepository<User>
    {
        bool Create(User obj, out string errorMsg);
        DateTime GetLastLogin(int userId);
        User? Authenticate(string email, string password, out string errorMsg);
        User? GetByEmail(string email);
        IEnumerable<User> SearchUsers(string searchTerm);
    }
}
