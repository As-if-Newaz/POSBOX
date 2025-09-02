using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PosBox.DAL.Entity_Framework.Table_Models
{
    public class Business
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
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Role { get; set; }


        [Required]
        public bool IsActive { get; set; } //ISDELETED

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string BuName { get; set; } //Business Username

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        public int Cash { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }


        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string UpdatedBy { get; set; }


        public DateTime? UpdatedAt { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string DeletedBy { get; set; }

        public DateTime? DeletedAt { get; set; }


        public virtual ICollection<Customer> Customers { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Sell> Sells { get; set; }

        public virtual ICollection<QuickSell> QuickSells { get; set; }

        public virtual ICollection<Report> Reports { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public Business()
        {
            Customers = new List<Customer>();
            Suppliers = new List<Supplier>();
            Invoices = new List<Invoice>();
            Products = new List<Product>();
            Sells = new List<Sell>();
            QuickSells = new List<QuickSell>();
            Reports = new List<Report>();
            Transactions = new List<Transaction>();
        }
    }
}
