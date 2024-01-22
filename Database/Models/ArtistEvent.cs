namespace webdb.Database.Models
{
    public class ArtistEvent
    {
        public int ArtistId { get; set; }
        public Artist Artist { get; set; } = default!;

        public int EventId { get; set; }
        public Event Event { get; set; } = default!;
    }
}
