using FootballApi.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApi.Core.Models
{
    public class Club:BaseEntity
    {
        public string City { get; set; } = null!;
        public int FoundedYear { get; set; }
        public int NumberOfTournaments { get; set; }
        public int LeagueId { get; set; }   
        public League? League { get; set; }
        public ICollection<Player> Players { get; set; } = new List<Player>();

    }
}
