using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FootballApi.Core.DTOs
{
    public class PlayerDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string Nationality { get; set; } = null!;
        public int ClubId { get; set; }
    }
}
