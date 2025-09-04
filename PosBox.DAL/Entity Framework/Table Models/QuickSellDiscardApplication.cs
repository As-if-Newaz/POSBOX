using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosBox.DAL.Entity_Framework.Table_Models
{
    public class QuickSellDiscardApplication
    {
        public int Id { get; set; }

        public virtual QuickSell QuickSell { get; set; }
        [ForeignKey("QuickSell")]
        [Required]
        public int QuickSellId { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Type { get; set; } // return or dump

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Status { get; set; }

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

        public virtual Business Business { get; set; }
        [ForeignKey("Business")]
        [Required]
        public int BusinessId { get; set; }
    }
}
