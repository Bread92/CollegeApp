using CollegeApp.Dtos.MoldPurpose;
using Microsoft.AspNetCore.Mvc;
using CollegeApp.Services;

namespace CollegeApp.Controllers
{
    [Route("[controller]")]
    public class MoldPurposesController : Controller
    {
        private readonly IMoldPurposesService _moldPurposesService;

        public MoldPurposesController(IMoldPurposesService moldPurposesService)
        {
            _moldPurposesService = moldPurposesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _moldPurposesService.GetAllAsync());
        }

        [HttpGet("Details")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moldPurpose = await _moldPurposesService.GetOneAsync(id);
            
            if (moldPurpose == null)
            {
                return NotFound();
            }

            return View(moldPurpose);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MoldPurposeId,PurposeName")] MoldPurposeCreateDto moldPurposeCreateDto)
        {
            if (!ModelState.IsValid) return View(moldPurposeCreateDto);

            await _moldPurposesService.CreateAsync(moldPurposeCreateDto);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Edit/{id:guid}")]
        public async Task<IActionResult> Edit([FromRoute] Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moldPurpose = await _moldPurposesService.GetOneAsync(id);
            
            if (moldPurpose == null)
            {
                return NotFound();
            }

            var updatedMoldPurpose = new MoldPurposeUpdateDto()
            {
                PurposeName = moldPurpose.PurposeName
            };
            
            return View(updatedMoldPurpose);
        }

        [HttpPost("Edit/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [Bind("MoldPurposeId,PurposeName")] MoldPurposeUpdateDto updateDto)
        {
            if (!ModelState.IsValid) return View(updateDto);

            await _moldPurposesService.UpdateAsync(id, updateDto);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moldPurpose = await _moldPurposesService.GetOneAsync(id);
            
            if (moldPurpose == null)
            {
                return NotFound();
            }

            return View(moldPurpose);
        }

        [HttpPost("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _moldPurposesService.DeleteAsync(id);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
