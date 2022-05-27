using Microsoft.EntityFrameworkCore;
using Movie.Theater.Enterprises.Models.Entities;

namespace Movie.Theater.Enterprises.Repos.Context
{
    public interface IMovieTheaterDb
    {
        public DbSet<Customer> Customers { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
