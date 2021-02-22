using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab1ICTP;

namespace Lab1ICTP.Controllers
{
    public class CareersController : Controller
    {
        private readonly DBGameContext _context;

        public CareersController(DBGameContext context)
        {
            _context = context;
        }

        // GET: Careers
        public async Task<IActionResult> Index()
        {
            var dBGameContext = _context.Careers.Include(c => c.Player).Include(c => c.Position).Include(c => c.Team);
            return View(await dBGameContext.ToListAsync());
        }

        // GET: Careers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var career = await _context.Careers
                .Include(c => c.Player)
                .Include(c => c.Position)
                .Include(c => c.Team)
                .FirstOrDefaultAsync(m => m.CareerId == id);
            if (career == null)
            {
                return NotFound();
            }

            return View(career);
        }

        // GET: Careers/Create
        public IActionResult Create()
        {
            ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "FirstName");
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "Name");
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "Name");
            return View();
        }

        // POST: Careers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CareerId,TeamId,PlayerId,StartDate,EndDate,PositionId")] Career career)
        {
            if (ModelState.IsValid)
            {
                _context.Add(career);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "FirstName", career.PlayerId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "Name", career.PositionId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "Name", career.TeamId);
            return View(career);
        }

        // GET: Careers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var career = await _context.Careers.FindAsync(id);
            if (career == null)
            {
                return NotFound();
            }
            ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "FirstName", career.PlayerId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "Name", career.PositionId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "Name", career.TeamId);
            return View(career);
        }

        // POST: Careers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CareerId,TeamId,PlayerId,StartDate,EndDate,PositionId")] Career career)
        {
            if (id != career.CareerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(career);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CareerExists(career.CareerId))
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
            ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "FirstName", career.PlayerId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "Name", career.PositionId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "Name", career.TeamId);
            return View(career);
        }

        // GET: Careers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var career = await _context.Careers
                .Include(c => c.Player)
                .Include(c => c.Position)
                .Include(c => c.Team)
                .FirstOrDefaultAsync(m => m.CareerId == id);
            if (career == null)
            {
                return NotFound();
            }

            return View(career);
        }

        // POST: Careers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var career = await _context.Careers.FindAsync(id);
            _context.Careers.Remove(career);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CareerExists(int id)
        {
            return _context.Careers.Any(e => e.CareerId == id);
        }
    }
}
