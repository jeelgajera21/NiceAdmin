using lab_testing.Model;
using Microsoft.EntityFrameworkCore;

namespace lab_testing.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration configuration;
        public ApplicationDbContext(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        // DbSet properties represent collections of the entities.
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Staff> Staff { get; set; }

        // Configure the database connection string.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // SQL Server connection string.
            optionsBuilder.UseSqlServer(this.configuration.GetConnectionString("ConnectionString"));
        }
    }
}
