using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosBox.DAL.Entity_Framework.Table_Models
{
    public class AuditLog
    {
        public int Id { get; set; }

        [Required, Column(TypeName = "VARCHAR"), StringLength(50)]
        public string Action { get; set; }

        [Column(TypeName = "VARCHAR"), StringLength(150)]
        public string? Details { get; set; }

        [Required]
        public DateTime PerformedAt { get; set; }

        [Required]
        public virtual Business PerformedByBusiness { get; set; }
        [ForeignKey("PerformedByBusiness")]
        [Required]
        public int PerformedByBusinessId { get; set; }
    }
}
