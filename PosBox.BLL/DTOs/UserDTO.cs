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
    public class UserDTO : BaseEntityDTO
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Phone { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        public UserRole UserRole { get; set; }

        [Required]
        public UserStatus UserStatus { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public Language? PreferredLanguage { get; set; }

        [Required]
        public Theme? PreferredTheme { get; set; }


        [Required]
        public DateTime CreatedAt { get; set; }

        public string? EmailVerificationCode { get; set; }
        public DateTime? EmailVerificationExpiry { get; set; }

        public DateTime? LastLogin { get; set; }
    }
}
