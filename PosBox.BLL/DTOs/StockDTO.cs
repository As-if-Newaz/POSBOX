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
    public class StockDTO : BaseEntityDTO
    {
        public int Id { get; set; }

        public virtual ProductDTO Product { get; set; }
        [ForeignKey("Product")]
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string ProductName { get; set; }

        [Required]
        public int Cost { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string CostCode { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public DateTime AddingDate { get; set; }

        [Required]
        public DateTime ExpireDate { get; set; }

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

        public virtual SupplierDTO Supplier { get; set; }
        [ForeignKey("Supplier")]
        [Required]
        public int SupplierId { get; set; }

        public virtual EntryInvoiceDTO EntryInvoice { get; set; }
        [ForeignKey("EntryInvoice")]
        [Required]
        public int EntryInvoiceId { get; set; }
    }
}
