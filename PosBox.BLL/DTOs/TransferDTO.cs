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
    public class TransferDTO : BaseEntityDTO
    {
        public int Id { get; set; }

        public virtual StockDTO Stock { get; set; }
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

        public virtual TransferInvoiceDTO TransferInvoice { get; set; }
        [ForeignKey("TransferInvoice")]
        [Required]
        public int TransferInvoiceId { get; set; }
    }
}
