using Application.Services;
using Application.ViewModels;
using Database.Contexts;
using Microsoft.AspNetCore.Mvc;
using StreamingApp.Models;
using System.Diagnostics;

namespace StreamingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly SerieService _serieService;
        private readonly GenreService _genreService;
        private readonly ProducerService _producerService;


        public HomeController(ApplicationContext dbContext)
        {
            _serieService = new(dbContext);
            _genreService = new(dbContext);
            _producerService = new(dbContext);
        }

        public async Task<IActionResult> Index(FilterSerieViewModel vm)
        {

            ViewBag.SerieName = await _serieService.GetAllViewModel();
            ViewBag.Producers = await _producerService.GetAllViewModel();

            return View(await _serieService.GetAllViewModelWithFilters(vm));
        }

    }
}