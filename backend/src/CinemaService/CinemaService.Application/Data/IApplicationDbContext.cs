using Microsoft.EntityFrameworkCore;

namespace CinemaService.Application.Data;

public interface IApplicationDbContext
{
    public DbSet<Cinema> Cinemas { get; }
    public DbSet<Hall> Halls { get; }
    public DbSet<Seat> Seats { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}