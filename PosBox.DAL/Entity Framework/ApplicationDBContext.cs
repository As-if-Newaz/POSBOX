using Microsoft.EntityFrameworkCore;
using PosBox.DAL.Entity_Framework.Table_Models;
using PosBox.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosBox.DAL.Entity_Framework
{
    public class ApplicationDBContext : DbContext, IApplicationDBContext
    {
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<DailyReport> DailyReports { get; set; }
        public DbSet<EntryInvoice> EntryInvoices { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<QuickSell> QuickSells { get; set; }
        public DbSet<Sell> Sells { get; set; }
        public DbSet<SellInvoice> SellInvoices { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockDiscardApplication> StockDiscardApplications { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<TransferInvoice> TransferInvoices { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
    }
}
