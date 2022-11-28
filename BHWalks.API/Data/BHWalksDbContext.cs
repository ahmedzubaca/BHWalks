using BHWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BHWalks.API.Data
{
    public class BHWalksDbContext : DbContext
    {
        public BHWalksDbContext(DbContextOptions<BHWalksDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.Role)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(x => x.RoleId);

            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.User)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(x => x.UserId);
        }
        public DbSet <Region> Regions { get; set; }
        public DbSet <Walk> Walks { get; set; }
        public DbSet <WalkDifficulty> WalkDifficulties { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User_Role> User_Roles { get; set; }
    }
}
