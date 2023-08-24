using CollegeApp.Dtos;
using Microsoft.AspNetCore.Mvc;
using CollegeApp.Services;

namespace CollegeApp.Controllers
{
    public class RepairmenController : Controller
    {
        private readonly IRepairmenService _repairmenService;

        public RepairmenController(IRepairmenService repairmenService)
        {
            _repairmenService = repairmenService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _repairmenService.GetAllAsync());
        }

        // GET: Repairmen/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repairman = await _repairmenService.GetOneAsync(id);
            
            if (repairman == null)
            {
                return NotFound();
            }

            return View(repairman);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RepairmanId,FullName")] RepairmanCreateDto createDto)
        {
            if (!ModelState.IsValid) return View(createDto);

            await _repairmenService.CreateAsync(createDto);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repairman = await _repairmenService.GetOneAsync(id);
            
            if (repairman == null)
            {
                return NotFound();
            }
            return View(repairman);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RepairmanId,FullName")] RepairmanDto repairmanDto)
        {
            if (id != repairmanDto.RepairmanId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(repairmanDto);
            
            await _repairmenService.UpdateAsync(id, repairmanDto);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repairman = await _repairmenService.GetOneAsync(id);
            
            if (repairman == null)
            {
                return NotFound();
            }

            return View(repairman);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var repairman = await _repairmenService.GetOneAsync(id);
            
            if (repairman != null)
            {
                await _repairmenService.DeleteAsync(id);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
