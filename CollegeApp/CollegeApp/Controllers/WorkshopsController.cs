using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CollegeApp.Entities;
using CollegeApp.Services;

namespace CollegeApp.Controllers
{
    public class WorkshopsController : Controller
    {
        private readonly CollegeAppDbContext _context;

        public WorkshopsController(CollegeAppDbContext context)
        {
            _context = context;
        }

        // GET: Workshops
        public async Task<IActionResult> Index()
        {
            var collegeAppDbContext = _context.Workshops.Include(w => w.Director);
            return View(await collegeAppDbContext.ToListAsync());
        }

        // GET: Workshops/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Workshops == null)
            {
                return NotFound();
            }

            var workshop = await _context.Workshops
                .Include(w => w.Director)
                .FirstOrDefaultAsync(m => m.Section == id);
            if (workshop == null)
            {
                return NotFound();
            }

            return View(workshop);
        }

        // GET: Workshops/Create
        public IActionResult Create()
        {
            ViewData["DirectorId"] = new SelectList(_context.Directors, "Id", "Id");
            return View();
        }

        // POST: Workshops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Section,DirectorId")] Workshop workshop)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workshop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DirectorId"] = new SelectList(_context.Directors, "Id", "Id", workshop.DirectorId);
            return View(workshop);
        }

        // GET: Workshops/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Workshops == null)
            {
                return NotFound();
            }

            var workshop = await _context.Workshops.FindAsync(id);
            if (workshop == null)
            {
                return NotFound();
            }
            ViewData["DirectorId"] = new SelectList(_context.Directors, "Id", "Id", workshop.DirectorId);
            return View(workshop);
        }

        // POST: Workshops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Section,DirectorId")] Workshop workshop)
        {
            if (id != workshop.Section)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workshop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkshopExists(workshop.Section))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DirectorId"] = new SelectList(_context.Directors, "Id", "Id", workshop.DirectorId);
            return View(workshop);
        }

        // GET: Workshops/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Workshops == null)
            {
                return NotFound();
            }

            var workshop = await _context.Workshops
                .Include(w => w.Director)
                .FirstOrDefaultAsync(m => m.Section == id);
            if (workshop == null)
            {
                return NotFound();
            }

            return View(workshop);
        }

        // POST: Workshops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Workshops == null)
            {
                return Problem("Entity set 'CollegeAppDbContext.Workshops'  is null.");
            }
            var workshop = await _context.Workshops.FindAsync(id);
            if (workshop != null)
            {
                _context.Workshops.Remove(workshop);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkshopExists(string id)
        {
          return (_context.Workshops?.Any(e => e.Section == id)).GetValueOrDefault();
        }
    }
}
