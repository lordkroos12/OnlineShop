namespace OnlineShopUI
{
	public interface ICartRepository
	{
		 Task<int> RemoveItem(int partId);
		Task<ShoppingCart> GetUserCart();
		Task<int> AddItem(int partId, int quantity);
		Task<int> GetShoppingCartCount(string userId = "");
		Task<ShoppingCart> GetCart(string userID);
		 Task<bool> Checkout();
	}
}