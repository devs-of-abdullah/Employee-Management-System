namespace Data
{
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<DepartmentEntity> Departments { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<EmployeeDepartmentEntity> EmployeeDepartments { get; set; }
        public DbSet<EmployeeRoleEntity> EmployeeRoles { get; set; }

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
            });

            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(256);
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.PasswordHash).IsRequired().HasMaxLength(255);
            });

            modelBuilder.Entity<DepartmentEntity>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Name).IsRequired().HasMaxLength(100);
                entity.Property(d => d.Description).HasMaxLength(500);
            });

            modelBuilder.Entity<RoleEntity>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Name).IsRequired().HasMaxLength(100);
                entity.Property(r => r.Description).HasMaxLength(500);
            });

            modelBuilder.Entity<EmployeeDepartmentEntity>(entity =>
            {
                entity.HasKey(ed => new { ed.EmployeeId, ed.DepartmentId });

                entity.HasOne(ed => ed.Employee)
                    .WithMany(e => e.EmployeeDepartments)
                    .HasForeignKey(ed => ed.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ed => ed.Department)
                    .WithMany(d => d.EmployeeDepartments)
                    .HasForeignKey(ed => ed.DepartmentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<EmployeeRoleEntity>(entity =>
            {
                entity.HasKey(er => new { er.EmployeeId, er.RoleId });

                entity.HasOne(er => er.Employee)
                    .WithMany(e => e.EmployeeRoles)
                    .HasForeignKey(er => er.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(er => er.Role)
                    .WithMany(r => r.EmployeeRoles)
                    .HasForeignKey(er => er.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
