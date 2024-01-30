using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShopUI.Controllers
{
	[Authorize]
	public class CartController : Controller
	{
		private readonly ICartRepository _cartRepository;

		public CartController(ICartRepository cartRepository)
        {
			_cartRepository = cartRepository;
		}
        public async Task<IActionResult> GetShoppingCartCount()
		{
			int cartItemsCount = await _cartRepository.GetShoppingCartCount();
			return Ok(cartItemsCount);
		}
		public async Task<IActionResult> AddItem(int partId, int quantity = 1,int redirect = 0)
		{
			var carCount = await _cartRepository.AddItem(partId, quantity);
			if (redirect==0)
			{
				return Ok(carCount);	
			}
			else
				return RedirectToAction("GetUserCart");
		}
		public async Task<IActionResult> RemoveItem(int partId)
		{
			var cartCount = await _cartRepository.RemoveItem(partId);
			return RedirectToAction("GetUserCart");
		}

		public async Task<IActionResult> GetUserCart()
		{
			var cart =await _cartRepository.GetUserCart();
			return View(cart);
		}

		public async Task<IActionResult> Checkout()
		{
			var isFinished = await _cartRepository.Checkout();
			if (!isFinished)
			{
				throw new Exception("Error in Order");
			}
			return RedirectToAction("Index", "Home");
		}	

	}
}
