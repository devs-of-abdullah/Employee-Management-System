using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<DepartmentEntity> Departments { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EmployeeEntity>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(16);

                entity.HasIndex(e => e.Email).IsUnique();

                entity.Property(e => e.Salary).HasColumnType("decimal(18,2)");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(e => e.Department).WithMany(d => d.Employees).HasForeignKey(e => e.DepartmentId).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Role).WithMany(r => r.Employees).HasForeignKey(e => e.RoleId).OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<DepartmentEntity>(entity =>
            {
                entity.HasKey(d => d.Id);

                entity.Property(d => d.Name).IsRequired().HasMaxLength(100);

                entity.Property(d => d.Description).IsRequired().HasMaxLength(500);

                entity.HasMany(d => d.Employees).WithOne(e => e.Department).HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);


            });
            
            modelBuilder.Entity<RoleEntity>(entity =>
            {
                entity.HasKey(r => r.Id);

                entity.Property(r => r.Name).IsRequired().HasMaxLength(50);
                entity.Property(r => r.Description).IsRequired().HasMaxLength(250);


            });

            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);

                entity.HasIndex(u => u.Email).IsUnique();

                entity.Property(u => u.PasswordHash).IsRequired().HasMaxLength(255);
            });



        }
      
    }
}
