using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using webdb.Database.Models;

namespace webdb.Database
{
    public class WebDbContext : IdentityDbContext
    {
        public WebDbContext(DbContextOptions<WebDbContext> options) : base(options)
        {
        }

        public DbSet<Artist> Artists { get; set; } = default!;
        public DbSet<ArtistEvent> ArtistsEvents { get; set; } = default!;
        public DbSet<Event> Events { get; set; } = default!;
        public DbSet<Genre> Genres { get; set; } = default!;
        public DbSet<Reservation> Reservations { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            const int DefaultNameMaxLength = 50;

            modelBuilder.Entity<ArtistEvent>(artistEvent =>
            {
                artistEvent.HasKey(ae => new { ae.ArtistId, ae.EventId });
                artistEvent.HasOne(ae => ae.Artist).WithMany(a => a.ArtistsEvents).HasForeignKey(ae => ae.ArtistId);
                artistEvent.HasOne(ae => ae.Event).WithMany(e => e.ArtistsEvents).HasForeignKey(ae => ae.EventId);
            });

            modelBuilder.Entity<Artist>(artist =>
            {
                artist.Property(a => a.Name).HasMaxLength(DefaultNameMaxLength).IsRequired();
                artist.HasOne(a => a.Genre).WithMany(g => g.Artists).HasForeignKey(a => a.GenreId);
            });
            modelBuilder.Entity<Event>().Property(a => a.Name).HasMaxLength(DefaultNameMaxLength).IsRequired();
            modelBuilder.Entity<Genre>().Property(a => a.Name).HasMaxLength(DefaultNameMaxLength).IsRequired();

            modelBuilder.Entity<Reservation>().HasOne(r => r.Event).WithMany(e => e.Reservations).HasForeignKey(r => r.EventId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
