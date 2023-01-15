using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt_zaliczeniowy.Models;
using Projekt_zaliczeniowy.Models.Interfaces;

namespace Projekt_zaliczeniowy.Controllers
{
    public class MatchController : Controller
    {
        private readonly IMatchService _matchService;
        private readonly ITeamService _teamService;
        public MatchController(IMatchService matchService, ITeamService teamService)
        {
            _matchService = matchService;
            _teamService = teamService;
        }

        // GET: Match
        public async Task<IActionResult> Index(string? search)
        {

            return View(_matchService.FindAll());
        }

        // GET: Match/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var player = _matchService.FindBy(id);

            return player is null ? NotFound() : View(player);
        }

        // GET: Match/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            //Match match = new Match();
            //_context.Teams.ToList().ForEach(x => match.TeamsList.Add(x));
            ViewData["selectTeam"] = new SelectList(_teamService.FindAll(), "Id", "Name");
            return View();
        }

        // POST: Match/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HostId,GuestId,Date,Tickets_amount,Price,Teams")] Match match)
        {
            if (ModelState.IsValid)
            {
                _matchService.Save(match);
                return RedirectToAction(nameof(Index));
            }
            ViewData["selectTeam"] = new SelectList(_teamService.FindAll(), "Id", "Name");
            return View(match);
        }

        // GET: Match/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var match = _matchService.FindBy(id);

            if (match == null)
                return NotFound();

            ViewData["selectTeam"] = new SelectList(_teamService.FindAll(), "Id", "Name");
            return View(match);
        }

        // POST: Match/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HostId,GuestId,Date,Tickets_amount,Price,Teams")] Match match)
        {
            if (ModelState.IsValid)
            {
                _matchService.Update(match);
                return RedirectToAction(nameof(Index));
            }
            ViewData["selectTeam"] = new SelectList(_teamService.FindAll(), "Id", "Name");
            return View(match);
        }

        // GET: Match/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var team = _matchService.FindBy(id);

            return team is null ? NotFound() : View(team);
        }

        // POST: Match/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_matchService.Delete(id))
            {
                return RedirectToAction(nameof(Index));
            }
            return Problem("Trying delete no existing match");
        }
    }
}
