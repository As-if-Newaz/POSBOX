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
    public class QuickSellDTO : BaseEntityDTO
    {
        public int Id { get; set; }

        public virtual SellInvoiceDTO Invoice { get; set; }
        [ForeignKey("Invoice")]
        [Required]
        public int InvoiceId { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string ProductName { get; set; }

        public virtual CustomerDTO Customer { get; set; }
        [ForeignKey("Customer")]
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int UnitPrice { get; set; }


        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public virtual BusinessDTO Business { get; set; }
        [ForeignKey("Business")]
        [Required]
        public int BusinessId { get; set; }
    }
}
