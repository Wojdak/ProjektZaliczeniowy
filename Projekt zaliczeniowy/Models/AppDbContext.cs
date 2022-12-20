using Microsoft.EntityFrameworkCore;

namespace Projekt_zaliczeniowy.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Match> Matches { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            
        }
    }
}
