using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosBox.BLL.DTOs
{
    public abstract class BaseEntityDTO 
    {
        [Required]
        public bool IsDeleted { get; set; }
    }
}
