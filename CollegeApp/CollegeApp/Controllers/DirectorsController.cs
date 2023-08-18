using CollegeApp.Dtos;
using CollegeApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Controllers
{
    public class DirectorsController : Controller
    {
        private readonly CollegeAppDbContext _context;

        private readonly IDirectorService _directorService;

        public DirectorsController(CollegeAppDbContext context, IDirectorService directorService)
        {
            _context = context;
            _directorService = directorService;
        }

        // GET: Directors
        public async Task<IActionResult> Index()
        {
            var directorDtos = await _directorService.GetAllDirectorsAsync();
            return View(directorDtos);
        }

        // GET: Directors/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var dto = await _directorService.GetOneAsync(id);
            
            if (dto == null)
            {
                return NotFound();
            }

            return View(dto);
        }

        // GET: Directors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Directors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] DirectorCreateDto createDto)
        {
            if (ModelState.IsValid)
            {
                await _directorService.CreateDirectorAsync(createDto);

                return RedirectToAction(nameof(Index));
            }

            return View(createDto);
        }

        // GET: Directors/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var dto = await _directorService.GetOneAsync(id);
            
            if (dto == null)
            {
                return NotFound();
            }

            return View(dto);
        }

        // POST: Directors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] DirectorDto dto)
        {
            if (id != dto.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(dto);

            await _directorService.UpdateAsync(dto, id);

            return RedirectToAction(nameof(Index));

        }

        // GET: Directors/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var dto = await _directorService.DeleteOneAsync(id);
            if (dto == null)
            {
                return NotFound();
            }

            return View(dto);
        }

        // POST: Directors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Directors == null)
            {
                return Problem("Entity set 'CollegeAppDbContext.Directors'  is null.");
            }

            var director = await _context.Directors.FindAsync(id);
            if (director != null)
            {
                _context.Directors.Remove(director);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DirectorExists(Guid id)
        {
            return (_context.Directors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}