using FootballApi.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApi.Core.Models
{
    public class Player : BaseEntity
    {
        public int Age { get; set; }
        public string Nationality { get; set; } = null!;
        public int ClubId { get; set; }
        public Club? Club { get; set; }

    }
}
