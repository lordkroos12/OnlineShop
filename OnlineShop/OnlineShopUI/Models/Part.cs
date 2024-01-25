using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShopUI.Models
{
    [Table("Part")]
    public class Part
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string? PartName { get; set; }
        
        public string Image { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }

        [Required]
        public Category? category { get; set; }

        public List<OrderInformation> OrderInfo { get; set; }
        public List<CartInformation> CartInfo { get; set; }
    }
}
