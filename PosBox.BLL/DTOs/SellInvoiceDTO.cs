using PosBox.DAL.Entity_Framework.Table_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosBox.BLL.DTOs
{
    public class SellInvoiceDTO : BaseEntityDTO
    {
        public int Id { get; set; }

        [Required]
        public int GrossAmount { get; set; }

        [Required]
        public int NetAmount { get; set; }

        public int? DiscountTk { get; set; }

        public int? Due { get; set; }

        [Required]
        public DateTime InvoiceDateTime { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string PaymentMethod { get; set; }


        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string? Comment { get; set; }

        [Required]
        public int Cost { get; set; }

        [Required]
        public int Profit { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Status { get; set; }

        [Required]
        public bool HasQuickSell { get; set; }

        [Column(TypeName = "VARCHAR"), StringLength(500)]
        public string? QuickSellInvoiceImageUrl { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public virtual CustomerDTO Customer { get; set; }
        [ForeignKey("Customer")]
        [Required]
        public int CustomerId { get; set; }

        public virtual BusinessDTO Business { get; set; }
        [ForeignKey("Business")]
        [Required]
        public int BusinessId { get; set; }

        public virtual ICollection<SellDTO> Sells { get; set; }
        public virtual ICollection<QuickSellDTO> QuickSells { get; set; }
    }
}
