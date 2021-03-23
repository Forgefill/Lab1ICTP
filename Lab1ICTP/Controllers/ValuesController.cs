using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lab1ICTP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly DBGameContext _context;

        public ChartsController(DBGameContext context)
        {
            _context = context;
        }

        [HttpGet("JsonData")]

        public JsonResult JsonData()
        {
            var cities = _context.Cities.Include(b => b.Teams).ToList();

                List<object> catCity = new List<object>();
            catCity.Add(new[] { "Місто", "Кількість Команд" });

            foreach(var c in cities)
            {
                catCity.Add(new object[] { c.Name, c.Teams.Count() });
            }

            return new JsonResult(catCity);
        }
        [HttpGet("JsonData2")]
        public JsonResult JsonGamesToTournamentData()
        {
            var Tournaments = _context.Tournaments.Include(b => b.Games).ToList();

            List<object> catTournament = new List<object>();
            catTournament.Add(new[] { "Турнір", "Кількість Матчів" });

            foreach (var c in Tournaments)
            {
                catTournament.Add(new object[] { c.Name, c.Games.Count() });
            }

            return new JsonResult(catTournament);
        }
    }
}
