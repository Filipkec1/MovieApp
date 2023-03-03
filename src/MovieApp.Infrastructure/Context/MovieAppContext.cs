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

        public DbSet<Category> Category { get; set; }
        public DbSet<CategoryMovie> CategoryMovie { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }


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
            //Category
            Category category = new Category()
            {
                Id = Guid.Parse("19f977cc-3916-4a1f-908c-f48700a40880"),
                Name = "Action"
            };

            modelBuilder.Entity<Category>().HasData(category);

            //Movie
            Movie movie = new Movie()
            {
                Id = Guid.Parse("02d3fa37-f439-4e67-a87a-1dcf1d077ad6"),
                Title = "Blade Runner 2049"
            };

            modelBuilder.Entity<Movie>().HasData(movie);

            //CategoryMovie
            CategoryMovie categoryMovie = new CategoryMovie()
            {
                Id = Guid.Parse("74600e89-170c-41b7-8aae-48f8ec08630d"),
                CategoryId = category.Id,
                MovieId = movie.Id
            };

            modelBuilder.Entity<CategoryMovie>().HasData(categoryMovie);

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