using CollegeApp.Dtos.Mold;
using CollegeApp.Dtos.Repair;
using CollegeApp.Dtos.Repairman;
using Microsoft.AspNetCore.Mvc;
using CollegeApp.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CollegeApp.Controllers
{
    public class RepairsController : Controller
    {
        private readonly IRepairsService _repairsService;
        private readonly IRepairmenService _repairmenService;
        private readonly IMoldsService _moldsService;

        public RepairsController(IRepairsService repairsService, IRepairmenService repairmenService, IMoldsService moldsService)
        {
            _repairsService = repairsService;
            _repairmenService = repairmenService;
            _moldsService = moldsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _repairsService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repair = await _repairsService.GetOneAsync(id);
            
            if (repair == null)
            {
                return NotFound();
            }

            return View(repair);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            PopulateRepairsViewBagsAsync();
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RepairId,RepairTime,RepairmanId,MoldId,Description")] RepairCreateDto createDto)
        {
            PopulateRepairsViewBagsAsync();
            
            if (!ModelState.IsValid) return View(createDto);

            await _repairsService.CreateAsync(createDto);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            PopulateRepairsViewBagsAsync();
            
            if (id == null)
            {
                return NotFound();
            }

            var repair = await _repairsService.GetOneAsync(id);
            
            if (repair == null)
            {
                return NotFound();
            }
            return View(repair);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RepairId,RepairTime,RepairmanId,MoldId")] RepairDto repairDto)
        {
            if (id != repairDto.RepairId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(repairDto);

            await _repairsService.UpdateAsync(id, repairDto);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repair = await _repairsService.GetOneAsync(id);
            
            if (repair == null)
            {
                return NotFound();
            }

            return View(repair);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var repair = await _repairsService.GetOneAsync(id);
            
            if (repair != null)
            {
                await _repairsService.DeleteAsync(id);
            }
            
            return RedirectToAction(nameof(Index));
        }

        public async void PopulateRepairsViewBagsAsync()
        {
            ViewBag.RepairmanId = new SelectList(await _repairmenService.GetAllAsync(), 
                nameof(RepairmanDto.RepairmanId), 
                nameof(RepairmanDto.FullName));
            
            ViewBag.MoldId = new SelectList(await _moldsService.GetAllAsync(), 
                nameof(MoldDto.MoldId), 
                nameof(MoldDto.Name));
        }
    }
}
