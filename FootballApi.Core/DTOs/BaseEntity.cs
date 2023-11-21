using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApi.Core.DTOs
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

    }
}
