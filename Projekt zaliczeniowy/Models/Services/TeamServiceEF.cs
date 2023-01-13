using Microsoft.EntityFrameworkCore;
using Projekt_zaliczeniowy.Models.Interfaces;

namespace Projekt_zaliczeniowy.Models.Services
{
    public class TeamServiceEF : ITeamService
    {
        private readonly AppDbContext _context;
        public TeamServiceEF(AppDbContext context)
        {
            _context = context;
        }

        public int Save(Team team)
        {
            var entityEntry = _context.Teams.Add(team);
            _context.SaveChanges();
            return entityEntry.Entity.Id;
        }
        public bool Delete(int id)
        {
            var find = _context.Teams.Find(id);

            if(find is not null)
            {
                _context.Teams.Remove(find);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Update(Team team)
        {
            try
            {
                var find= _context.Teams.Find(team.Id);
                if(find is not null)
                {
                    find.Name=team.Name;
                    find.City=team.City;
                    find.Stadium=team.Stadium;
                    find.Country = team.Country;
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

        public Team? FindBy(int? id)
        {
            var find = _context.Teams.Include(p => p.Players).FirstOrDefault(t => t.Id == id);
            return find;
        }

        public ICollection<Team> FindAll()
        {
            return _context.Teams.ToList();
        }

        public IEnumerable<Team> GetTeamsBySearch(string search)
        {
            var result = _context.Teams.Where(x => x.Name.Contains(search) || x.Country.Contains(search) || x.City.Contains(search) || x.Stadium.Contains(search));
            return result;
        }
    }
}
