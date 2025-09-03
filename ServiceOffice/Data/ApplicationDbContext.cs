using Microsoft.EntityFrameworkCore;
using ServiceOffice.Models;

namespace ServiceOffice.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<WorkTask> WorkTasks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ضبط العلاقات وربط المفاتيح الخارجية إن لزم
        }
    }
}