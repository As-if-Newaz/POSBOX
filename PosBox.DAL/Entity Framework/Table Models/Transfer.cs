using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosBox.DAL.Entity_Framework.Table_Models
{
    public class Transfer
    {
        public int Id { get; set; }

        public virtual Product Product { get; set; }
        [ForeignKey("Product")]
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public DateTime TransferTime { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Comment { get; set; }

        [Required]
        public int TransferCost { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Status { get; set; }

        public virtual Business FromBusiness { get; set; }
        [ForeignKey("FromBusiness")]
        [Required]
        public int FromBusinessId { get; set; }

        public virtual Business ToBusiness { get; set; }
        [ForeignKey("ToBusiness")]
        [Required]
        public int ToBusinessId { get; set; }


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

    }
}
