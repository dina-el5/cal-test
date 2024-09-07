using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Calendar.Models;


namespace Calendar.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Mission>()
                .HasMany(m => m.Employees)
                .WithMany();
            modelBuilder.Entity<Employee>().HasData(
                    new Employee { Id = 1, Name = "Employee" },
                    new Employee { Id = 2, Name = "Guardian" },
                    new Employee { Id = 3, Name = "Directeur" },
                    new Employee { Id = 4, Name = "Conseiller" }
                    );
        }
    }
}

