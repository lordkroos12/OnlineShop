using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.Eventing.Reader;

namespace OnlineShopUI.Repositories
{
	public class HomeRepository : IHomeRepository
	{
		private readonly ApplicationDbContext _db;

		public HomeRepository(ApplicationDbContext db)
        {
			_db = db;
		}
		public async Task<IEnumerable<Category>> GetCategories()
		{ 
			return await _db.Categories.ToListAsync();
		
		}
        public async Task<IEnumerable<Part>> GetParts(string searchTerm="", int categoryId = 0)
		{
			searchTerm = searchTerm.ToLower();
			IEnumerable<Part> parts = await (from part in _db.Parts
						 join category in _db.Categories
						 on part.CategoryId equals category.Id
						 where string.IsNullOrWhiteSpace(searchTerm) || (part != null && part.PartName.ToLower().StartsWith(searchTerm))
						 select new Part
						 {
							 Id = part.Id,
							 PartName = part.PartName,
							 Image = part.Image,
							 ManifacturerName = part.ManifacturerName,
							 CategoryId = part.CategoryId,
							 CategoryName = category.CategoryName,
							 Price = part.Price,	

						 }
						 ).ToListAsync();
			if (categoryId > 0)
			{
				parts =  parts.Where(x=>x.CategoryId == categoryId).ToList();
			}
			return parts;
		}
	}
}
