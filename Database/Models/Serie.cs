using Microsoft.Identity.Client;

namespace Database.Models
{
    public class Serie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public int ProducerId { get; set; }
        public int GenreId { get; set; }
        public Producer? Producer { get; set; }
        public Genre? Genre { get; set; }
    }
}
