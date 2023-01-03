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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Projekt_zaliczeniowy.Controllers
{
    public class PlayerController : Controller
    {
        private readonly AppDbContext _context;

        public PlayerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Player
        
        public async Task<IActionResult> Index(string? search)
        {
            ViewData["GetDetails"] = search;
            if (!String.IsNullOrEmpty(search))
            {
                var query = GetPlayersBySearch(search);
                return View(query);
            }

            var appDbContext = _context.Players.Include(p => p.Team);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Player/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Players == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // GET: Player/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name");
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
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", player.TeamId);
            return View(player);
        }

        // GET: Player/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Players == null)
            {
                return NotFound();
            }

            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", player.TeamId);
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
            if (id != player.Id)
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
                    if (!PlayerExists(player.Id))
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
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", player.TeamId);
            return View(player);
        }

        // GET: Player/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Players == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Player/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Players == null)
            {
                return Problem("Entity set 'AppDbContext.Players'  is null.");
            }
            var player = await _context.Players.FindAsync(id);
            if (player != null)
            {
                _context.Players.Remove(player);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id)
        {
          return _context.Players.Any(e => e.Id == id);
        }

        public IEnumerable<Player> GetPlayersBySearch(string search)
        {
            var result = _context.Players.Where(x => x.Name.Contains(search) || x.Surname.Contains(search) || x.Nationality.Contains(search) ||x.Position.Contains(search) || x.Team.Name.Contains(search)).Include(p => p.Team);
            return result;
        }
    }
}
