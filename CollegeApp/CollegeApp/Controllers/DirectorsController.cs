using CollegeApp.Dtos.Director;
using Microsoft.AspNetCore.Mvc;
using CollegeApp.Services;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("Details")]
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

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DirectorId,FullName")] DirectorCreateDto directorCreateDto)
        {
            if (!ModelState.IsValid) return View(directorCreateDto);

            await _directorsService.CreateAsync(directorCreateDto);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Edit/{id:guid}")]
        public async Task<IActionResult> Edit([FromRoute] Guid id)
        {
            var director = await _directorsService.GetOneAsync(id);
            
            if (director == null)
            {
                return NotFound();
            }

            var updatedDto = new DirectorUpdateDto()
            {
                FullName = director.FullName
            };
            
            return View(updatedDto);
        }

        [HttpPost("Edit/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [Bind("FullName")] DirectorUpdateDto updateDto)
        {
            if (!ModelState.IsValid) return View(updateDto);
            
            await _directorsService.UpdateAsync(id, updateDto);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Delete")]
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

        [HttpPost("Delete"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _directorsService.DeleteAsync(id);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
