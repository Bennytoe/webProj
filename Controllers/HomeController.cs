using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using webdb.Database;
using webdb.Database.Models;
using webdb.Models;

namespace webdb.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebDbContext _db;

        public HomeController(WebDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var genres = _db.Genres
                .Select(g => new GenreViewModel { Id = g.Id, Name = g.Name, })
                .ToArray();

			return View(genres);
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
