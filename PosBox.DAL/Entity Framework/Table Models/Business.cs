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

        public virtual ICollection<Supplier> Suppliers { get; set; }

        public virtual ICollection<SellInvoice> Invoices { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Sell> Sells { get; set; }

        public virtual ICollection<QuickSell> QuickSells { get; set; }

        public virtual ICollection<DailyReport> DailyReports { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public Business()
        {
            Customers = new List<Customer>();
            Suppliers = new List<Supplier>();
            Invoices = new List<SellInvoice>();
            Products = new List<Product>();
            Sells = new List<Sell>();
            QuickSells = new List<QuickSell>();
            DailyReports = new List<DailyReport>();
            Transactions = new List<Transaction>();
        }
    }
}
