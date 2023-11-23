using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FootballApi.Core.DTOs
{
    public class LeagueDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int FoundedYear { get; set; }
        public int NumberOfTeams { get; set; }
    }
}
