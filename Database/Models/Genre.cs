namespace webdb.Database.Models
{
    public class Genre
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public bool Active { get; set; } = true;

        public ICollection<Artist> Artists { get; set; } = new HashSet<Artist>();
    }
}
