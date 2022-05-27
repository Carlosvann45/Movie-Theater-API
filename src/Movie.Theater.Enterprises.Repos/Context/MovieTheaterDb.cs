using Microsoft.EntityFrameworkCore;
using Movie.Theater.Enterprises.Models.Entities;

namespace Movie.Theater.Enterprises.Repos.Context
{
    public class MovieTheaterDb : DbContext, IMovieTheaterDb
    {
        public MovieTheaterDb(DbContextOptions<MovieTheaterDb> options) : base(options)
        {
            // disables only allowing UTC date time in postgres
            // AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<Customer> Customers { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
