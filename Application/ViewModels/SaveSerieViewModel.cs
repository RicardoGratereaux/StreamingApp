using Database.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class SaveSerieViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe colocar el nombre de la serie")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Debe colocar la url de la imagen")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Debe colocar la url del video")]
        public string VideoUrl { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debe colocar el genero de la serie")]
        public int GenreId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debe colocar la productora de la serie")]
        public int ProducerId { get; set; }

        public List<GenreViewModel>? GenreList { get; set; }
        public List<ProducerViewModel>? ProducerList { get; set; }
    }
}
