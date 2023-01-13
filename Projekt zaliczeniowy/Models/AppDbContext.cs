using Microsoft.EntityFrameworkCore;
using System.Globalization;

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
            CultureInfo provider = CultureInfo.InvariantCulture;

            modelBuilder.Entity<Team>().HasData(
                    new Team() { Id = 1, Name = "Manchester City", Country = "England", City = "Manchester", Stadium = "Etihad Stadium" },
                    new Team() { Id = 2, Name = "Arsenal", Country = "England", City = "London", Stadium = "Emirates Stadium" },
                    new Team() { Id = 3, Name = "Liverpool FC", Country = "England", City = "Liverpool", Stadium = "Anfield Stadium" },
                    new Team() { Id = 4, Name = "Chelsea", Country = "England", City = "London", Stadium = "Stamford Bridge Stadium" }
                );

            modelBuilder.Entity<Player>().HasData(
                    new Player() { Id = 1, Name = "Gabriel", Surname = "Jesus", Nationality = "Brazilian", Date_of_birth = DateTime.ParseExact("1997-04-03","yyyy-MM-dd", provider), Position = "Attacker", TeamId=2 },
                    new Player() { Id = 2, Name = "Martin", Surname = "odegaard", Nationality = "Norwegian", Date_of_birth = DateTime.ParseExact("1998-12-17", "yyyy-MM-dd", provider), Position = "Midfielder", TeamId = 2 },
                    new Player() { Id = 3, Name = "Kieran", Surname = "Tierney", Nationality = "Scottish", Date_of_birth = DateTime.ParseExact("1997-06-05", "yyyy-MM-dd", provider), Position = "Defender", TeamId = 2 },

                    new Player() { Id = 4, Name = "Erling", Surname = "Haaland", Nationality = "Norwegian", Date_of_birth = DateTime.ParseExact("2000-07-21", "yyyy-MM-dd", provider), Position = "Attacker", TeamId = 1 },
                    new Player() { Id = 5, Name = "Kevin", Surname = "De Bruyne", Nationality = "Belgian", Date_of_birth = DateTime.ParseExact("1991-06-28", "yyyy-MM-dd", provider), Position = "Midfielder", TeamId = 1 },
                    new Player() { Id = 6, Name = "Ruben", Surname = "Dias", Nationality = "Portuguese", Date_of_birth = DateTime.ParseExact("1997-05-14", "yyyy-MM-dd", provider), Position = "Defender", TeamId = 1 },

                    new Player() { Id = 7, Name = "Mohamed", Surname = "Salah", Nationality = "Egyptian", Date_of_birth = DateTime.ParseExact("1992-06-15", "yyyy-MM-dd", provider), Position = "Attacker", TeamId = 3 },
                    new Player() { Id = 8, Name = "Jordan", Surname = "Henderson", Nationality = "English", Date_of_birth = DateTime.ParseExact("2090-06-17", "yyyy-MM-dd", provider), Position = "Midfielder", TeamId = 3 },
                    new Player() { Id = 9, Name = "Virgil", Surname = "van Dijk", Nationality = "Dutch", Date_of_birth = DateTime.ParseExact("1991-07-08", "yyyy-MM-dd", provider), Position = "Defender", TeamId = 3 },

                    new Player() { Id = 10, Name = "Kai", Surname = "Havertz", Nationality = "German", Date_of_birth = DateTime.ParseExact("1999-06-11", "yyyy-MM-dd", provider), Position = "Attacker", TeamId = 4 },
                    new Player() { Id = 11, Name = "Mason", Surname = "Mount", Nationality = "English", Date_of_birth = DateTime.ParseExact("1999-01-10", "yyyy-MM-dd", provider), Position = "Midfielder", TeamId = 4 },
                    new Player() { Id = 12, Name = "Thiago", Surname = "Silva", Nationality = "Brazilian", Date_of_birth = DateTime.ParseExact("1984-09-22", "yyyy-MM-dd", provider), Position = "Defender", TeamId = 4 }

                );

            modelBuilder.Entity<Match>().HasData(
                    new Match() { Id = 1, HostId = 2, GuestId = 4, Date = DateTime.Parse("2022-12-26 17:30"), Tickets_amount = 15, Price = 50},
                    new Match() { Id = 2, HostId = 3, GuestId = 1, Date = DateTime.Parse("2022-12-28 21:00"), Tickets_amount = 0, Price = 40 },
                    new Match() { Id = 3, HostId = 4, GuestId = 3, Date = DateTime.Parse("2022-12-31 13:30"), Tickets_amount = 5, Price = 50 },
                    new Match() { Id = 4, HostId = 1, GuestId = 2, Date = DateTime.Parse("2023-01-02 20:45"), Tickets_amount = 2, Price = 45 }
                );

        }
    }
}
