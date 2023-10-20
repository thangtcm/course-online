using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace IIG_School.Models
{
    public class Product : IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [DisplayName("Tên sản phẩm")]
        public string? ProductName { get; set; }

        [Required]
        [DisplayName("Mô tả sản phẩm")]
        public string? ProductDescription { get; set; }


        [Required]
        [DisplayName("Giá sản phẩm")]
        public double ProductPrice { get; set; }

        [DisplayName("Giảm giá sản phẩm")]
        public double ProductSale { get; set; }

        [Required]
        [DisplayName("Số lượng tồn kho")]
        public int Inventory { get; set; }

        [DisplayName("Name Image")]
        public string? ImageName { get; set; }
        [DisplayName("Upload File")]
        public string? ImagePath { get; set; }
    }
}
