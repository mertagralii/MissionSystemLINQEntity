using Microsoft.EntityFrameworkCore;
using MissionSystem.Models;

namespace MissionSystem.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<Worker> Workers { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Duty> Duties { get; set; }

        public DbSet<WorkerCategoryDuty> WorkerCategoryDuties { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
