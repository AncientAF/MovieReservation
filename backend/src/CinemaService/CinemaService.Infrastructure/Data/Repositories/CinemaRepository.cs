using CinemaService.Core.Repositories;
using CinemaService.Infrastructure.Exceptions;

namespace CinemaService.Infrastructure.Data.Repositories;

public class CinemaRepository(IApplicationDbContext dbContext) : ICinemaRepository
{
    public async Task<Cinema> GetByIdAsync(CinemaId id, CancellationToken cancellationToken)
    {
        var cinema = await dbContext.Cinemas
            .AsNoTracking()
            .Include(c => c.Halls)
            .FirstOrDefaultAsync(c => c.Id == id,
                cancellationToken);

        if (cinema == null) throw new CinemaNotFoundException(id);

        return cinema;
    }

    public async Task<(List<Cinema> cinemas, long count)> GetPaginatedAsync(int pageSize, int pageIndex,
        CancellationToken cancellationToken)
    {
        var cinemas = await dbContext.Cinemas
            .OrderBy(c => c.Name)
            .Include(c => c.Halls)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var count = await dbContext.Cinemas.LongCountAsync(cancellationToken);

        return (cinemas, count);
    }

    public async Task<CinemaId> CreateAsync(Cinema cinema, CancellationToken cancellationToken)
    {
        await dbContext.Cinemas.AddAsync(cinema, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return cinema.Id;
    }

    public async Task UpdateAsync(Cinema updatedCinema, CancellationToken cancellationToken)
    {
        var cinema = await dbContext.Cinemas.Include(c => c.Halls)
            .FirstOrDefaultAsync(c => c.Id == updatedCinema.Id, cancellationToken);

        if (cinema == null) throw new CinemaNotFoundException(updatedCinema.Id);

        cinema.Update(updatedCinema.Name, updatedCinema.Address, updatedCinema.Halls);
        dbContext.Cinemas.Update(cinema);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CinemaId id, CancellationToken cancellationToken)
    {
        var cinema = await dbContext.Cinemas.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (cinema == null) throw new CinemaNotFoundException(id);

        dbContext.Cinemas.Remove(cinema);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddHallsToCinemaAsync(CinemaId id, List<Hall> halls, CancellationToken cancellationToken)
    {
        var cinema = await dbContext.Cinemas.Include(c => c.Halls)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (cinema == null) throw new CinemaNotFoundException(id);

        foreach (var hall in halls)
            cinema.Add(hall);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}