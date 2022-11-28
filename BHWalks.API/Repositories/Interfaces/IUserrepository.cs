using BHWalks.API.Models.Domain;

namespace BHWalks.API.Repositories.Interfaces
{
    public interface IUserrepository
    {
        Task<User> AuthenticateUser(string username, string password);
    }
}
