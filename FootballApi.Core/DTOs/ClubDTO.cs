using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FootballApi.Core.DTOs
{
    public class ClubDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; } = null!;
        public int FoundedYear { get; set; }
        public int NumberOfTournaments { get; set; }
        public int LeagueId { get; set; }
    }
}
