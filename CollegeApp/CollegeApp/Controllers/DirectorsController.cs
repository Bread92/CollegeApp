using CollegeApp.Dtos;
using Microsoft.AspNetCore.Mvc;
using CollegeApp.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CollegeApp.Controllers
{
    public class DirectorsController : Controller
    {
        private readonly IDirectorsService _directorsService;
        
        public DirectorsController(IDirectorsService directorsService)
        {
            _directorsService = directorsService;
        }

        // GET: Directors
        public async Task<IActionResult> Index()
        {
            return View(await _directorsService.GetAllAsync());
        }

        // GET: Directors/Details/5
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

        // GET: Directors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Directors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DirectorId,FullName")] DirectorCreateDto directorCreateDto)
        {
            if (!ModelState.IsValid) return View(directorCreateDto);

            await _directorsService.CreateAsync(directorCreateDto);
            
            return RedirectToAction(nameof(Index));
        }

        // GET: Directors/Edit/5
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

        // POST: Directors/Edit/5
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

        // GET: Directors/Delete/5
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

        // POST: Directors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _directorsService.DeleteAsync(id);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
