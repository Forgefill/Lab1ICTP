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
    public class GamesController : Controller
    {
        private readonly DBGameContext _context;

        public GamesController(DBGameContext context)
        {
            _context = context;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            var dBGameContext = _context.Games.Include(g => g.Referee).Include(g => g.Stadium).Include(g => g.Team).Include(g => g.TeamId2Navigation).Include(g => g.Tournament);
            return View(await dBGameContext.ToListAsync());
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .Include(g => g.Referee)
                .Include(g => g.Stadium)
                .Include(g => g.Team)
                .Include(g => g.TeamId2Navigation)
                .Include(g => g.Tournament)
                .FirstOrDefaultAsync(m => m.GameId == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            ViewData["RefereeId"] = new SelectList(_context.Referees, "RefereeId", "FullName");
            ViewData["StadiumId"] = new SelectList(_context.Stadiums, "StadiumId", "Name");
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "Name");
            ViewData["TeamId2"] = new SelectList(_context.Teams, "TeamId", "Name");
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "TournamentId", "Name");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameId,StartTime,StadiumId,TournamentId,TeamId,TeamId2,RefereeId")] Game game)
        {
            if(game.TeamId == game.TeamId2)
            {
                ModelState.AddModelError("TeamId", "Помилкова пара команд");
            }
            if (game.StartTime < new DateTime(1970, 12, 1))
            {
                ModelState.AddModelError("StartTime", "Час доступний 1970 р.н.е. і далі");
            }
            if (ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RefereeId"] = new SelectList(_context.Referees, "RefereeId", "FullName", game.RefereeId);
            ViewData["StadiumId"] = new SelectList(_context.Stadiums, "StadiumId", "Name", game.StadiumId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "Name", game.TeamId);
            ViewData["TeamId2"] = new SelectList(_context.Teams, "TeamId", "Name", game.TeamId2);
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "TournamentId", "Name", game.TournamentId);
            return View(game);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            ViewData["RefereeId"] = new SelectList(_context.Referees, "RefereeId", "FullName", game.RefereeId);
            ViewData["StadiumId"] = new SelectList(_context.Stadiums, "StadiumId", "Name", game.StadiumId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "Name", game.TeamId);
            ViewData["TeamId2"] = new SelectList(_context.Teams, "TeamId", "Name", game.TeamId2);
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "TournamentId", "Name", game.TournamentId);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GameId,StartTime,StadiumId,TournamentId,TeamId,TeamId2,RefereeId")] Game game)
        {
            if (id != game.GameId)
            {
                return NotFound();
            }
            if (game.TeamId == game.TeamId2)
            {
                ModelState.AddModelError("TeamId", "Помилкова пара команд");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.GameId))
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
            ViewData["RefereeId"] = new SelectList(_context.Referees, "RefereeId", "FullName", game.RefereeId);
            ViewData["StadiumId"] = new SelectList(_context.Stadiums, "StadiumId", "Name", game.StadiumId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "Name", game.TeamId);
            ViewData["TeamId2"] = new SelectList(_context.Teams, "TeamId", "Name", game.TeamId2);
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "TournamentId", "Name", game.TournamentId);
            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .Include(g => g.Referee)
                .Include(g => g.Stadium)
                .Include(g => g.Team)
                .Include(g => g.TeamId2Navigation)
                .Include(g => g.Tournament)
                .FirstOrDefaultAsync(m => m.GameId == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Games.FindAsync(id);
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.GameId == id);
        }

        public async Task<IActionResult> GamesByReferee(int refereeId, string name)
        {
            ViewBag.refereeId = refereeId;
            ViewBag.refereeName = name;
            var GamesByReferee = _context.Games.Where(c=>c.Referee.RefereeId == refereeId).Include(g => g.Stadium).Include(g => g.Team).Include(g => g.TeamId2Navigation).Include(g => g.Tournament);
            return View(await GamesByReferee.ToListAsync());
        }
        public async Task<IActionResult> GamesByTeam(int teamId, string name)
        {
            ViewBag.teamId = teamId;
            ViewBag.teamName = name;
            var GamesByTeam = _context.Games.Where(c => c.TeamId == teamId || c.TeamId2 == teamId).Include(g =>g.Referee).Include(g => g.Stadium).Include(g => g.Team).Include(g => g.TeamId2Navigation).Include(g => g.Tournament);
            return View(await GamesByTeam.ToListAsync());
        }
        public async Task<IActionResult> GamesByStadium(int stadiumId, string name)
        {
            ViewBag.stadiumId = stadiumId;
            ViewBag.stadiumName = name;
            var GamesByTeam = _context.Games.Where(c => c.StadiumId == stadiumId).Include(g => g.Referee).Include(g => g.Stadium).Include(g => g.Team).Include(g => g.TeamId2Navigation).Include(g => g.Tournament);
            return View(await GamesByTeam.ToListAsync());
        }
        public async Task<IActionResult> GamesByTournament(int tournamentId, string name)
        {
            ViewBag.tournamentId = tournamentId;
            ViewBag.tournamentName = name;
            var GamesByTeam = _context.Games.Where(c => c.TournamentId == tournamentId).Include(g => g.Referee).Include(g => g.Stadium).Include(g => g.Team).Include(g => g.TeamId2Navigation).Include(g => g.Tournament);
            return View(await GamesByTeam.ToListAsync());
        }
    }
}
