namespace Projekt_zaliczeniowy.Models.Interfaces
{
    public interface IPlayerService
    {
        public int Save(Player player);
        public bool Delete(int id);
        public bool Update(Player player);
        public Player? FindBy(int? id);
        public ICollection<Player> FindAll();
        public IEnumerable<Player> GetTeamsBySearch(string search);
    }
}
