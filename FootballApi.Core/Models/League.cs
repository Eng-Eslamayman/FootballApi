using FootballApi.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApi.Core.Models
{
    public class League : BaseEntity
    {
        public string Country { get; set; } = null!;
        public int FoundedYear { get; set; }
        public int NumberOfTeams { get; set; }
        public string Description { get; set; } = string.Empty;
        public ICollection<Club> Clubs { get; set; } = new List<Club>();    
    }
}
