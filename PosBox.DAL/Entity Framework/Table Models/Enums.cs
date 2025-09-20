using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosBox.DAL.Entity_Framework.Table_Models
{
    public class Enums
    {
        public enum Role
        {
            SuperAdmin,
            Admin,
            Manager,
            Business
        }
        public enum UserRole
        {
            Admin,
            Manager
        }
        public enum UserStatus
        {
            Active,
            Inactive,
            Blocked
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
            LoginAttempt,
            Created,
            Deleted,
            Updated,
        }
        public enum ApprovalStatus
        {
            Pending,
            Approved,
            Rejected,
            Completed,
            Cancelled
        }

        public enum SellStatus
        {
            Sold,
            Returned,
            Cancelled
        }

        public enum TransactionType
        {
            Deposit,
            Withdrawal
        }
    }
}
