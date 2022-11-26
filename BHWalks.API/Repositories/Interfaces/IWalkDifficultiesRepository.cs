using BHWalks.API.Models.Domain;

namespace BHWalks.API.Repositories.Interfaces
{
    public interface IWalkDifficultiesRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAllWalkDiff();
        Task<WalkDifficulty> GetWalkDiffById(Guid id);
        Task<WalkDifficulty> AddWalkDiff(WalkDifficulty walkDiff);
        Task<WalkDifficulty> UpdateWalkDiff(Guid id, WalkDifficulty walkDiff);
        Task<WalkDifficulty> DeleteWalkDiff(Guid id);
    }
}
