using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace _20201132039_SinavPortali.Models

{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedRoles(modelBuilder);

            modelBuilder.Entity<AppUserCourse>()
                .HasKey(e => new { e.CourseId, e.AppUserId });

            modelBuilder.Entity<AppUserOption>()
                .HasKey(e => new { e.OptionId, e.AppUserId });
        }
        private void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData
                (
                    new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN" },
                    new IdentityRole() { Name = "Student", ConcurrencyStamp = "2", NormalizedName = "STUDENT" }
                );
        }
        public DbSet<Course> Course { get; set; }
        public DbSet<Assessment> Assessment { get; set; }
        public DbSet<Result> Result { get; set; }
        public DbSet<Option> Option { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<AppUserOption> AppUserOption { get; set; }
        public DbSet <AppUserCourse> AppUserCourse { get; set; }
    }
}


