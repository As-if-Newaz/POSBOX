using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosBox.DAL.Entity_Framework.Table_Models
{
    public class Supplier : BaseEntity
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

        public int? PaymentDue { get; set; }

      
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string? Remarks { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Phone { get; set; }


        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public virtual Business Business { get; set; }
        [ForeignKey("Business")]
        [Required]
        public int BusinessId { get; set; }

        public virtual ICollection<EntryInvoice> EntryInvoices { get; set; }

        public virtual ICollection<Stock> Stocks { get; set; }

        public Supplier()
        {
            EntryInvoices = new HashSet<EntryInvoice>();
            Stocks = new HashSet<Stock>();
        }

    }
}
