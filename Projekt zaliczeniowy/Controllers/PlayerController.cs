using System;
using System.Collections;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Projekt_zaliczeniowy.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;
        private readonly ITeamService _teamService;

        public PlayerController(IPlayerService playerService, ITeamService teamService)
        {
            _playerService = playerService;
            _teamService = teamService;
        }

        // GET: Player

        public async Task<IActionResult> Index(string? search)
        {
            ViewData["GetDetails"] = search;
            if (!String.IsNullOrEmpty(search))
                return View(_playerService.GetTeamsBySearch(search));

            return View(_playerService.FindAll());
        }

        // GET: Player/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var player = _playerService.FindBy(id);

            return player is null ? NotFound() : View(player);
        }

        // GET: Player/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["TeamId"] = new SelectList(_teamService.FindAll(), "Id", "Name");
            return View();
        }

        // POST: Player/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Nationality,Date_of_birth,Position,TeamId")] Player player)
        {
            if (ModelState.IsValid)
            {
                _playerService.Save(player);
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamId"] = new SelectList(_teamService.FindAll(), "Id", "Name", player.TeamId);
            return View(player);
        }

        // GET: Player/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var player = _playerService.FindBy(id);

            if (player == null)
                return NotFound();

            ViewData["TeamId"] = new SelectList(_teamService.FindAll(), "Id", "Name", player.TeamId);
            return View(player);
        }

        // POST: Player/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Nationality,Date_of_birth,Position,TeamId")] Player player)
        {
            if (ModelState.IsValid)
            {
                _playerService.Update(player);
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamId"] = new SelectList(_teamService.FindAll(), "Id", "Name", player.TeamId);
            return View(player);
        }

        // GET: Player/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var team = _playerService.FindBy(id);

            return team is null ? NotFound() : View(team);
        }

        // POST: Player/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_playerService.Delete(id))
            {
                return RedirectToAction(nameof(Index));
            }
            return Problem("Trying delete no existing player");
        }
    }
}
