using Microsoft.CodeAnalysis.CSharp.Syntax;
using Projekt_zaliczeniowy.Models;
using Projekt_zaliczeniowy.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Projekt_test
{
    public class MatchServiceEFTest : IMatchService
    {
        private readonly Dictionary<int, Match?> repository = new Dictionary<int, Match?>();
        private readonly Dictionary<int, Team?> teamRepository = new Dictionary<int, Team?>()
        {
            {1,new Team() { Id = 1, Name = "Manchester City", Country = "England", City = "Manchester", Stadium = "Etihad Stadium" }},
            {2,new Team() { Id = 2, Name = "Arsenal", Country = "England", City = "London", Stadium = "Emirates Stadium" }},
            {3,new Team() { Id = 3, Name = "Liverpool FC", Country = "England", City = "Liverpool", Stadium = "Anfield Stadium" }},
            {4,new Team() { Id = 4, Name = "Chelsea", Country = "England", City = "London", Stadium = "Stamford Bridge Stadium" }}

        };

        private int counter = 1;
        private int UniqueId()
        {
            return counter++;
        }

        public int Save(Match match)
        {
            match.Id = UniqueId();
            match.Teams.Add(FindTeam(match.HostId));
            match.Teams.Add(FindTeam(match.GuestId));
            repository.Add(match.Id, match);
            return match.Id;
        }

        public bool Update(Match match)
        {
            if (repository.ContainsKey(match.Id))
            {
                match.Teams.Clear();
                match.Teams.Add(FindTeam(match.HostId));
                match.Teams.Add(FindTeam(match.GuestId));
                repository[match.Id] = match;
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            return repository.Remove(id);
        }
        public Match? FindBy(int? id)
        {
            if (id is null)
                return null;

            return repository.TryGetValue(id ?? 1, out var match) ? match : null;
        }

        public ICollection<Match> FindAll()
        {
            return repository.Values.ToList();
        }

        public Team FindTeam(int id)
        {
            return teamRepository[id];
        }


    }
}
