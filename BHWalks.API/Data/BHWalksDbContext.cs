using BHWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BHWalks.API.Data
{
    public class BHWalksDbContext : DbContext
    {
        public BHWalksDbContext(DbContextOptions<BHWalksDbContext> options) : base(options)
        {

        }
        public DbSet <Region> Regions { get; set; }
        public DbSet <Walk> Walks { get; set; }
        public DbSet <WalkDifficulty> WalkDifficulties { get; set; }
    }
}
