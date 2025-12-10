using GameStore.Data;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Services
{
    public class CardsService
    {
        private readonly ApplicationDbContext _db;

        public CardsService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Card>> GetForUserAsync(string userId)
        {
            return await _db.Cards.Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task<List<Card>> GetForAccountAsync(int accountId)
        {
            return await _db.Cards.Where(c => c.AccountId == accountId).ToListAsync();
        }

        public async Task<Card?> GetByIdAsync(int id)
        {
            return await _db.Cards.FindAsync(id);
        }

        public async Task AddAsync(Card card)
        {
            _db.Cards.Add(card);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Card card)
        {
            _db.Cards.Update(card);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var c = await _db.Cards.FindAsync(id);
            if (c != null)
            {
                _db.Cards.Remove(c);
                await _db.SaveChangesAsync();
            }
        }
    }
}