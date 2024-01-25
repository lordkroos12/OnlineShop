using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShopUI.Models
{
    [Table("CartDetail")]
    public class CartInformation
    {
        public int Id { get; set; }
        [Required]
        public int ShoppingCart_Id {get;set;}
        [Required]
        public int Part_Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        public Part Part { get; set; }
        public ShoppingCart ShoppingCart { get; set;}
    }
}
