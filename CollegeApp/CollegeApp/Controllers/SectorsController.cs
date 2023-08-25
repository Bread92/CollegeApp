using CollegeApp.Dtos;
using CollegeApp.Dtos.Sector;
using Microsoft.AspNetCore.Mvc;
using CollegeApp.Services;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    public class SectorsController : Controller
    {
        private readonly ISectorsService _sectorsService;

        public SectorsController(ISectorsService sectorsService)
        {
            _sectorsService = sectorsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _sectorsService.GetAllAsync());
        }

        [HttpGet("Details")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sector = await _sectorsService.GetOneAsync(id);
            
            if (sector == null)
            {
                return NotFound();
            }

            return View(sector);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SectorId,Name")] SectorCreateDto createDto)
        {
            if (!ModelState.IsValid) return View(createDto);

            await _sectorsService.CreateAsync(createDto);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Edit/{id:guid}")]
        public async Task<IActionResult> Edit([FromRoute] Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sectorDto = await _sectorsService.GetOneAsync(id);
            
            if (sectorDto == null)
            {
                return NotFound();
            }

            var updatedSector = new SectorUpdateDto()
            {
                Name = sectorDto.Name
            };
            
            return View(updatedSector);
        }
        
        [HttpPost("Edit/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [Bind("Name")] SectorUpdateDto updateDto)
        {
            if (!ModelState.IsValid) return View(updateDto);

            await _sectorsService.UpdateAsync(id ,updateDto);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sectorDto = await _sectorsService.GetOneAsync(id);
            
            if (sectorDto == null)
            {
                return NotFound();
            }

            return View(sectorDto);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var sector = await _sectorsService.GetOneAsync(id);

            if (sector != null)
            {
                await _sectorsService.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
