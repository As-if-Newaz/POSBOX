using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosBox.DAL.Entity_Framework.Table_Models
{
    public class Sell
    {
        public int Id { get; set; }


        public virtual Invoice Invoice { get; set; }
        [ForeignKey("Invoice")]
        [Required]
        public int InvoiceId { get; set; }

        public virtual Product Product { get; set; }
        [ForeignKey("Product")]
        [Required]
        public int ProductId { get; set; }

        public virtual Customer Customer { get; set; }
        [ForeignKey("Customer")]
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int UnitPrice { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

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

        public virtual Business Business { get; set; }
        [ForeignKey("Business")]
        [Required]
        public int BusinessId { get; set; }
    }
}
