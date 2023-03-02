using Microsoft.EntityFrameworkCore;

namespace MovieApp.Infrastructure.Context
{
    public partial class MovieAppContext : DbContext
    {
        public MovieAppContext()
        { }

        public MovieAppContext(DbContextOptions<MovieAppContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data
            SeedData(modelBuilder);
        }

        /// <summary>
        /// Seed data in the database.
        /// </summary>
        public void SeedData(ModelBuilder modelBuilder)
        {

        }
    }
}