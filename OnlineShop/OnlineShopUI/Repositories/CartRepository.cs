using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace OnlineShopUI.Repositories
{
	public class CartRepository : ICartRepository
	{
		private ApplicationDbContext _db;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IHttpContextAccessor _contextAccessor;

		public CartRepository(ApplicationDbContext db, UserManager<IdentityUser> userManager, IHttpContextAccessor contextAccessor)
		{
			_db = db;
			_userManager = userManager;
			_contextAccessor = contextAccessor;
		}
		public async Task<int> RemoveItem(int partId)
		{
			string userId = GetUserId();
			try
			{
				if (string.IsNullOrEmpty(userId))
				{
					throw new Exception("UserId is null");
				}
				var cart = await GetCart(userId);
				if (cart is null)
				{
					new Exception("No such cart");
				}
				var cartItem = _db.CartInformations.FirstOrDefault(x => x.PartId == partId && x.ShoppingCartId == cart.Id);
				if (cartItem == null)
				{
					new Exception("No items in cart");
				}
				else if (cartItem.Quantity == 1)
				{
					_db.CartInformations.Remove(cartItem);
				}
				else
				{
					cartItem.Quantity--;
				}
				_db.SaveChanges();


			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			var cartTotalItems = await GetShoppingCartCount(userId);
			return cartTotalItems;

		}

		public async Task<ShoppingCart> GetUserCart()
		{
			var userId = GetUserId();
			if (userId == null)
			{
				throw new Exception("Invalid userId");
			}
			var shoppingCart = await _db.ShoppingCarts.Include(a => a.CartInformations)
								.ThenInclude(a => a.Part)
								.ThenInclude(a => a.category)
								.Where(x => x.UserId == userId).FirstOrDefaultAsync();
			return shoppingCart;

		}
		public async Task<int> AddItem(int partId, int quantity)
		{
			string userId = GetUserId();
			using var transaction = _db.Database.BeginTransaction();
			try
			{
				if (string.IsNullOrEmpty(userId))
					throw new Exception("user is not logged-in");
				var cart = await GetCart(userId);
				if (cart is null)
				{
					cart = new ShoppingCart
					{
						UserId = userId
					};
					_db.ShoppingCarts.Add(cart);
				}
				_db.SaveChanges();
				// cart detail section
				var cartItem = _db.CartInformations
								  .FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.PartId == partId);
				if (cartItem is not null)
				{
					cartItem.Quantity += quantity;
				}
				else
				{
					var part = _db.Parts.Find(partId);
					cartItem = new CartDetail
					{
						PartId = partId,
						ShoppingCartId = cart.Id,
						Quantity = quantity,
						UnitPrice = part.Price
					};
					_db.CartInformations.Add(cartItem);
				}
				_db.SaveChanges();
				transaction.Commit();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			var cartItemCount = await GetShoppingCartCount(userId);
			return cartItemCount;
		}

		public async Task<int> GetShoppingCartCount(string userId = "")
		{
			if (string.IsNullOrEmpty(userId))
			{
				userId = GetUserId();
			}
			var cartCount = await (from shoppingCarts in _db.ShoppingCarts
								   join cartInformation in _db.CartInformations
								   on shoppingCarts.Id equals cartInformation.ShoppingCartId
								   where shoppingCarts.UserId == userId
								   select new { cartInformation.Id }
								   ).ToListAsync();
			return cartCount.Count();
		}

		public async Task<bool> Checkout()
		{ 
			using var transaction = _db.Database.BeginTransaction();
			try
			{
				string userId = GetUserId();
				if (userId == null)
				{
					throw new Exception("User is not logged in!");
				}
				var cart = await GetCart(userId);
				if (cart is null)
				{
					throw new Exception("Invalid cart");
				}
				var cartDetail = _db.CartInformations.Where(x => x.ShoppingCartId == cart.Id).ToList();
				if (cartDetail.Count == 0)
				{
					throw new Exception("Cart is empty");
				}
				var Order = new Order
				{
					UserId = userId,
					DateOfCreation = DateTime.UtcNow,
					OrderStatusId=1
				};
				_db.Orders.Add(Order);
				_db.SaveChanges();
				foreach (var item in cartDetail)
				{
					var orderDetail = new OrderInformation
					{
						PartId = item.PartId,
						OrderId = Order.Id,
						Quantity = item.Quantity,
						UnitPrice = item.UnitPrice
					};
					_db.OrderInformations.Add(orderDetail);
				}
				_db.SaveChanges();
				_db.CartInformations.RemoveRange(cartDetail);
				_db.SaveChanges();
				transaction.Commit();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
			

		}
		public async Task<ShoppingCart> GetCart(string userID)
		{
			var cart = await _db.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userID);
			return cart;
		}

		private string GetUserId()
		{
			ClaimsPrincipal user = _contextAccessor.HttpContext.User;
			var userId = _userManager.GetUserId(user);
			return userId;
		}
	}
}
