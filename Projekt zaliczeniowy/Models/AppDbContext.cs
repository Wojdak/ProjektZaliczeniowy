using Microsoft.EntityFrameworkCore;

namespace Projekt_zaliczeniowy.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Match> Matches { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().HasData(
                    new Team() { Id = 1, Name = "Manchester City", Country = "England", City = "Manchester", Stadium = "Etihad Stadium" },
                    new Team() { Id = 2, Name = "Arsenal", Country = "England", City = "London", Stadium = "Emirates Stadium" },
                    new Team() { Id = 3, Name = "Liverpool FC", Country = "England", City = "Liverpool", Stadium = "Anfield Stadium" },
                    new Team() { Id = 4, Name = "Chelsea", Country = "England", City = "London", Stadium = "Stamford Bridge Stadium" }
                );

            modelBuilder.Entity<Player>().HasData(
                    new Player() { Id = 1, Name = "Gabriel", Surname = "Jesus", Nationality = "Brazilian", Date_of_birth = DateTime.Parse("1997-04-03"), Position = "Attacker", TeamId=2 },
                    new Player() { Id = 2, Name = "Martin", Surname = "odegaard", Nationality = "Norwegian", Date_of_birth = DateTime.Parse("1998-12-17"), Position = "Midfielder", TeamId = 2 },
                    new Player() { Id = 3, Name = "Kieran", Surname = "Tierney", Nationality = "Scottish", Date_of_birth = DateTime.Parse("1997-06-05"), Position = "Defender", TeamId = 2 },

                    new Player() { Id = 4, Name = "Erling", Surname = "Haaland", Nationality = "Norwegian", Date_of_birth = DateTime.Parse("2000-07-21"), Position = "Attacker", TeamId = 1 },
                    new Player() { Id = 5, Name = "Kevin", Surname = "De Bruyne", Nationality = "Belgian", Date_of_birth = DateTime.Parse("1991-06-28"), Position = "Midfielder", TeamId = 1 },
                    new Player() { Id = 6, Name = "Ruben", Surname = "Dias", Nationality = "Portuguese", Date_of_birth = DateTime.Parse("1997-05-14"), Position = "Defender", TeamId = 1 },

                    new Player() { Id = 7, Name = "Mohamed", Surname = "Salah", Nationality = "Egyptian", Date_of_birth = DateTime.Parse("1992-06-15"), Position = "Attacker", TeamId = 3 },
                    new Player() { Id = 8, Name = "Jordan", Surname = "Henderson", Nationality = "English", Date_of_birth = DateTime.Parse("2090-06-17"), Position = "Midfielder", TeamId = 3 },
                    new Player() { Id = 9, Name = "Virgil", Surname = "van Dijk", Nationality = "Dutch", Date_of_birth = DateTime.Parse("1991-07-08"), Position = "Defender", TeamId = 3 },

                    new Player() { Id = 10, Name = "Kai", Surname = "Havertz", Nationality = "German", Date_of_birth = DateTime.Parse("1999-06-11"), Position = "Attacker", TeamId = 4 },
                    new Player() { Id = 11, Name = "Mason", Surname = "Mount", Nationality = "English", Date_of_birth = DateTime.Parse("1999-01-10"), Position = "Midfielder", TeamId = 4 },
                    new Player() { Id = 12, Name = "Thiago", Surname = "Silva", Nationality = "Brazilian", Date_of_birth = DateTime.Parse("1984-09-22"), Position = "Defender", TeamId = 4 }

                );

            modelBuilder.Entity<Match>().HasData(
                    new Match() { Id=1,HostId=2,GuestId=4,Date= DateTime.Parse("2022-12-26"),Tickets_amount=107,Price=80 },
                    new Match() { Id = 2, HostId = 3, GuestId = 1, Date = DateTime.Parse("2022-12-28"), Tickets_amount = 100,Price = 70 },
                    new Match() { Id = 3, HostId = 4, GuestId = 3, Date = DateTime.Parse("2022-12-31"), Tickets_amount = 260, Price = 80 },
                    new Match() { Id = 4, HostId = 1, GuestId = 2, Date = DateTime.Parse("2023-01-02"), Tickets_amount = 324, Price = 75 }
                );

        }
    }
}
