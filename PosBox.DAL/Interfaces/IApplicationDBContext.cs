using Microsoft.EntityFrameworkCore;
using PosBox.DAL.Entity_Framework.Table_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosBox.DAL.Interfaces
{
    public interface IApplicationDBContext
    {
        DbSet<AuditLog> AuditLogs { get; set; }
        DbSet<Business> Businesses { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<DailyReport> DailyReports { get; set; }
        DbSet<EntryInvoice> EntryInvoices { get; set; }   
        DbSet<Product> Products { get; set; }
        DbSet<QuickSell> QuickSells { get; set; }
        DbSet<Sell> Sells { get; set; }
        DbSet<SellInvoice> SellInvoices { get; set; }
        DbSet<Stock> Stocks { get; set; }
        DbSet<StockDiscardApplication> StockDiscardApplications { get; set; }
        DbSet<Supplier> Suppliers { get; set; }
        DbSet<Transaction> Transactions { get; set; }   
        DbSet<Transfer> Transfers { get; set; }
        DbSet<TransferInvoice> TransferInvoices { get; set; }
        DbSet<User> Users { get; set; }

    }
}
