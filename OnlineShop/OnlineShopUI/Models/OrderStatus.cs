using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShopUI.Models
{
    [Table("OrderStatus")]
    public class OrderStatus
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        
        public string? Status { get; set; }

        [Required]
        public int StatusId { get; set; }

    }
}
