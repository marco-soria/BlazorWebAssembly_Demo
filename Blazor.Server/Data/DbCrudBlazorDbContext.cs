using Blazor.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.Data
{
    public class DbCrudBlazorDbContext : DbContext
    {
        public DbCrudBlazorDbContext(DbContextOptions<DbCrudBlazorDbContext> options) : base(options)
        {

        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.IdDepartment).HasName("PK__Department__123456");
                entity.ToTable("Departamento");
                entity.Property(e => e.Name)
                    .HasMaxLength(50);             
            });
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.IdEmployee).HasName("PK__Employee__CEA18963214");
                entity.ToTable("Empleado");
                entity.Property(e => e.FullName)
                .HasMaxLength(50);
                entity.HasOne(d => d.IdDepartmentNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(e => e.IdDepartment)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Employee_IdDepartment__015983");
            });
        }
    }
}
