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
    public class ProductDTO
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public int Cost { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string CostCode { get; set; }

        [Required]
        public int Stock { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Barcode { get; set; }

        [Column(TypeName = "VARCHAR"), StringLength(500)]
        public string? ProductImageUrl { get; set; }

        [Required]
        public DateTime AddingDate { get; set; }

        [Required]
        public DateTime ExpireDate { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string? Comment { get; set; }


        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public virtual CategoryDTO Category { get; set; }
        [ForeignKey("Category")]
        [Required]
        public int CategoryId { get; set; }

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
