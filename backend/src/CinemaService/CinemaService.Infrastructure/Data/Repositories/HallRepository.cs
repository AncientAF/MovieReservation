using CinemaService.Core.Repositories;
using CinemaService.Infrastructure.Exceptions;

namespace CinemaService.Infrastructure.Data.Repositories;

public class HallRepository(IApplicationDbContext dbContext) : IHallRepository
{
    public async Task<Hall> GetByIdAsync(HallId id, CancellationToken cancellationToken)
    {
        var hall = await dbContext.Halls.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id, cancellationToken);

        if (hall == null) throw new HallNotFoundException(id);

        return hall;
    }

    public async Task<List<Hall>> GetByCinemaAsync(CinemaId cinemaId, CancellationToken cancellationToken)
    {
        var halls = await dbContext.Halls.AsNoTracking().Include(h => h.Seats).Where(h => h.CinemaId == cinemaId).ToListAsync(cancellationToken);

        return halls;
    }

    public async Task<(List<Hall> halls, long count)> GetPaginatedAsync(int pageSize, int pageIndex, CancellationToken cancellationToken)
    {
        var halls = await dbContext.Halls.AsNoTracking()
            .Include(h => h.Seats)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var count = await dbContext.Halls.LongCountAsync(cancellationToken);

        return (halls, count);
    }

    public async Task<HallId> CreateAsync(Hall hall, CancellationToken cancellationToken)
    {
        await dbContext.Halls.AddAsync(hall,cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
        
        return hall.Id;
    }

    public async Task UpdateAsync(Hall updatedHall, CancellationToken cancellationToken)
    {
        var hall = await dbContext.Halls.FirstOrDefaultAsync(h => h.Id == updatedHall.Id, cancellationToken);
        
        if(hall == null) throw new HallNotFoundException(updatedHall.Id);

        hall.Update(updatedHall.CinemaId, updatedHall.Name, updatedHall.Seats);
        dbContext.Halls.Update(updatedHall);
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(HallId id, CancellationToken cancellationToken)
    {
        var hall = await dbContext.Halls.FirstOrDefaultAsync(h => h.Id == id, cancellationToken);

        if (hall == null) throw new HallNotFoundException(id);
        
        dbContext.Halls.Remove(hall);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}