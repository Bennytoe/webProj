using Microsoft.AspNetCore.Identity;
using webdb.Database.Models;

namespace webdb.Database
{
    public class ApplicationDbContextSeeder
    {
        public async Task SeedAsync(WebDbContext db, IServiceProvider serviceProvider)
        {
            if (db.Genres.Any() || db.Users.Any())
            {
                return;
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var genres = new[]
            {
                new Genre { Name = "Alternative" },
                new Genre { Name = "Blues" },
                new Genre { Name = "Classical" },
                new Genre { Name = "Country" },
                new Genre { Name = "Electronic" },
                new Genre { Name = "Folk" },
                new Genre { Name = "Hip Hop" },
                new Genre { Name = "Jazz" },
                new Genre { Name = "Latin" },
                new Genre { Name = "Metal" },
                new Genre { Name = "Pop" },
                new Genre { Name = "Punk" },
                new Genre { Name = "R&B/Soul" },
                new Genre { Name = "Reggae" },
                new Genre { Name = "Rock" }
            };

            var artists = new[]
            {
                new Artist { Name = "Metallica", Genre = genres[9]  },
                new Artist { Name = "Rage Against The Machine", Genre = genres[0]  },
                new Artist { Name = "Pet", Genre = genres[6]  },
            };

            var events = new[]
            {
                new Event { Name = "Metal Solid", Location = "Sofia, BG", AvailableSpace = 1000, Date = new DateTime(2024, 1, 19, 22, 0, 0) },
                new Event { Name = "Alter", Location = "Los Santos, USA", AvailableSpace = 500, Date = new DateTime(2024, 9, 5, 20, 0, 0) },
                new Event { Name = "Pet", Location = "London, UK", AvailableSpace = 1500, Date = new DateTime(2024, 5, 1, 21, 0, 0) },
            };

            var artistEvents = new[]
            {
                new ArtistEvent { Artist = artists[0], Event = events[0] },
                new ArtistEvent { Artist = artists[1], Event = events[1] },
                new ArtistEvent { Artist = artists[2], Event = events[2] },
            };

            var role = new IdentityRole { Name = "Admin" };
            var user = new IdentityUser { UserName = "test", };
            await roleManager.CreateAsync(role);
            await userManager.CreateAsync(user, "!1Qq123");
            await userManager.AddToRoleAsync(user, "Admin");

            await db.Genres.AddRangeAsync(genres);
            await db.Artists.AddRangeAsync(artists);
            await db.Events.AddRangeAsync(events);
            await db.ArtistsEvents.AddRangeAsync(artistEvents);
            await db.SaveChangesAsync();
        }
    }
}
