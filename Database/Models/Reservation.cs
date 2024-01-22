namespace webdb.Database.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; } = default!;
    }
}
