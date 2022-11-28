using System.ComponentModel.DataAnnotations.Schema;

namespace BHWalks.API.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public List<string> Roles { get; set; }

        //Nav property
        public List<User_Role> UserRoles { get; set; }
    }
}
