using GameStore.Data;
using Microsoft.EntityFrameworkCore;
using GameStore.Models;

namespace GameStore.Services
{
    public class GamesService
    {
        private readonly ApplicationDbContext _ctx;

        public GamesService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<Game>> GetGamesAsync()
        {
            return await _ctx.Games.ToListAsync();
        }
    }
}