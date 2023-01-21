using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt_zaliczeniowy.Models;
using Projekt_zaliczeniowy.Models.Interfaces;

namespace Projekt_zaliczeniowy.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly IMatchService _matchService;
        public TicketController(ITicketService ticketService, IMatchService matchService)
        {
            _ticketService = ticketService;
            _matchService = matchService;
        }

        // GET: Ticket
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(_ticketService.FindAll(userId));
        }

        // GET: Ticket/Details/5
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var ticket = _ticketService.FindBy(id);

            return ticket is null ? NotFound() : View(ticket);
        }

        // GET: Ticket/Create
        [Authorize(Roles = "Admin,User")]
        public IActionResult Create(int? id)
        {
            Match match = _matchService.FindBy(id);
            Ticket ticket = new Ticket();
            ticket.MatchId = match.Id;
            ticket.totalPrice = match.Price;
            ticket.Status = "In process";

            ViewBag.maxValue = match.Tickets_amount;
            return View(ticket);
        }

        // POST: Ticket/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("howManyPeople,totalPrice,Status,MatchId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _ticketService.Save(ticket, userId);
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: Ticket/Edit/5
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var ticket = _ticketService.FindBy(id);

            if (ticket == null)
                return NotFound();

            ViewBag.maxValue = _matchService.FindBy(ticket.MatchId).Tickets_amount + ticket.howManyPeople;
            return View(ticket);
        }

        // POST: Ticket/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,howManyPeople,totalPrice,Status,MatchId,UserId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _ticketService.Update(ticket);
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: Ticket/Delete/5
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var team = _ticketService.FindBy(id);

            return team is null ? NotFound() : View(team);
        }

        // POST: Ticket/Delete/5
        [Authorize(Roles = "Admin,User")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_ticketService.Delete(id))
            {
                return RedirectToAction(nameof(Index));
            }
            return Problem("Trying delete no existing ticket");
        }
    }
}
