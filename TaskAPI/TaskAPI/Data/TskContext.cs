using Microsoft.EntityFrameworkCore;
using TaskAPI.Model;

namespace TaskAPI.Data
{
    public class TskContext :DbContext
    {
        public TskContext(DbContextOptions<TskContext> options)
          : base(options)
        {
        }

        public DbSet<Organization> Organizations { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<UserTask> Tasks { get; set; } = null!;

    }
}
