namespace webdb.Database.Models
{
    public class Artist
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public bool Active { get; set; } = true;

        public int GenreId { get; set; }
        public Genre Genre { get; set; } = default!;

        public ICollection<ArtistEvent> ArtistsEvents { get; set; } = new HashSet<ArtistEvent>();
    }
}
