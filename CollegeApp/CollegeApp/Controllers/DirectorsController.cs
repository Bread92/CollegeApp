using CollegeApp.Dtos.Director;
using Microsoft.AspNetCore.Mvc;
using CollegeApp.Services;

namespace CollegeApp.Controllers
{
    public class DirectorsController : Controller
    {
        private readonly IDirectorsService _directorsService;
        
        public DirectorsController(IDirectorsService directorsService)
        {
            _directorsService = directorsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _directorsService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = await _directorsService.GetOneAsync(id);
            
            if (director == null)
            {
                return NotFound();
            }

            return View(director);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DirectorId,FullName")] DirectorCreateDto directorCreateDto)
        {
            if (!ModelState.IsValid) return View(directorCreateDto);

            await _directorsService.CreateAsync(directorCreateDto);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = await _directorsService.GetOneAsync(id);
            
            if (director == null)
            {
                return NotFound();
            }
            return View(director);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DirectorId,FullName")] DirectorDto director)
        {
            if (id != director.DirectorId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(director);
            
            await _directorsService.UpdateAsync(id, director);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var director = await _directorsService.GetOneAsync(id);
            
            if (director == null)
            {
                return NotFound();
            }

            await _directorsService.DeleteAsync(id);
            
            return View(director);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _directorsService.DeleteAsync(id);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
