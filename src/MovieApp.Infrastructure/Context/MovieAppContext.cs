using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Models.Entities;
using MovieApp.Core.Models.Enums;

namespace MovieApp.Infrastructure.Context
{
    public partial class MovieAppContext : DbContext
    {
        public MovieAppContext()
        { }

        public MovieAppContext(DbContextOptions<MovieAppContext> options)
            : base(options)
        { }

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Role
            modelBuilder.Entity<Role>()
            .Property(p => p.Name)
            .HasConversion(
                    mse => mse.ToString(),
                    mse => (RoleEnum)Enum.Parse(typeof(RoleEnum), mse));

            modelBuilder.Entity<Role>()
                        .Property(p => p.Name)
                        .HasColumnType("varchar(5)");

            //User
            modelBuilder.Entity<User>()
                        .HasIndex(m => m.Name)
                        .IsUnique();

            //Seed data
            SeedData(modelBuilder);
        }

        /// <summary>
        /// Seed data in the database.
        /// </summary>
        public void SeedData(ModelBuilder modelBuilder)
        {
            //Role
            Role adminRole = new Role()
            {
                Id = Guid.Parse("71fc7674-18c7-4a01-ad55-fbecdfd7feda"),
                Name = RoleEnum.Admin
            };

            Role userRole = new Role()
            {
                Id = Guid.Parse("89432022-e55f-48fb-92d9-29ccd24d7eca"),
                Name = RoleEnum.User
            };

            modelBuilder.Entity<Role>().HasData(adminRole);
            modelBuilder.Entity<Role>().HasData(userRole);

            //User
            User adminUser = new User()
            {
                Id = Guid.Parse("799b8043-067a-4c67-8175-e28d431e8e8d"),
                Name = "admin",
                Hash = "PLDHFD75aPuvzK2XFZPXpw==.pU451GhsQ0RRi0n5AgDGkJCOXv0o+XeZp0rTlxDsulA=",
                RoleId = adminRole.Id
            };
            //pass: Test123!

            User userUser = new User()
            {
                Id = Guid.Parse("1d700ebe-fcd2-4439-9470-2ff1ee635b7d"),
                Name = "user",
                Hash = "HqWx1NypREjA4NyDYrrDvw==.2aFPO9MsQ2E6FshrYQNB/aXYfFoGNJNoM9b3V080vrA=",
                RoleId = userRole.Id
            };
            //pass: Test1!

            modelBuilder.Entity<User>().HasData(adminUser);
            modelBuilder.Entity<User>().HasData(userUser);
        }
    }
}