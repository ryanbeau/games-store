using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sprint.Data;
using Sprint.Enums;
using Sprint.ViewModels;

namespace Sprint.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            DateTime now = DateTime.Now;

            var homeViewModel = new HomeViewModel
            {
                BannerGames = await _context.GameImages
                    .Where(i => i.ImageType == ImageType.Banner)
                    .Include(i => i.Game)
                    .OrderBy(r => Guid.NewGuid())
                    .Take(8)
                    .Select(i => new GameItemViewModel
                    {
                        Discount = i.Game.Discounts.Where(d => d.DiscountPrice < i.Game.RegularPrice && d.DiscountStart <= now && d.DiscountFinish > now)
                            .OrderBy(d => d.DiscountPrice)
                            .FirstOrDefault(),
                        Image = i,
                        Game = i.Game,
                    })
                    .ToListAsync(),

                DiscountedGames = (await _context.GameDiscounts
                    .Include(d => d.Game)
                        .ThenInclude(g => g.GameImages)
                    .Where(d => d.DiscountPrice < d.Game.RegularPrice && d.DiscountStart <= now && d.DiscountFinish > now && d.Game.GameImages.Any(i => i.ImageType == ImageType.Banner))
                    .OrderBy(d => d.DiscountPrice)
                    .ToListAsync())
                    .GroupBy(d => d.GameId)
                    .OrderBy(r => Guid.NewGuid())
                    .Take(6)
                    .Select(g => new GameItemViewModel
                    {
                        Game = g.First().Game,
                        Discount = g.FirstOrDefault(),
                        Image = g.First().Game.GameImages.First(i => i.ImageType == ImageType.Banner),
                    })
                    .ToList(),
            };

            return View(homeViewModel);
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
