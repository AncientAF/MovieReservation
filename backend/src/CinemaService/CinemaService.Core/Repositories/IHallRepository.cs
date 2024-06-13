namespace CinemaService.Core.Repositories;

public interface IHallRepository
{
    Task<Hall> GetByIdAsync(HallId id, CancellationToken cancellationToken);
    Task<List<Hall>> GetByCinemaAsync(CinemaId cinemaId, CancellationToken cancellationToken);
    Task<HallId> CreateAsync(Hall hall, CancellationToken cancellationToken);
    Task UpdateAsync(Hall updatedHall, CancellationToken cancellationToken);
    Task DeleteAsync(HallId id, CancellationToken cancellationToken);
}