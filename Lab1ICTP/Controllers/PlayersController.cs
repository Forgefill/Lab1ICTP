using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab1ICTP;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;

namespace Lab1ICTP.Controllers
{
    public class PlayersController : Controller
    {
        private readonly DBGameContext _context;

        public PlayersController(DBGameContext context)
        {
            _context = context;
        }

        // GET: Players
        public async Task<IActionResult> Index()
        {
            SelectList players = new SelectList(_context.Players, "PlayerId", "FullName");
            SelectList teams = new SelectList(_context.Teams, "TeamId", "Name");
            SelectList positions = new SelectList(_context.Positions, "PositionId", "Name");
            ViewBag.NULL = -1;
            ViewBag.players = players;
            ViewBag.teams = teams;
            ViewBag.positions = positions;
            return View(await _context.Players.ToListAsync());
        }

        // GET: Players/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .FirstOrDefaultAsync(m => m.PlayerId == id);
            if (player == null)
            {
                return NotFound();
            }

            //return View(player);
            return RedirectToAction("Index", "Careers", new { id = player.PlayerId, name = player.FullName });
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlayerId,FullName")] Player player)
        {
            if (ModelState.IsValid)
            {
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlayerId,FullName")] Player player)
        {
            if (id != player.PlayerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(player);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.PlayerId))
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
            return View(player);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .FirstOrDefaultAsync(m => m.PlayerId == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = await _context.Players.FindAsync(id);
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.PlayerId == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile fileExcel)
        {
            if (ModelState.IsValid) 
            {
                if (fileExcel != null)
                {
                    using (var stream = new FileStream(fileExcel.FileName, FileMode.Create))
                    {
                        await fileExcel.CopyToAsync(stream);
                        using (XLWorkbook workBook = new XLWorkbook(stream, XLEventTracking.Disabled))
                        {
                            //перегляд усіх листів (в даному випадку категорій)
                            foreach (IXLWorksheet worksheet in workBook.Worksheets)
                            {
                                //worksheet.Name - ім'я гравця. Пробуємо знайти в БД, якщо відсутня, то створюємо нову
                                Player newcat;
                                var c = (from cat in _context.Players.Include("Careers")
                                         where cat.FullName.Contains(worksheet.Name)
                                         select cat).ToList();
                                if (c.Count > 0)
                                {
                                    newcat = c[0];
                                }
                                else
                                {
                                    newcat = new Player();
                                    newcat.FullName = worksheet.Name;
                                    _context.Players.Add(newcat);
                                }
                                //перегляд усіх рядків                    
                                foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
                                {
                                    try
                                    {

                                        string teamStr = row.Cell(1).Value.ToString();
                                        string teamCityStr = row.Cell(2).Value.ToString();
                                        string startDate = row.Cell(3).Value.ToString();
                                        string endDate = row.Cell(4).Value.ToString();
                                        string posStr = row.Cell(5).Value.ToString();

                                        if (teamStr.Length == 0 || teamCityStr.Length == 0 || posStr.Length == 0 || startDate.Length == 0 || endDate.Length == 0)
                                        {
                                            throw new Exception("row has empty field");
                                        }
                                        DateTime start;
                                        DateTime end;
                                        if(!DateTime.TryParse(startDate, out start) || !DateTime.TryParse(endDate, out end))
                                        {
                                            throw new Exception("wrong data type");
                                        }
                                        if(start >= end)
                                        {
                                            throw new Exception("start later than end");
                                        }
                                        if(newcat.Careers.Any(c => c.StartDate >= end || c.EndDate >= start))
                                        {
                                            throw new Exception("player's career intersects");
                                        }



                                        Career career = new Career();
                                        career.StartDate = start;
                                        career.EndDate = end;
                                        career.Player = newcat;
                                        _context.Careers.Add(career);
                                        //у разі наявності команди знайти її, у разі відсутності - додати
                                        Team team;

                                        var teams = (from aut in _context.Teams
                                                     where aut.Name.Contains(row.Cell(1).Value.ToString())
                                                     select aut).ToList();
                                        if (teams.Count > 0)
                                        {
                                            team = teams[0];
                                        }
                                        else
                                        {
                                            team = new Team();
                                            team.Name = row.Cell(1).Value.ToString();
                                            var b = (from aut in _context.Cities
                                                     where aut.Name.Contains(row.Cell(2).Value.ToString())
                                                     select aut).ToList();
                                            if (b.Count > 0) team.City = b[0];
                                            else
                                            {
                                                team.City = new City();
                                                team.City.Name = row.Cell(2).Value.ToString();
                                                _context.Cities.Add(team.City);
                                            }
                                            _context.Teams.Add(team);
                                        }
                                        career.Team = team;


                                        
                                        Position pos;

                                        var a = (from aut in _context.Positions
                                                 where aut.Name.Contains(row.Cell(5).Value.ToString())
                                                 select aut).ToList();
                                        if (a.Count > 0)
                                        {
                                            pos = a[0];
                                        }
                                        else
                                        {
                                            pos = new Position();
                                            pos.Name = row.Cell(5).Value.ToString();
                                            _context.Positions.Add(pos);
                                        }
                                        career.Position = pos;
                                        

                                    }
                                    catch (Exception e)
                                    {
                                        //logging самостійно :)
                                        StreamWriter a = new StreamWriter(@"log.txt", true);
                                        a.WriteLine("Error with player: {0} row num {1} --- error message: {2} - time: {3}", worksheet.Name, row.RowNumber(), e.Message, DateTime.Now.ToString());
                                        a.Close();
                                    }
                                }
                            }
                        }
                    }
                }

                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Export(int? playerid, int? teamid, int? posid)
        {
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var players = _context.Players.Include("Careers").ToList();
                if (playerid != null) players = players.Where(c => c.PlayerId == playerid).ToList();

                //тут, для прикладу ми пишемо усі книжки з БД, в своїх проектах ТАК НЕ РОБИТИ (писати лише вибрані)
                foreach (var c in players)
                {
                    var careers = c.Careers.ToList();
                    if (teamid != null)
                    {
                        careers = careers.Where(a => a.TeamId == teamid).ToList();                        
                    }
                    if (posid != null)
                    {
                        careers = careers.Where(a => a.PositionId == posid).ToList();
                    }
                    if (careers.Count == 0) continue;

                    var worksheet = workbook.Worksheets.Add(c.FullName);
                    worksheet.Cell("A1").Value = "Команда";
                    worksheet.Cell("B1").Value = "Місто команди";
                    worksheet.Cell("C1").Value = "Дата початку";
                    worksheet.Cell("D1").Value = "Дата закінчення";
                    worksheet.Cell("E1").Value = "Позиція";
                    worksheet.Row(1).Style.Font.Bold = true;

                    //нумерація рядків/стовпчиків починається з індекса 1 (не 0)
                    for(int i = 0; i < careers.Count; i++)
                    {
                        var team = _context.Teams.Where(a => a.TeamId == careers[i].TeamId).FirstOrDefault();
                        var city = _context.Cities.Where(a => a.CityId == team.CityId).FirstOrDefault();
                        var pos = _context.Positions.Where(a => a.PositionId == careers[i].PositionId).FirstOrDefault();
                        worksheet.Cell(i + 2, 3).Value = careers[i].StartDate;
                        worksheet.Cell(i + 2, 4).Value = careers[i].EndDate;
                        worksheet.Cell(i + 2, 1).Value = team.Name;
                        worksheet.Cell(i + 2, 2).Value = city.Name;
                        worksheet.Cell(i + 2, 5).Value = pos.Name;
                    }
                }
                if (workbook.Worksheets.Count == 0) workbook.AddWorksheet();
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        stream.Flush();

                        return new FileContentResult(stream.ToArray(),
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                        {
                            FileDownloadName = $"Players_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                        };
                    }
            }
        }
    }
}

