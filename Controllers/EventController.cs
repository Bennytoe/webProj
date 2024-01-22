using Microsoft.AspNetCore.Mvc;
using webdb.Database;

namespace webdb.Controllers
{
    public class EventController : Controller
    {
        private readonly WebDbContext _db;

        public EventController(WebDbContext db)
        {
            _db = db;
        }

        public class ArtistViewModel
        {
            public string Name { get; set; } = default!;
            public string Genre { get; set; } = default!;
        }

        public class EventViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; } = default!;
            public string Location { get; set; } = default!;
            public int AvailableSpace { get; set; }
            public string Date { get; set; } = default!;
            public ArtistViewModel[] Artists { get; set; } = default!;
        }

        [HttpGet]
        public EventViewModel[] GetList(int genreId = 0, string searchText = "")
        {
            return _db.Events
                .OrderBy(e => e.Date).ThenBy(e => e.Location)
                .Where(@event =>
                    (@event.ArtistsEvents.Any(ae => ae.Artist.GenreId == genreId) || genreId == 0) &&
                    (@event.ArtistsEvents.Any(ae => ae.Artist.Name.Contains(searchText)) || searchText == null)
                )
                .Select(e => new EventViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Location = e.Location,
                    AvailableSpace = e.AvailableSpace,
                    Date = e.Date.ToString("yyyy:MM:dd HH:mm:ss"),
                    Artists = e.ArtistsEvents.Select(ae => ae.Artist)
                        .Select(a => new ArtistViewModel
                        {
                            Name = a.Name,
                            Genre = a.Genre.Name 
                        }).ToArray()
                })
                .ToArray();
        }
    }
}
