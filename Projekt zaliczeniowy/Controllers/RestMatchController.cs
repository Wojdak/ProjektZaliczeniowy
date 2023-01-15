using Microsoft.AspNetCore.Mvc;
using Projekt_zaliczeniowy.Models;
using Projekt_zaliczeniowy.Models.Interfaces;
using Projekt_zaliczeniowy.Models.Services;
using System.Numerics;

namespace Projekt_zaliczeniowy.Controllers
{
    [Route("api/matches")]
    [ApiController]
    public class RestMatchController : ControllerBase
    {
        private readonly IMatchService _matchService;

        public RestMatchController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Match?>> GetMatches()
        {
            if (_matchService is null)
                return NotFound();
 
            return new ActionResult<IEnumerable<Match?>>(_matchService.FindAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Match> GetMatch(int id)
        {
            if (_matchService == null)
                return NotFound();

            var match = _matchService.FindBy(id);

            return match is null ? NotFound() : match;
        }

        [HttpPost]
        public ActionResult<Match> PostMatch(Match match)
        {
            if (_matchService == null)
                return Problem("Entity set 'AppDbContext.Match'  is null.");

            if (ModelState.IsValid)
            {
                var saved = _matchService.Save(match);
                return Created($"/api/books/{saved}", match);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMatch(int id)
        {
            if (_matchService == null || _matchService.FindBy(id) == null)
            {
                return NotFound();
            }

            var match = _matchService.Delete(id);

            if (match == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult PutMatch(int id, Match? match)
        {
            if (id != match.Id)
                return BadRequest();

            match.Id = id;
            _matchService.Update(match);

            return NoContent();
        }

    }
}
