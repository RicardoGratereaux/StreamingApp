using Application.Services;
using Application.ViewModels;
using Database.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace StreamingApp.Controllers
{
    public class ProducerController : Controller
    {
        private readonly ProducerService _producerService;
        public ProducerController(ApplicationContext dbContext)
        {
            _producerService = new(dbContext);
        }
        public async Task<IActionResult> Index()
        {
            return View(await _producerService.GetAllViewModel());
        }

        public IActionResult Create()
        {
            return View("SaveProducer", new SaveProducerViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveProducerViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveProducer", vm);
            }

            await _producerService.Add(vm);
            return RedirectToRoute(new { controller = "Producer", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View("SaveProducer", await _producerService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveProducerViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveProducer", vm);
            }

            await _producerService.Update(vm);
            return RedirectToRoute(new { controller = "Producer", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _producerService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _producerService.Delete(id);
            return RedirectToRoute(new { controller = "Producer", action = "Index" });
        }
    }
}
