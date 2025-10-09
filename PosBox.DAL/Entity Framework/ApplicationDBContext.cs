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
        public DbSet<BusinessAccess> BusinessAccesses { get; set; }
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
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure TransferInvoice relationships to prevent cascade delete cycles
            modelBuilder.Entity<TransferInvoice>()
                .HasOne(t => t.FromBusiness)
                .WithMany()
                .HasForeignKey(t => t.FromBusinessId)
                .OnDelete(DeleteBehavior.NoAction);
                
            modelBuilder.Entity<TransferInvoice>()
                .HasOne(t => t.ToBusiness)
                .WithMany()
                .HasForeignKey(t => t.ToBusinessId)
                .OnDelete(DeleteBehavior.NoAction);
                
            // Configure SellInvoice relationships to prevent cascade delete cycles
            modelBuilder.Entity<SellInvoice>()
                .HasOne(s => s.Customer)
                .WithMany()
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);
                
            modelBuilder.Entity<SellInvoice>()
                .HasOne(s => s.Business)
                .WithMany(b => b.SellInvoices)
                .HasForeignKey(s => s.BusinessId)
                .OnDelete(DeleteBehavior.NoAction);
                
            // Configure EntryInvoice relationships to prevent cascade delete cycles
            modelBuilder.Entity<EntryInvoice>()
                .HasOne(e => e.Supplier)
                .WithMany(s => s.EntryInvoices)
                .HasForeignKey(e => e.SupplierId)
                .OnDelete(DeleteBehavior.NoAction);
                
            modelBuilder.Entity<EntryInvoice>()
                .HasOne(e => e.Business)
                .WithMany(b => b.EntryInvoices)
                .HasForeignKey(e => e.BusinessId)
                .OnDelete(DeleteBehavior.NoAction);
                
            // Configure Stock relationships to prevent cascade delete cycles
            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Supplier)
                .WithMany(sup => sup.Stocks)
                .HasForeignKey(s => s.SupplierId)
                .OnDelete(DeleteBehavior.NoAction);
                
            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Business)
                .WithMany(b => b.Stocks)
                .HasForeignKey(s => s.BusinessId)
                .OnDelete(DeleteBehavior.NoAction);
                
            modelBuilder.Entity<Stock>()
                .HasOne(s => s.EntryInvoice)
                .WithMany(e => e.Stocks)
                .HasForeignKey(s => s.EntryInvoiceId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
