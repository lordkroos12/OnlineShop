namespace OnlineShopUI
{
	public interface IHomeRepository
	{
		Task<IEnumerable<Category>> GetCategories();
		Task<IEnumerable<Part>> GetParts(string searchTerm = "", int categoryId = 0);
	}
}