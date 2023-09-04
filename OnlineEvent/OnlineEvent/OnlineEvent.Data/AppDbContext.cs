using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineEvent.Core.Entities;
using OnlineEvent.Core.EntityConst;
using System.Net.Sockets;
using System.Reflection;
using System.Reflection.Emit;

namespace OnlineEvent.Data
{
    public class AppDbContext : IdentityDbContext<AppUser,AppRole,int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<TicketInfo> TicketInfos { get; set; }
        public virtual DbSet<AppUserEvent> AppUserEvents { get; set; }
    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            // Sistem ilk migration yapılırken admin kullanıcını oluşturma
            var adminRole = new AppRole() { Id = 1, Name = "Admin", NormalizedName = "ADMIN" };
            modelBuilder.Entity<AppRole>().HasData(adminRole);

            var hasher = new PasswordHasher<AppUser>();
            var adminUser = new AppUser
            {
                Id = 1,
                Name = "admin",
                Surname = "admin",
                UserType = UserTypes.Admin,
                UserName = "admin@example.com",
                NormalizedUserName = "ADMIN@EXAMPLE.COM",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin123.")
            };
            modelBuilder.Entity<AppUser>().HasData(adminUser);

            var adminUserRole = new IdentityUserRole<int>
            {
                RoleId = adminRole.Id,
                UserId = adminUser.Id
            };

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(adminUserRole);
        }
    }
}
