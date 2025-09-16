using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PosBox.DAL.Entity_Framework.Table_Models.Enums;

namespace PosBox.BLL.DTOs
{
    public class UserPreferencesDTO
    {
        public int UserId { get; set; }
        [Required]
        public Language PreferredLanguage { get; set; }
        [Required]
        public Theme PreferredTheme { get; set; }
    }
}
