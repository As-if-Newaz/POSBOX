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
            SuperAdmin,
            Admin,
            Manager, 
            Business,
            Unknown
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
            PartiallyPaid,
            Unpaid
        }

        public enum AuditActions
        {
            LoggedIn,
            LoggedOut,
            LoginAttempt,
            Created,
            Deleted,
            Updated,
            Blocked,
            Unblocked,
            Requested,
            Sold,
            Activated,
            Deactivated,
            GaveAccess,
            RemovedAccess,
            Added
        }
        public enum ApprovalStatus
        {
            Pending,
            Approved,
            Rejected,
            Completed,
            Drafted,
            Cancelled
        }

        public enum SellStatus
        {
            Sold,
            Returned,
            Cancelled,
            Draft
        }

        public enum TransactionType
        {
            Deposit,
            Withdrawal
        }

        public enum PaymentMethods
        {
            Cash,
            Bkash,
            Rocket,
            Nagad,
            BankTransfer,
            CardPayment,
            Other
        }

        public enum MakeCountries
        {
            Bangladesh,
            India,
            China,
            UAE,
            Italy,
            Poland,
            Norway,
            Australia,
            Thailand,
            Malaysia,
            Spain,
            Denmark,
            France,
            Philippines,
            Nepal,
            UK,
            USA,
            Switzerland,
            Sweden, 
            Newzealand,
            Germany,
            Japan,
            Korea,
            Other
        }
    }
}
