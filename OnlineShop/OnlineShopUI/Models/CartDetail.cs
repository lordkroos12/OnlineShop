using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShopUI.Models
{
	[Table("CartDetail")]
    public class CartDetail
    {
        public int Id { get; set; }
        [Required]
        public int ShoppingCartId {get;set;}
        [Required]
        public int PartId { get; set; }
        [Required]
        public int Quantity { get; set; }
		[Required]
		public double UnitPrice { get; set; }
		public Part Part { get; set; }
        public ShoppingCart ShoppingCart { get; set;}
    }
}
