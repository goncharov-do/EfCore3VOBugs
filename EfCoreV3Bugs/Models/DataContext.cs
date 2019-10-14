using Microsoft.EntityFrameworkCore;

namespace EfCoreV3Bugs.Models
{
    public class DataContext: DbContext
    {
        public static string ConnectionString { get; set; }
        public DataContext() : base()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
        public DbSet<TestEntity> TestEntities { get; set; }
    }
}
