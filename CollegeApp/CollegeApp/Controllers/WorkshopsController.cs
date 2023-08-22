using CollegeApp.Dtos;
using Microsoft.AspNetCore.Mvc;
using CollegeApp.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CollegeApp.Controllers
{
    public class WorkshopsController : Controller
    {
        private readonly IWorkshopsService _workshopsService;
        private readonly IDirectorsService _directorsService;

        public WorkshopsController(IWorkshopsService workshopsService, IDirectorsService directorsService)
        {
            _workshopsService = workshopsService;
            _directorsService = directorsService;
        }

        // GET: Workshops
        public async Task<IActionResult> Index()
        {
            return View(await _workshopsService.GetAllAsync());
        }

        // GET: Workshops/Details/5
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

        // GET: Workshops/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.DirectorId = new SelectList(await _directorsService.GetAllAsync(), 
                nameof(DirectorDto.DirectorId), 
                nameof(DirectorDto.FullName));
            
            return View();
        }

        // POST: Workshops/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkshopId,Name,DirectorId,SectorId")] WorkshopCreateDto workshopCreateDto)
        {
            if (!ModelState.IsValid) return View(workshopCreateDto);

            await _workshopsService.CreateAsync(workshopCreateDto);
            
            return RedirectToAction(nameof(Index));
        }

        // GET: Workshops/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
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

        // POST: Workshops/Edit/5
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

        // GET: Workshops/Delete/5
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

        // POST: Workshops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _workshopsService.DeleteAsync(id);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
