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
    public class AuditLog : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        public AuditActions Action { get; set; }

        [Column(TypeName = "VARCHAR"), StringLength(150)]
        public string? Details { get; set; }


        [Required]
        public DateTime PerformedAt { get; set; }

        [Required]
        public UserRole PerformedByRole { get; set; }

        [Required]
        public int PerformedById { get; set; }
    }
}
