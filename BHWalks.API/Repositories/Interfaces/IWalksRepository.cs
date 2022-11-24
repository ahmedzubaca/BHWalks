using BHWalks.API.Models.Domain;

namespace BHWalks.API.Repositories.Interfaces
{
    public interface IWalksRepository
    {
       Task<IEnumerable<Walk>> GetAllWalks();
        Task<Walk> GetWalkById(Guid id);
        Task<Walk> AddWalk(Walk walk);
        Task<bool> DeleteWalk(Guid id);
        Task<Walk> UpdateWalk(Guid id, Walk walk);
    }
}
