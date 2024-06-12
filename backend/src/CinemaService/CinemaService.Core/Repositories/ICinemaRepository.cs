namespace CinemaService.Core.Repositories;

public interface ICinemaRepository
{
    Task<Cinema> GetByIdAsync(CinemaId id, CancellationToken cancellationToken);
    Task<(List<Cinema> cinemas, long count)> GetPaginatedAsync(int pageSize, int pageIndex,
        CancellationToken cancellationToken);
    Task<CinemaId> CreateAsync(Cinema cinema, CancellationToken cancellationToken);
    Task UpdateAsync(Cinema updatedCinema, CancellationToken cancellationToken);
    Task DeleteAsync(CinemaId id, CancellationToken cancellationToken);

    Task AddHallsToCinemaAsync(CinemaId id, List<Hall> halls, CancellationToken cancellationToken);
}