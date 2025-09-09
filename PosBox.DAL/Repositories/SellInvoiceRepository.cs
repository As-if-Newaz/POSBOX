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
    internal class SellInvoiceRepository : Repository<SellInvoice>, ISellInvoice
    {
        public SellInvoiceRepository(ApplicationDBContext db) : base(db)
        {
        }
    }
}
