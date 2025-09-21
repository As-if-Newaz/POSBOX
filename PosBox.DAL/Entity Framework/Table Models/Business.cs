using Microsoft.EntityFrameworkCore;
using PosBox.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static PosBox.DAL.Entity_Framework.Table_Models.Enums;

namespace PosBox.DAL.Entity_Framework.Table_Models
{
    [Index(nameof(BusinessUserName), IsUnique = true)]
    public class Business : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Phone { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Email { get; set; }

        [Column(TypeName = "VARCHAR"), StringLength(500)]
        public string? LogoImageUrl { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string BusinessUserName { get; set; } //Business Username

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(500)]
        public string Password { get; set; }

        [Required]
        public UserStatus BusinessStatus { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string CostCodeString { get; set; }

        [Required]
        public Language PreferredLanguage { get; set; }

        [Required]
        public Theme PreferredTheme { get; set; }


        [Required]
        public int Cash { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? LastLogin { get; set; }


        public virtual ICollection<Customer> Customers { get; set; }

        public virtual ICollection<DailyReport> DailyReports { get; set; }

        public virtual ICollection<EntryInvoice> EntryInvoices { get; set; }

        public virtual ICollection<SellInvoice> SellInvoices { get; set; }

        public virtual ICollection<Stock> Stocks { get; set; }

        public virtual ICollection<StockDiscardApplication> StockDiscardApplications { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public Business()
        {
            Customers = new List<Customer>();
            DailyReports = new List<DailyReport>();
            EntryInvoices = new List<EntryInvoice>();     
            SellInvoices = new List<SellInvoice>();
            Stocks = new List<Stock>();
            StockDiscardApplications = new List<StockDiscardApplication>();
            Suppliers = new List<Supplier>();
            Transactions = new List<Transaction>();
        }
    }
}
