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
        public async Task<ActionResult<IEnumerable<Match?>>> GetMatches()
        {
            if (_matchService is null)
                return NotFound();
 
            return new ActionResult<IEnumerable<Match?>>(_matchService.FindAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Match>> GetMatch(int id)
        {
            if (_matchService == null)
                return NotFound();

            var book = _matchService.FindBy(id);

            return book is null ? NotFound() : book;
        }

        [HttpPost]
        public async Task<ActionResult<Match>> PostBook(Match match)
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
        public async Task<IActionResult> DeleteMatch(int id)
        {
            if (_matchService == null)
            {
                return NotFound();
            }

            var book = _matchService.Delete(id);

            if (book == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatch(int id, Match? match)
        {
            if (id != match.Id)
                return BadRequest();

            match.Id = id;
            _matchService.Update(match);

            return NoContent();
        }

    }
}
