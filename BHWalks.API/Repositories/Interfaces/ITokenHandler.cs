using BHWalks.API.Models.Domain;

namespace BHWalks.API.Repositories.Interfaces
{
    public interface ITokenHandler
    {
        Task<string> CreateToken(User user);
    }
}
