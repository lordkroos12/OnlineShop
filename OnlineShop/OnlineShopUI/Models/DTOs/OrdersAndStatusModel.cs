namespace OnlineShopUI.Models.DTOs
{
	public class OrdersAndStatusModel
	{
		public IEnumerable<Order> UserOrders { get; set; }
		public IEnumerable<Order> AllOrders { get; set; }
		public IEnumerable<OrderStatus> Status { get; set; }

        public int orderId { get; set; }
    }
}
