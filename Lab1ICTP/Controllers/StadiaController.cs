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
    public class StadiaController : Controller
    {
        private readonly DBGameContext _context;

        public StadiaController(DBGameContext context)
        {
            _context = context;
        }

        // GET: Stadia
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Cities", "Index");
            //var dBGameContext = _context.Stadiums.Include(s => s.City);
            ViewBag.CityId = id;
            ViewBag.Name = name;
            var StadiumsByCities = _context.Stadiums.Where(b => b.CityId == id).Include(b => b.City);
            return View(await StadiumsByCities.ToListAsync());
        }

        // GET: Stadia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stadium = await _context.Stadiums
                .Include(s => s.City)
                .FirstOrDefaultAsync(m => m.StadiumId == id);
            if (stadium == null)
            {
                return NotFound();
            }

            return View(stadium);
        }

        // GET: Stadia/Create
        public IActionResult Create(int CityId)
        {
            ViewBag.CityId = CityId;
            ViewBag.Name = _context.Cities.Where(c => c.CityId == CityId).FirstOrDefault().Name;
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "Name");
            return View();
        }

        // POST: Stadia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int CityId, [Bind("StadiumId,Name,CityId")] Stadium stadium)
        {
            stadium.CityId = CityId;
            if (ModelState.IsValid)
            {
                _context.Add(stadium);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Stadia", new { id = CityId, name = _context.Cities.Where(c => c.CityId == CityId).FirstOrDefault().Name });
            }
            //ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "Name", stadium.CityId);
            //return View(stadium);
            return RedirectToAction("Index", "Stadia", new { id = CityId, name = _context.Cities.Where(c => c.CityId == CityId).FirstOrDefault().Name });
        }

        // GET: Stadia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var stadium = await _context.Stadiums.FindAsync(id);
            if (stadium == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "Name", stadium.CityId);
            return View(stadium);
        }

        // POST: Stadia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StadiumId,Name,CityId")] Stadium stadium)
        {
            if (id != stadium.StadiumId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stadium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StadiumExists(stadium.StadiumId))
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
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "Name", stadium.CityId);
            return View(stadium);
        }

        // GET: Stadia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stadium = await _context.Stadiums
                .Include(s => s.City)
                .FirstOrDefaultAsync(m => m.StadiumId == id);
            if (stadium == null)
            {
                return NotFound();
            }

            return View(stadium);
        }

        // POST: Stadia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stadium = await _context.Stadiums.FindAsync(id);
            _context.Stadiums.Remove(stadium);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StadiumExists(int id)
        {
            return _context.Stadiums.Any(e => e.StadiumId == id);
        }
    }
}
