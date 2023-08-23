using CollegeApp.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CollegeApp.Entities;
using CollegeApp.Services;

namespace CollegeApp.Controllers
{
    public class SectorsController : Controller
    {
        private readonly ISectorsService _sectorsService;

        public SectorsController(ISectorsService sectorsService)
        {
            _sectorsService = sectorsService;
        }

        // GET: Sectors
        public async Task<IActionResult> Index()
        {
            return View(await _sectorsService.GetAllAsync());
        }

        // GET: Sectors/Details/5
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

        // GET: Sectors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sectors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SectorId,Name")] SectorCreateDto createDto)
        {
            if (!ModelState.IsValid) return View(createDto);

            await _sectorsService.CreateAsync(createDto);
            
            return RedirectToAction(nameof(Index));
        }

        // GET: Sectors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
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

        // POST: Sectors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SectorId,Name")] SectorDto updateDto)
        {
            if (id != updateDto.SectorId)
            {
                return NotFound();
            }
            
            if (!ModelState.IsValid) return View(updateDto);

            await _sectorsService.UpdateAsync(id ,updateDto);
            
            return RedirectToAction(nameof(Index));
        }

        // GET: Sectors/Delete/5
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

        // POST: Sectors/Delete/5
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
