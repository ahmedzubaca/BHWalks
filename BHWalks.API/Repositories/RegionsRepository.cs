using BHWalks.API.Data;
using BHWalks.API.Models.Domain;
using BHWalks.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace BHWalks.API.Repositories
{
    public class RegionsRepository : IRegionsRepository
    {
        private readonly BHWalksDbContext _db;

        public RegionsRepository(BHWalksDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Region>> GetAll()
        {
            return await _db.Regions
                .Include(x => x.Walks)
                .ToListAsync();
        }

        public async Task<Region> GetRegion(Guid id)
        {
            return await _db.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Region> AddRegion(Region region)
        {   
            region.Id= Guid.NewGuid();
            await _db.Regions.AddAsync(region);
            await _db.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteRegion(Guid id)
        {
            var region = await _db.Regions.FirstOrDefaultAsync(r=> r.Id == id);
            if (region == null)
            {
                return null;
            }
            _db.Regions.Remove(region);
            await _db.SaveChangesAsync();
            return region;
        }        

        public async Task<Region> UpdateRegion(Guid id, Region region)
        {
            var existingRegion = await _db.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            
            existingRegion.Name = region.Name;
            existingRegion.RegionCode= region.RegionCode;
            existingRegion.Area = region.Area;
            existingRegion.Latitude = region.Latitude;
            existingRegion.Longitude = region.Longitude;
            existingRegion.Population = region.Population;

            await _db.SaveChangesAsync();
            return existingRegion;
        }
    }
}
