using Microsoft.EntityFrameworkCore;
using GameStore.Data;
using GameStore.Models;

namespace GameStore.Services
{
    public class GamesService
    {
        private readonly ApplicationDbContext _db;

        public GamesService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddGameAsync(Game game)
        {
            _db.Games.Add(game);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Game>> GetAllAsync()
        {
            return await _db.Games.AsNoTracking().OrderByDescending(g => g.CreatedAt).ToListAsync();
        }

        public async Task<List<Game>> GetByConditionAsync(GameCondition condition)
        {
            return await _db.Games.Where(g => g.Condition == condition).AsNoTracking().OrderByDescending(g => g.CreatedAt).ToListAsync();
        }

        public async Task<Dictionary<GameCondition,int>> GetCountsByConditionAsync()
        {
            return await _db.Games
                .GroupBy(g => g.Condition)
                .Select(g => new { Condition = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Condition, x => x.Count);
        }

        // New: return a list of distinct platforms for filtering UI
        public async Task<List<string>> GetPlatformsAsync()
        {
            return await _db.Games
                .AsNoTracking()
                .Select(g => g.Platform)
                .Distinct()
                .OrderBy(p => p)
                .ToListAsync();
        }

        // Optional: query by platform
        public async Task<List<Game>> GetByPlatformAsync(string platform)
        {
            return await _db.Games.Where(g => g.Platform == platform).AsNoTracking().OrderByDescending(g => g.CreatedAt).ToListAsync();
        }

        // New: server-side filter + sort
        public async Task<List<Game>> GetFilteredAsync(GameCondition? condition, string? platform, string? sort)
        {
            IQueryable<Game> q = _db.Games.AsNoTracking();

            if (condition != null)
            {
                q = q.Where(g => g.Condition == condition.Value);
            }

            if (!string.IsNullOrEmpty(platform))
            {
                q = q.Where(g => g.Platform == platform);
            }

            q = (sort ?? string.Empty) switch
            {
                "Condition" => q.OrderBy(g => g.Condition),
                "Platform" => q.OrderBy(g => g.Platform),
                "Title" => q.OrderBy(g => g.Title),
                "PriceAsc" => q.OrderBy(g => g.Price),
                "PriceDesc" => q.OrderByDescending(g => g.Price),
                "CreatedAtAsc" => q.OrderBy(g => g.CreatedAt),
                "CreatedAtDesc" => q.OrderByDescending(g => g.CreatedAt),
                _ => q.OrderByDescending(g => g.CreatedAt)
            };

            return await q.ToListAsync();
        }

        // Remove games by ids when purchased
        public async Task RemoveGamesByIdAsync(IEnumerable<int> ids)
        {
            var idList = ids?.ToList() ?? new List<int>();
            if (!idList.Any()) return;

            var toRemove = await _db.Games.Where(g => idList.Contains(g.Id)).ToListAsync();
            if (toRemove.Any())
            {
                _db.Games.RemoveRange(toRemove);
                await _db.SaveChangesAsync();
            }
        }
    }
}