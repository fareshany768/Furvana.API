using Microsoft.EntityFrameworkCore;
using Furvana.API.Models;

namespace Furvana.API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Pet> Pets { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AdoptionRequest> AdoptionRequests { get; set; }

        internal async Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
