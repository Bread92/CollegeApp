using CollegeApp.Dtos.MoldPurpose;
using Microsoft.AspNetCore.Mvc;
using CollegeApp.Services;

namespace CollegeApp.Controllers
{
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

        [HttpGet]
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MoldPurposeId,PurposeName")] MoldPurposeCreateDto moldPurposeCreateDto)
        {
            if (!ModelState.IsValid) return View(moldPurposeCreateDto);

            await _moldPurposesService.CreateAsync(moldPurposeCreateDto);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MoldPurposeId,PurposeName")] MoldPurposeDto moldPurpose)
        {
            if (id != moldPurpose.MoldPurposeId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(moldPurpose);

            await _moldPurposesService.UpdateAsync(id, moldPurpose);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var moldPurpose = await _moldPurposesService.GetOneAsync(id);
            
            if (moldPurpose != null)
            {
                await _moldPurposesService.DeleteAsync(id);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
