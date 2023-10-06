namespace Database.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Serie> Series { get; set; }

    }
}
