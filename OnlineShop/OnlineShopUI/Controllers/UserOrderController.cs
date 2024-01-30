using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShopUI.Controllers
{
	[Authorize]
	public class UserOrderController : Controller
	{
		private readonly IUserOrderRepository _userOrderRepository;
		private readonly ApplicationDbContext _applicationDbContext;

		public UserOrderController(ApplicationDbContext applicationDbContext, IUserOrderRepository userOrderRepository)
        {
			_applicationDbContext = applicationDbContext;
			_userOrderRepository = userOrderRepository;
		}
        public async Task<IActionResult> UserOrders()
		{
			IEnumerable<OrderStatus> orderStatuses = await _userOrderRepository.GetAllStatuses();
			IEnumerable<Order> userOrders = await _userOrderRepository.UserOrder();
			IEnumerable<Order> allOrders = await _userOrderRepository.GetAllOrders();
			OrdersAndStatusModel ordersAndStatusModel = new OrdersAndStatusModel
			{
				UserOrders = userOrders,
				AllOrders = allOrders,
				Status = orderStatuses

			};
			return View(ordersAndStatusModel);
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> EditStatus(int orderId = 0,int statusId = 0)
		{

			var s = await _userOrderRepository.EditOrderStatus(statusId,orderId);
			return RedirectToAction("UserOrders", "UserOrder");
		
		}
	}
}
