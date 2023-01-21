using Microsoft.EntityFrameworkCore;
using Projekt_zaliczeniowy.Models.Interfaces;
using System.Security.Claims;


namespace Projekt_zaliczeniowy.Models.Services
{
    public class TicketServiceEF : ITicketService
    {
        private readonly AppDbContext _context;
        public TicketServiceEF(AppDbContext context)
        {
            _context = context;
        }

        public int Save(Ticket ticket,string userId)
        {
            ticket.Status = "Completed";
            ticket.totalPrice *= ticket.howManyPeople;
            ticket.UserId = userId;

            SubtractTicket(ticket.MatchId,ticket.howManyPeople);

            var entityEntry = _context.Tickets.Add(ticket);
            _context.SaveChanges();

            return entityEntry.Entity.Id;
        }

        public bool Delete(int id)
        {
            var find = _context.Tickets.Find(id);

            if (find is not null)
            {
                _context.Tickets.Remove(find);
                AddTicket(find.MatchId, find.howManyPeople);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Update(Ticket ticket)
        {
            try
            {
                var find = _context.Tickets.Find(ticket.Id);
                if (find is not null)
                {
                    var _match = _context.Matches.Find(ticket.MatchId);

                    if (find.howManyPeople > ticket.howManyPeople)
                        AddTicket(ticket.MatchId, (find.howManyPeople - ticket.howManyPeople));
                    else
                        SubtractTicket(ticket.MatchId, (ticket.howManyPeople - find.howManyPeople));

                    find.howManyPeople = ticket.howManyPeople;
                    find.totalPrice = ticket.howManyPeople * _match.Price;
                    find.Status = "Edited";
                    find.MatchId = ticket.MatchId;
                    find.UserId = ticket.UserId;  

                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }
        public Ticket? FindBy(int? id)
        {
            var find = _context.Tickets.Find(id);
            return find;
        }

        public ICollection<Ticket> FindAll(string userId)
        {
            return _context.Tickets.Where(t => t.UserId == userId).Include(t => t.Match).ToList();
        }
        public void SubtractTicket(int matchId, int howMany)
        {
            var match = _context.Matches.Find(matchId);
            match.Tickets_amount -= howMany;
        }
        public void AddTicket(int matchId, int howMany)
        {
            var match = _context.Matches.Find(matchId);
            match.Tickets_amount += howMany;
        }


    }
}
