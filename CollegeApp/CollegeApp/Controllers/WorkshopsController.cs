using CollegeApp.Dtos.Workshop;
using CollegeApp.Dtos.Sector;
using CollegeApp.Dtos.Director;
using Microsoft.AspNetCore.Mvc;
using CollegeApp.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    public class WorkshopsController : Controller
    {
        private readonly IWorkshopsService _workshopsService;
        private readonly IDirectorsService _directorsService;
        private readonly ISectorsService _sectorsService;

        public WorkshopsController(IWorkshopsService workshopsService, IDirectorsService directorsService, ISectorsService sectorsService)
        {
            _workshopsService = workshopsService;
            _directorsService = directorsService;
            _sectorsService = sectorsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _workshopsService.GetAllAsync());
        }

        [HttpGet("Details")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workshop = await _workshopsService.GetOneAsync(id);
            
            if (workshop == null)
            {
                return NotFound();
            }

            return View(workshop);
        }

        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            PopulateWorkshopsViewBagsAsync();
            
            return View();
        }
        
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkshopId,Name,DirectorId,SectorId")] WorkshopCreateDto workshopCreateDto)
        {
            PopulateWorkshopsViewBagsAsync();
            
            if (!ModelState.IsValid) return View(workshopCreateDto);

            await _workshopsService.CreateAsync(workshopCreateDto);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Edit/{id:guid}")]
        public async Task<IActionResult> Edit([FromRoute] Guid? id)
        {
            PopulateWorkshopsViewBagsAsync();
            
            if (id == null)
            {
                return NotFound();
            }

            var workshop = await _workshopsService.GetOneAsync(id);
            
            if (workshop == null)
            {
                return NotFound();
            }

            var updatedWorkshop = new WorkshopUpdateDto()
            {
                Name = workshop.Name,
                DirectorId = workshop.DirectorId,
                SectorId = workshop.SectorId
            };
            
            return View(updatedWorkshop);
        }
        
        [HttpPost("Edit/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [Bind("Name,DirectorId,SectorId")] WorkshopUpdateDto updateDto)
        {
            PopulateWorkshopsViewBagsAsync();
            
            if (!ModelState.IsValid) return View(updateDto);

            await _workshopsService.UpdateAsync(id, updateDto);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workshop = await _workshopsService.GetOneAsync(id);
            
            if (workshop == null)
            {
                return NotFound();
            }

            return View(workshop);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _workshopsService.DeleteAsync(id);
            
            return RedirectToAction(nameof(Index));
        }

        public async void PopulateWorkshopsViewBagsAsync()
        {
            ViewBag.DirectorId = new SelectList(await _directorsService.GetAllAsync(), 
                nameof(DirectorDto.DirectorId), 
                nameof(DirectorDto.FullName));
            
            ViewBag.SectorId = new SelectList(await _sectorsService.GetAllAsync(), 
                nameof(SectorDto.SectorId), 
                nameof(SectorDto.Name));
        }
    }
}
