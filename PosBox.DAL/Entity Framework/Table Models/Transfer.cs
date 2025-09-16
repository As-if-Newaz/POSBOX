using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosBox.DAL.Entity_Framework.Table_Models
{
    public class Transfer : BaseEntity
    {
        public int Id { get; set; }

        public virtual Stock Stock { get; set; }
        [ForeignKey("Stock")]
        [Required]
        public int StockId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public virtual TransferInvoice TransferInvoice { get; set; }
        [ForeignKey("TransferInvoice")]
        [Required]
        public int TransferInvoiceId { get; set; }

    }
}
