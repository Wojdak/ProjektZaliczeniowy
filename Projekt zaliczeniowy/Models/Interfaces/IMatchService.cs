namespace Projekt_zaliczeniowy.Models.Interfaces
{
    public interface IMatchService
    {
        public int Save(Match match);
        public bool Delete(int id);
        public bool Update(Match match);
        public Match? FindBy(int? id);
        public ICollection<Match> FindAll();
        public Team FindTeam(int id);
    }
}
