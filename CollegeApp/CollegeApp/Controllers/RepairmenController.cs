using CollegeApp.Dtos.Repairman;
using Microsoft.AspNetCore.Mvc;
using CollegeApp.Services;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("Details")]
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

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RepairmanId,FullName")] RepairmanCreateDto createDto)
        {
            if (!ModelState.IsValid) return View(createDto);

            await _repairmenService.CreateAsync(createDto);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Edit/{id:guid}")]
        public async Task<IActionResult> Edit([FromRoute] Guid? id)
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

            var updatedRepairman = new RepairmanUpdateDto()
            {
                FullName = repairman.FullName
            };
            
            return View(updatedRepairman);
        }

        [HttpPost("Edit/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [Bind("FullName")] RepairmanUpdateDto updateDto)
        {
            if (!ModelState.IsValid) return View(updateDto);
            
            await _repairmenService.UpdateAsync(id, updateDto);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Delete")]
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
