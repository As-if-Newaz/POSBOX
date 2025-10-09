using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosBox.DAL.Entity_Framework.Table_Models
{
    public abstract class BaseEntity
    {
        [Required]
        public bool IsDeleted { get; set; }
    }
}
