namespace Projekt_zaliczeniowy.Models.Interfaces
{
    public interface ITeamService
    {
        public int Save(Team team);
        public bool Delete(int id);
        public bool Update(Team team);
        public Team? FindBy(int? id);
        public ICollection<Team> FindAll();
        public IEnumerable<Team> GetTeamsBySearch(string search);

    }
}
