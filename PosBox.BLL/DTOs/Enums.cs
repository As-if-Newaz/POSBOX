using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosBox.DAL.Entity_Framework.Table_Models
{
    public class Enums
    {
        public enum UserRole
        {
            Admin,
            Manager
        }
        public enum Language
        {
            English,
            Bangla
        }
        public enum Theme
        {
            Light,
            Dark
        }
        public enum PaymentStatus
        {
            Paid,
            Partial,
            Unpaid
        }

        public enum AuditActions
        {
            LoggedIn,
            Created,
            Deleted,
            Updated,

        }
    }
}
