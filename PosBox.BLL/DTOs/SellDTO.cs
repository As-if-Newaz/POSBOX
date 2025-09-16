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
    public class SellDTO : BaseEntityDTO
    {
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int UnitPrice { get; set; }

        [Required]
        public int NetPrice { get; set; }

        [Required]
        public SellStatus SellStatus { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public virtual SellInvoiceDTO SellInvoice { get; set; }
        [ForeignKey("SellInvoice")]
        [Required]
        public int SellInvoiceId { get; set; }

        public virtual StockDTO Stock { get; set; }
        [ForeignKey("Stock")]
        [Required]
        public int StockId { get; set; }
    }
}
