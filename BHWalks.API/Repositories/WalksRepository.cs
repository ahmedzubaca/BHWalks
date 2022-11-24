using BHWalks.API.Data;
using BHWalks.API.Models.Domain;
using BHWalks.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BHWalks.API.Repositories
{
    public class WalksRepository : IWalksRepository
    {
        private readonly BHWalksDbContext _db;

        public WalksRepository(BHWalksDbContext db)
        {
            _db= db;
        }
        public async Task<IEnumerable<Walk>> GetAllWalks()
        {
            return await _db.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk> GetWalkById(Guid id)
        {
            var walkToReturn = await _db.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(w =>w.Id == id);

            return walkToReturn!;
        }
        public async Task<Walk> AddWalk(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await _db.Walks.AddAsync(walk);            
            await _db.SaveChangesAsync();
           return(walk);
        }
        public async Task<bool> DeleteWalk(Guid id)
        {            
            var walkToDelete = await _db.Walks.FindAsync(id);
            if (walkToDelete != null)
            {
                _db.Walks.Remove(walkToDelete!);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;                      
        }
        public async Task<Walk> UpdateWalk(Guid id, Walk walk)
        {            
            var walkDb = await _db.Walks.FindAsync(id);
            if(walkDb == null)
            {
                return null;
            } 
            
            walkDb.Name = walk.Name;
            walkDb.RegionId = walk.RegionId;
            walkDb.Length = walk.Length;
            walkDb.WalkDifficultyId = walk.WalkDifficultyId;
            await _db.SaveChangesAsync();
            return walkDb;
        }        
    }
}
