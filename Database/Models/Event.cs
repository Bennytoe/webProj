namespace webdb.Database.Models
{
    public class Event
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string Location { get; set; } = default!;

        public int AvailableSpace { get; set; }

        public DateTime Date { get; set; }

        public bool Active { get; set; } = true;

        public ICollection<ArtistEvent> ArtistsEvents { get; set; } = new HashSet<ArtistEvent>();

        public ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
    }
}
