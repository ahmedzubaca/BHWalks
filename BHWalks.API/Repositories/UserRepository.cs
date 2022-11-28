using BHWalks.API.Data;
using BHWalks.API.Models.Domain;
using BHWalks.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BHWalks.API.Repositories
{
    public class UserRepository : IUserrepository
    {
        private readonly BHWalksDbContext _db;

        public UserRepository(BHWalksDbContext db)
        {
            _db = db;
        }
        public async Task<User> AuthenticateUser(string username, string password)
        {
            var user = await  _db.Users.FirstOrDefaultAsync(
                x => x.UserName == username && x.Password == password);
            if (user == null)
            {
                return null!;
            }
            var userRoles = await _db.User_Roles.Where(
                x => x.UserId == user.Id).ToListAsync();
            if (userRoles.Any()) 
            {
                user.Roles = new List<string>();
                foreach(var userRole in userRoles)
                {
                    var role = await _db.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
                    if(role != null)
                    {
                        user.Roles.Add(role.Name);
                    }
                }
            }
            user.Password = null;
            return user;
        }
    }
}
