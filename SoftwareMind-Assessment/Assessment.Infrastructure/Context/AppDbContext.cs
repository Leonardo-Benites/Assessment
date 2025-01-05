using Assessment.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Assessment.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Department> Department { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
             .HasOne(e => e.Department) 
             .WithMany(d => d.Employees) 
             .HasForeignKey(e => e.DepartmentId)  
             .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Employee>()
                .Property(e => e.Phone) 
                .HasMaxLength(15)
                .IsRequired();

            modelBuilder.Entity<Employee>()
                .Property(e => e.Address)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Employee>()
                .Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Employee>()
                .Property(e => e.LastName)
                .HasMaxLength(50);

            modelBuilder.Entity<Department>();
        }
    }
}
