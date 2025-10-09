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
    public class ProductDTO
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Barcode { get; set; }

        public MakeCountries? MadeIn { get; set; }

        [Required]
        public int WeightGramML { get; set; }

        [Column(TypeName = "VARCHAR"), StringLength(500)]
        public string? ProductDetails { get; set; }

        [Column(TypeName = "VARCHAR"), StringLength(500)]
        public string? ProductImageUrl { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string? Comment { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public virtual CategoryDTO Category { get; set; }
        [ForeignKey("Category")]
        [Required]
        public int CategoryId { get; set; }
    }
}
