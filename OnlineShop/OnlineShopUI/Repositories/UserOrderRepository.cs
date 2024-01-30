using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Security.Claims;

namespace OnlineShopUI.Repositories
{
	public class UserOrderRepository :IUserOrderRepository
	{
		private ApplicationDbContext _db;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IHttpContextAccessor _contextAccessor;

		public UserOrderRepository(ApplicationDbContext db, UserManager<IdentityUser> userManager, IHttpContextAccessor contextAccessor)
		{
			_db = db;
			_userManager = userManager;
			_contextAccessor = contextAccessor;
		}
		public async Task<IEnumerable<Order>> UserOrder()
		{
			string userId = GetUserId();
			if (userId==null)
			{
				throw new Exception("User is not logged in!");
			}
			IEnumerable<Order> orders =  await _db.Orders
									.Include(x => x.OrderStatus)
									.Include(x=>x.OrderInfo)
									.ThenInclude(x=>x.Part)
									.ThenInclude(x=>x.category)
									.Where(x => x.UserId == userId).ToListAsync();

			return orders;
			
		}
		private string GetUserId()
		{
			ClaimsPrincipal user = _contextAccessor.HttpContext.User;
			var userId = _userManager.GetUserId(user);
			return userId;
		}
		public async Task<IEnumerable<Order>> GetAllOrders()
		{
			IEnumerable<Order> orders = await _db.Orders
										.Include(x => x.OrderStatus)
										.Include(x => x.OrderInfo)
										.ThenInclude(x => x.Part)
										.ThenInclude(x => x.category)
										.ToListAsync();
			return orders;
		}
		public async Task<IEnumerable<OrderStatus>> GetAllStatuses()
		{ 
			return await _db.OrderStatuses.ToListAsync();
		}

		public async Task<bool> EditOrderStatus(int statusId,int orderId)
		{
			if (statusId <= 0)
			{
				throw new Exception("Wrong status");
			}
			var order = await _db.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
			if (order == null)
			{
				throw new Exception("No such order");
			}
			order.OrderStatusId = statusId;
			_db.SaveChanges();
			return true;
		}
	}
}
