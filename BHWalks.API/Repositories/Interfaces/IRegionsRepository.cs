using BHWalks.API.Models.Domain;
using System.Collections;

namespace BHWalks.API.Repositories.Interfaces
{
    public interface IRegionsRepository
    {
        Task<IEnumerable<Region>> GetAll();
        Task<Region> GetRegion(Guid id);
        Task<Region> AddRegion(Region region);
        Task<Region>DeleteRegion(Guid id);
        Task<Region> UpdateRegion(Guid id, Region region);
    }
}
