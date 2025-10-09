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
    public class StockDiscardApplication : BaseEntity
    {
        public int Id { get; set; }

        public virtual Stock Stock { get; set; }
        [ForeignKey("Stock")]
        [Required]
        public int StockId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int NetCost { get; set; }

        [Column(TypeName = "VARCHAR"), StringLength(500)]
        public string? DiscardProductsImageUrl { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Type { get; set; } // return or dump

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public ApprovalStatus Status { get; set; }


        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }


        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual Business Business { get; set; }
        [ForeignKey("Business")]
        [Required]
        public int BusinessId { get; set; }
    }
}
