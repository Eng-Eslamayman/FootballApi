using FootballApi.Core;
using FootballApi.Core.Models;
using FootballApi.Core.Services;
using FootballApi.EF.Data;
using FootballApi.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApi.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly ApplicationDbContext _context;

        public IBaseService<League> Leagues { get; private set; }
        public IBaseService<Club> Clubs { get; private set; }
        public IBaseService<Player> Players { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Leagues = new BaseRepository<League>(_context);
            Clubs = new BaseRepository<Club>(_context);
            Players = new BaseRepository<Player>(_context);
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
