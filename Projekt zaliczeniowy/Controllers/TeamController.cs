using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt_zaliczeniowy.Models;
using Projekt_zaliczeniowy.Models.Interfaces;

namespace Projekt_zaliczeniowy.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamController( ITeamService teamService)
        {
            _teamService = teamService;
        }

        // GET: Team
        public async Task<IActionResult> Index(string? search)
        {
            ViewData["GetDetails"] = search;
            if (!String.IsNullOrEmpty(search))
                return View(_teamService.GetTeamsBySearch(search));

            return View(_teamService.FindAll());
        }

        // GET: Team/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var team = _teamService.FindBy(id);

            return team is null ? NotFound() : View(team);
        }

        // GET: Team/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Team/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Country,City,Stadium")] Team team)
        {
            if (ModelState.IsValid)
            {
                _teamService.Save(team);
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // GET: Team/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var team = _teamService.FindBy(id);

            return team is null ? NotFound() : View(team);
        }

        // POST: Team/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Country,City,Stadium")] Team team)
        {
            if (ModelState.IsValid)
            {
                _teamService.Update(team);
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // GET: Team/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var team = _teamService.FindBy(id);

            return team is null ? NotFound() : View(team);
        }

        // POST: Team/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_teamService.Delete(id))
            {
                return RedirectToAction(nameof(Index));
            }
            return Problem("Trying delete no existing team");
        }
    }
}
