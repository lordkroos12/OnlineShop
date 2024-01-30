using Microsoft.AspNetCore.Mvc;
using OnlineShopUI.Models;
using OnlineShopUI.Models.DTOs;
using System.Diagnostics;

namespace OnlineShopUI.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IHomeRepository _homeRepository;

		public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository)
		{
			_logger = logger;
			_homeRepository = homeRepository;
		}

		public async Task<IActionResult> Index(string searchTerm = "", int categoryId = 0)
		{
			IEnumerable<Part> parts = await _homeRepository.GetParts(searchTerm, categoryId);
			IEnumerable<Category> categories = await _homeRepository.GetCategories();
			PartDisplayModel partDisplayModel = new PartDisplayModel
			{
				Categories = categories,
				Parts = parts,
				SearchTerm = searchTerm,
				CategoryId = categoryId
			};

			return View(partDisplayModel);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
