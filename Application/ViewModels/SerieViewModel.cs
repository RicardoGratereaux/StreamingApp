using Database.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class SerieViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public string GenreName { get; set; }
        public string ProducerName { get; set; }
        public string SerieName { get; set; }
        public int GenreId {  get; set; }
        public int ProducerId { get; set; }
    }
}
