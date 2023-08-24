using CollegeApp.Dtos.Workshop;
using CollegeApp.Dtos.Sector;
using CollegeApp.Dtos.Director;
using Microsoft.AspNetCore.Mvc;
using CollegeApp.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CollegeApp.Controllers
{
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

        [HttpGet]
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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            PopulateWorkshopsViewBagsAsync();
            
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkshopId,Name,DirectorId,SectorId")] WorkshopCreateDto workshopCreateDto)
        {
            PopulateWorkshopsViewBagsAsync();
            
            if (!ModelState.IsValid) return View(workshopCreateDto);

            await _workshopsService.CreateAsync(workshopCreateDto);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
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
            return View(workshop);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("WorkshopId,Name,DirectorId,SectorId")] WorkshopDto workshopDto)
        {
            if (id != workshopDto.WorkshopId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(workshopDto);

            await _workshopsService.UpdateAsync(id, workshopDto);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
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
