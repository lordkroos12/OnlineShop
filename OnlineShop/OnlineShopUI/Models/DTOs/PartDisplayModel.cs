namespace OnlineShopUI.Models.DTOs
{
	public class PartDisplayModel
	{
        public IEnumerable<Part> Parts { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        public string SearchTerm { get; set; } = "";
        public int CategoryId { get; set; } = 0;
    }
}
