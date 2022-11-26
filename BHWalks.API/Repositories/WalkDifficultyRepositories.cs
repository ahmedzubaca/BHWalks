using BHWalks.API.Data;
using BHWalks.API.Models.Domain;
using BHWalks.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BHWalks.API.Repositories
{
    public class WalkDifficultyRepositories : IWalkDifficultiesRepository
    {
        private readonly BHWalksDbContext _db;

        public WalkDifficultyRepositories(BHWalksDbContext db)
        {
            _db = db;
        }       

        public async Task<IEnumerable<WalkDifficulty>> GetAllWalkDiff()
        {
            var walkDiffs = await _db.WalkDifficulties.ToListAsync();
            if(walkDiffs.Any())
            {
                return walkDiffs;
            }
            return null!;
        }

        public async Task<WalkDifficulty> GetWalkDiffById(Guid id)
        {
            var walkDiff = await _db.WalkDifficulties.FindAsync(id);
            if(walkDiff == null)
            {
                return null!;
            }
            return walkDiff;
        }

        public async Task<WalkDifficulty> AddWalkDiff(WalkDifficulty walkDiff)
        {
            walkDiff.Id = Guid.NewGuid();
            await _db.WalkDifficulties.AddAsync(walkDiff);
            await _db.SaveChangesAsync();
            return walkDiff;
        }

        public async Task<WalkDifficulty> UpdateWalkDiff(Guid id, WalkDifficulty walkDiff)
        {
            var walkDiffDb = await _db.WalkDifficulties.FindAsync(id);
            if(walkDiffDb == null)
            {
                return null!;
            }
            walkDiffDb.DifficultyCode = walkDiff.DifficultyCode;
            await _db.SaveChangesAsync();
            return walkDiffDb!;
        }
        public async Task<WalkDifficulty> DeleteWalkDiff(Guid id)
        {
            var walkDiffDb = await _db.WalkDifficulties.FindAsync(id);
            if(walkDiffDb == null)
            {
                return null!;
            }
            _db.WalkDifficulties.Remove(walkDiffDb);
            await _db.SaveChangesAsync();
            return walkDiffDb;
        } 
    }
}
