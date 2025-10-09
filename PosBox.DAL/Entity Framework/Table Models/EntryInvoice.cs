using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PosBox.DAL.Entity_Framework.Table_Models.Enums;

namespace PosBox.DAL.Entity_Framework.Table_Models
{
    public class EntryInvoice : BaseEntity
    {

        public int Id { get; set; }

        [Required]
        public int NetCost { get; set; }

        public int? PaymentDue { get; set; }

        [Column(TypeName = "VARCHAR"), StringLength(500)]
        public string? InvoiceImageUrl { get; set; }

        [Required]
        public DateTime InvoiceDateTime { get; set; }

        [Required]
        public DateTime? PaymentDueDateTime { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public PaymentStatus Status { get; set; }

        public virtual Supplier Supplier { get; set; }
        [ForeignKey("Supplier")]
        [Required]
        public int SupplierId { get; set; }

        public virtual Business Business { get; set; }
        [ForeignKey("Business")]
        [Required]
        public int BusinessId { get; set; }

        public virtual ICollection<Stock> Stocks { get; set; }

        public EntryInvoice()
        {
            Stocks = new HashSet<Stock>();
        }
    }
}
