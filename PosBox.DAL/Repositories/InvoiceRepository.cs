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
    internal class InvoiceRepository : Repository<Invoice>, IInvoice
    {
        public InvoiceRepository(ApplicationDBContext db) : base(db)
        {
        }
    }
}
