using Microsoft.EntityFrameworkCore;
using Projekt_zaliczeniowy.Models.Interfaces;

namespace Projekt_zaliczeniowy.Models.Services
{
    public class MatchServiceEF : IMatchService
    {
        private readonly AppDbContext _context;
        public MatchServiceEF(AppDbContext context)
        {
            _context = context;
        }

        public int Save(Match match)
        {
            match.Teams.Add(FindTeam(match.HostId));
            match.Teams.Add(FindTeam(match.GuestId));
            var entityEntry = _context.Matches.Add(match);
            _context.SaveChanges();
            return entityEntry.Entity.Id;
        }

        public bool Delete(int id)
        {
            var find = _context.Matches.Find(id);

            if (find is not null)
            {
                _context.Matches.Remove(find);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Update(Match match)
        {
            try
            {
                var find = _context.Matches.Find(match.Id);
                
                if (find is not null)
                {
                    
                    find.HostId = match.HostId;
                    find.GuestId=match.GuestId;
                    find.Date = match.Date;
                    find.Tickets_amount = match.Tickets_amount;
                    find.Price=match.Price;

                    _context.Database.ExecuteSqlRaw($"DELETE FROM [MatchTeam] WHERE MatchesId={match.Id}");

                    find.Teams.Clear();
                    find.Teams.Add(FindTeam(match.HostId));
                    find.Teams.Add(FindTeam(match.GuestId));
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

        public Match? FindBy(int? id)
        {
            var find = _context.Matches.Include(m => m.Teams).FirstOrDefault(m => m.Id == id);
            return find;
        }

        public ICollection<Match> FindAll()
        {
            return _context.Matches.Include(m => m.Teams).ToList();
        }

        public Team FindTeam(int id)
        {
            return _context.Teams.Find(id);
        }

    }
}
