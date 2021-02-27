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
        public async Task<IActionResult> Index(int? id, string name)
        {
            if (id == null) return RedirectToAction("Index", "Players");
            ViewBag.PlayerId = id;
            ViewBag.PlayerName = name;
            var CareersByPlayer = _context.Careers.Where(c => c.PlayerId == id).Include(b => b.Player).Include(a => a.Position).Include(c => c.Team);
            //var dBGameContext = _context.Careers.Include(c => c.Player).Include(c => c.Position).Include(c => c.Team);
            return View(await CareersByPlayer.ToListAsync());
        }

        // GET: Careers/Details/5
        public async Task<IActionResult> Details(int? id, int playerId)
        {
            ViewBag.PlayerId = playerId;
            ViewBag.PlayerName = _context.Players.Where(c => c.PlayerId == playerId).FirstOrDefault().FullName;
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
        public IActionResult Create(int playerId)
        {
            ViewBag.PlayerId = playerId;
            ViewBag.PlayerName = _context.Players.Where(c => c.PlayerId == playerId).FirstOrDefault().FullName;
            //ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "FullName");
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "Name");
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "Name");
            return View();
        }

        // POST: Careers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int playerId,[Bind("CareerId,TeamId,PlayerId,StartDate,EndDate,PositionId")] Career career)
        {
            career.PlayerId = playerId;
            if (ModelState.IsValid)
            {
                _context.Add(career);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Careers", new { id = playerId, name = _context.Players.Where(c => c.PlayerId == playerId).FirstOrDefault().FullName });
            }
            //ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "FullName", career.PlayerId);
            //ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "Name", career.PositionId);
            //ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "Name", career.TeamId);
            //return View(career);
            return RedirectToAction("Index", "Careers", new { id = playerId, name = _context.Players.Where(a => a.PlayerId == playerId).FirstOrDefault().FullName });
        }

        // GET: Careers/Edit/5
        public async Task<IActionResult> Edit(int? id, int playerId)
        {
            ViewBag.PlayerId = playerId;
            ViewBag.PlayerName = _context.Players.Where(c => c.PlayerId == playerId).FirstOrDefault().FullName;
            if (id == null)
            {
                return NotFound();
            }

            var career = await _context.Careers.FindAsync(id);
            if (career == null)
            {
                return NotFound();
            }
            //ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "FullName", career.PlayerId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "Name", career.PositionId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "Name", career.TeamId);
            return View(career);
        }

        // POST: Careers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int playerId, [Bind("CareerId,TeamId,PlayerId,StartDate,EndDate,PositionId")] Career career)
        {
            career.PlayerId = playerId;
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
                return RedirectToAction("Index", "Careers", new { id = playerId, name = _context.Players.Where(c => c.PlayerId == playerId).FirstOrDefault().FullName });
            }
            //ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "FullsName", career.PlayerId);
            //ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "Name", career.PositionId);
            //ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "Name", career.TeamId);
            return RedirectToAction("Index", "Careers", new { id = playerId, name = _context.Players.Where(c => c.PlayerId == playerId).FirstOrDefault().FullName });
        }

        // GET: Careers/Delete/5
        public async Task<IActionResult> Delete(int? id, int playerId)
        {
            ViewBag.PlayerId = playerId;
            ViewBag.PlayerName = _context.Players.Where(c => c.PlayerId == playerId).FirstOrDefault().FullName;
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
        public async Task<IActionResult> DeleteConfirmed(int id, int playerId)
        {
            var career = await _context.Careers.FindAsync(id);
            _context.Careers.Remove(career);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Careers", new { id = playerId, name = _context.Players.Where(a => a.PlayerId == playerId).FirstOrDefault().FullName });
        }

        private bool CareerExists(int id)
        {
            return _context.Careers.Any(e => e.CareerId == id);
        }
    }
}
