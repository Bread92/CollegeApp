using CollegeApp.Dtos.Mold;
using CollegeApp.Dtos.MoldPurpose;
using CollegeApp.Dtos.Workshop;
using Microsoft.AspNetCore.Mvc;
using CollegeApp.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("Details")]
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
            
            // Displaying Names
            // Workshop
            var workshop = await _workshopsService.GetOneAsync(moldDto.WorkshopId);
            ViewBag.WorkshopName = workshop.Name;
            
            //Purpose
            var purpose = await _moldPurposesService.GetOneAsync(moldDto.MoldPurposeId);
            ViewBag.PurposeName = purpose.PurposeName;
            
            return View(moldDto);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            PopulateMoldsViewBagsAsync();
            
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MoldId,MoldPurposeId,Name,InstallationDate,WorkshopId")] MoldCreateDto createDto)
        {
            PopulateMoldsViewBagsAsync();
            
            if (!ModelState.IsValid) return View(createDto);

            await _moldsService.CreateAsync(createDto);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Edit/{id:guid}")]
        public async Task<IActionResult> Edit([FromRoute] Guid? id)
        {
            PopulateMoldsViewBagsAsync();
            
            if (id == null)
            {
                return NotFound();
            }

            var moldDto = await _moldsService.GetOneAsync(id);
            
            if (moldDto == null)
            {
                return NotFound();
            }

            var updatedDto = new MoldUpdateDto()
            {
                Name = moldDto.Name,
                MoldPurposeId = moldDto.MoldPurposeId,
                WorkshopId = moldDto.WorkshopId
            };
            
            return View(updatedDto);
        }

        [HttpPost("Edit/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [Bind("MoldPurposeId,Name,WorkshopId")] MoldUpdateDto updateDto)
        {
            PopulateMoldsViewBagsAsync();
            
            if (!ModelState.IsValid) return View(updateDto);

            await _moldsService.UpdateAsync(id, updateDto);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Delete")]
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

        public async void PopulateMoldsViewBagsAsync()
        {
            ViewBag.WorkshopId = new SelectList(await _workshopsService.GetAllAsync(), 
                nameof(WorkshopDto.WorkshopId), 
                nameof(WorkshopDto.Name));
            
            ViewBag.MoldPurposeId = new SelectList(await _moldPurposesService.GetAllAsync(), 
                nameof(MoldPurposeDto.MoldPurposeId), 
                nameof(MoldPurposeDto.PurposeName));
        }
    }
}
