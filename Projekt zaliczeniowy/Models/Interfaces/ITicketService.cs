namespace Projekt_zaliczeniowy.Models.Interfaces
{
    public interface ITicketService
    {
        public int Save(Ticket ticket, string userId);
        public bool Delete(int id);
        public bool Update(Ticket ticket);
        public Ticket? FindBy(int? id);
        public ICollection<Ticket> FindAll(string userId);
        public void SubtractTicket(int matchId,int howMany);
        public void AddTicket(int matchId, int howMany);
    }
}
