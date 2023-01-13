using Microsoft.EntityFrameworkCore;
using Projekt_zaliczeniowy.Models.Interfaces;

namespace Projekt_zaliczeniowy.Models.Services
{
    public class PlayerServiceEF : IPlayerService
    {
        private readonly AppDbContext _context;
        public PlayerServiceEF(AppDbContext context)
        {
            _context = context;
        }

        public int Save(Player player)
        {
            var entityEntry = _context.Players.Add(player);
            _context.SaveChanges();
            return entityEntry.Entity.Id;
        }
        public bool Delete(int id)
        {
            var find = _context.Players.Find(id);

            if (find is not null)
            {
                _context.Players.Remove(find);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Update(Player player)
        {
            try
            {
                var find = _context.Players.Find(player.Id);
                if (find is not null)
                {
                    find.Name = player.Name;
                    find.Surname = player.Surname;
                    find.Nationality=player.Nationality;
                    find.Position = player.Position;
                    find.Date_of_birth = player.Date_of_birth;
                    find.TeamId = player.TeamId;
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

        public Player? FindBy(int? id)
        {
            var find = _context.Players.Include(t => t.Team).FirstOrDefault(p=>p.Id==id);
            return find;
        }

        public ICollection<Player> FindAll()
        {
            return _context.Players.Include(p => p.Team).ToList();
        }

        public IEnumerable<Player> GetTeamsBySearch(string search)
        {
            var result = _context.Players.Where(x => x.Name.Contains(search) || x.Surname.Contains(search) || x.Nationality.Contains(search) || x.Position.Contains(search) || x.Team.Name.Contains(search)).Include(p => p.Team);
            return result;
        }
    }
}
