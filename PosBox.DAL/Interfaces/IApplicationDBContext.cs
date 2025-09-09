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
        DbSet<SellInvoice> Invoices { get; set; }   
        DbSet<Product> Products { get; set; }
        DbSet<ProductDiscardApplication> ProductDiscardApplications { get; set; }
        DbSet<QuickSell> QuickSells { get; set; }
        DbSet<Sell> Sells { get; set; }
        DbSet<Supplier> Suppliers { get; set; }
        DbSet<Transaction> Transactions { get; set; }   
        DbSet<Transfer> Transfers { get; set; }
        DbSet<User> Users { get; set; }

    }
}
