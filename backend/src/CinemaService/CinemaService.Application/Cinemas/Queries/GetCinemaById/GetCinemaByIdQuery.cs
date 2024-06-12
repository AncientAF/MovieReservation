using Shared.Caching;

namespace CinemaService.Application.Cinemas.Queries.GetCinemaById;

public record GetCinemaByIdQuery(Guid Id) : ICachedQuery<GetCinemaByIdResult>
{
    public string Key => $"cinema-by-id-{Id}";
    public TimeSpan? Expiration => TimeSpan.FromMinutes(3);
}

public record GetCinemaByIdResult(CinemaDto Cinema);