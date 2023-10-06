using Application.Services;
using Application.ViewModels;
using Database.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace StreamingApp.Controllers
{
    public class GenreController : Controller
    {
        private readonly GenreService _genreService;
        public GenreController(ApplicationContext dbContext)
        {
            _genreService = new(dbContext);
        }
        public async Task<IActionResult> Index()
        {
            return View(await _genreService.GetAllViewModel());
        }

        public IActionResult Create()
        {
            return View("SaveGenre", new SaveGenreViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveGenreViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveGenre", vm);
            }

            await _genreService.Add(vm);
            return RedirectToRoute(new { controller = "Genre", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View("SaveGenre", await _genreService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveGenreViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveGenre", vm);
            }

            await _genreService.Update(vm);
            return RedirectToRoute(new { controller = "Genre", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _genreService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _genreService.Delete(id);
            return RedirectToRoute(new { controller = "Genre", action = "Index" });
        }
    }
}
