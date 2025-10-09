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
     public class TransferInvoiceDTO : BaseEntityDTO
    {
        public int Id { get; set; }

        [Required]
        public DateTime TransferTime { get; set; }


        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string? Comment { get; set; }

        [Required]
        public int TransferCost { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public ApprovalStatus ApprovalStatus { get; set; }

        public virtual BusinessDTO FromBusiness { get; set; }
        [ForeignKey("FromBusiness")]
        [Required]
        public int FromBusinessId { get; set; }

        public virtual BusinessDTO ToBusiness { get; set; }
        [ForeignKey("ToBusiness")]
        [Required]
        public int ToBusinessId { get; set; }

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

        public virtual ICollection<TransferDTO> Transfers { get; set; }
    }
}
