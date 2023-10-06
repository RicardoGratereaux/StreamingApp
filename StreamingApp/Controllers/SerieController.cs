using Application.Services;
using Application.ViewModels;
using Database.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace StreamingApp.Controllers
{
    public class SerieController : Controller
    {
        private readonly SerieService _serieService;
        private readonly GenreService _genreService;
        private readonly ProducerService _producerService;

        public SerieController(ApplicationContext dbContext)
        {
            _serieService = new(dbContext);
            _genreService = new(dbContext);
            _producerService = new(dbContext);
        }
        public async Task<IActionResult> Index()
        {
            return View(await _serieService.GetAllViewModel());
        }

        public async Task<IActionResult> Create()
        {
            SaveSerieViewModel vm = new();
            vm.GenreList = await _genreService.GetAllViewModel();
            vm.ProducerList = await _producerService.GetAllViewModel();

            return View("SaveSerie", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveSerieViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.GenreList = await _genreService.GetAllViewModel();
                vm.ProducerList = await _producerService.GetAllViewModel();
                return View("SaveSerie", vm);
            }

            await _serieService.Add(vm);
            return RedirectToRoute(new { controller = "Serie", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            SaveSerieViewModel vm = await _serieService.GetByIdSaveViewModel(id);
            vm.GenreList = await _genreService.GetAllViewModel();
            vm.ProducerList = await _producerService.GetAllViewModel();
            return View("SaveSerie", vm);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(SaveSerieViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.GenreList = await _genreService.GetAllViewModel();
                vm.ProducerList = await _producerService.GetAllViewModel();
                return View("SaveSerie", vm);
            }

            await _serieService.Update(vm);
            return RedirectToRoute(new { controller = "Serie", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _serieService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _serieService.Delete(id);
            return RedirectToRoute(new { controller = "Serie", action = "Index" });
        }

        public async Task<IActionResult> VideoPlayer(int id)
        {
            var serie = await _serieService.GetByIdSaveViewModel(id);

            if (serie == null)
            {
                return NotFound();
            }

            return View("VideoPlayer", serie);
        }
    }
}
