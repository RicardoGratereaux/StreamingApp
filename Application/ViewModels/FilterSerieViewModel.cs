using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class FilterSerieViewModel
    {
        public string? SerieName {  get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione una productora")]
        public int? ProducerId {  get; set; }
    }
}
