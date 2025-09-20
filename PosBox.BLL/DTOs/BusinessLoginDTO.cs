using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosBox.BLL.DTOs
{
    public class BusinessLoginDTO
    {
        [Required(ErrorMessage = "Business Name is Required!")]
        [StringLength(100)]
        public string BusinessUserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is Required!")]
        public string Password { get; set; } = string.Empty;
    }
}
