using PosBox.DAL.Entity_Framework;
using PosBox.DAL.Entity_Framework.Table_Models;
using PosBox.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PosBox.DAL.Repositories
{
    internal class TransactionRepository : Repository<PosBox.DAL.Entity_Framework.Table_Models.Transaction>, ITransaction
    {
        public TransactionRepository(ApplicationDBContext db) : base(db)
        {
        }
    }
}
