using CollegeApp.Dtos;
using Microsoft.AspNetCore.Mvc;
using CollegeApp.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CollegeApp.Controllers
{
    public class MoldsController : Controller
    {
        private readonly IMoldsService _moldsService;
        private readonly IWorkshopsService _workshopsService;
        private readonly IMoldPurposesService _moldPurposesService;
        
        public MoldsController(IMoldsService moldsService, IWorkshopsService workshopsService, IMoldPurposesService moldPurposesService)
        {
            _moldsService = moldsService;
            _workshopsService = workshopsService;
            _moldPurposesService = moldPurposesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _moldsService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moldDto = await _moldsService.GetOneAsync(id);
            
            if (moldDto == null)
            {
                return NotFound();
            }

            return View(moldDto);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.WorkshopId = new SelectList(await _workshopsService.GetAllAsync(), 
                nameof(WorkshopDto.WorkshopId), 
                nameof(WorkshopDto.Name));
            
            ViewBag.MoldPurposeId = new SelectList(await _moldPurposesService.GetAllAsync(), 
                nameof(MoldPurposeDto.MoldPurposeId), 
                nameof(MoldPurposeDto.PurposeName));
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MoldId,MoldPurposeId,Name,InstallationDate,WorkshopId")] MoldCreateDto createDto)
        {
            if (!ModelState.IsValid) return View(createDto);

            await _moldsService.CreateAsync(createDto);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            ViewBag.WorkshopId = new SelectList(await _workshopsService.GetAllAsync(), 
                nameof(WorkshopDto.WorkshopId), 
                nameof(WorkshopDto.Name));
            
            ViewBag.MoldPurposeId = new SelectList(await _moldPurposesService.GetAllAsync(), 
                nameof(MoldPurposeDto.MoldPurposeId), 
                nameof(MoldPurposeDto.PurposeName));
            
            if (id == null)
            {
                return NotFound();
            }

            var moldDto = await _moldsService.GetOneAsync(id);
            
            if (moldDto == null)
            {
                return NotFound();
            }
            return View(moldDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MoldId,MoldPurposeId,Name,InstallationDate,WorkshopId")] MoldDto moldDto)
        {
            if (id != moldDto.MoldId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(moldDto);

            await _moldsService.UpdateAsync(id, moldDto);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moldDto = await _moldsService.GetOneAsync(id);
            
            if (moldDto == null)
            {
                return NotFound();
            }

            return View(moldDto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var moldDto = await _moldsService.GetOneAsync(id);
            
            if (moldDto != null)
            {
                await _moldsService.DeleteAsync(id);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
