using FootballApi.Core.Models;
using FootballApi.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApi.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseService<League> Leagues { get; }
        IBaseService<Club> Clubs { get; }
        IBaseService<Player> Players { get; }
        Task<int> Complete();
    }
}
