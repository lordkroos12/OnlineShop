using System.Collections;
using System.Collections.Generic;

namespace OnlineShopUI.Repositories
{
	public interface IUserOrderRepository
	{
		Task<IEnumerable<Order>> UserOrder();
		Task<IEnumerable<Order>> GetAllOrders();
		Task<IEnumerable<OrderStatus>> GetAllStatuses();
		Task<bool> EditOrderStatus(int statusId, int orderId);

	}
}