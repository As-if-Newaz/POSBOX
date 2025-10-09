using PosBox.DAL.Entity_Framework.Table_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PosBox.DAL.Entity_Framework.Table_Models.Enums;

namespace PosBox.BLL.DTOs
{
    public class BusinessDTO : BaseEntityDTO
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
        
        //// For tracking the Google Drive file ID of the logo
        //[Column(TypeName = "VARCHAR"), StringLength(100)]
        //public string? LogoFileId { get; set; }

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


        public virtual ICollection<CustomerDTO>? Customers { get; set; }

        public virtual ICollection<DailyReportDTO>? DailyReports { get; set; }

        public virtual ICollection<EntryInvoiceDTO>? EntryInvoices { get; set; }

        public virtual  ICollection<SellInvoiceDTO>? SellInvoices { get; set; }

        public virtual ICollection<StockDTO>? Stocks { get; set; }

        public virtual ICollection<StockDiscardApplicationDTO>? StockDiscardApplications { get; set; }

        public virtual ICollection<SupplierDTO>? Suppliers { get; set; }

        public virtual ICollection<TransactionDTO>? Transactions { get; set; }

    }
}
