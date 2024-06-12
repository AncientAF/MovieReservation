using Shared.Caching;

namespace CinemaService.Application.Halls.Queries.GetHallById;

public record GetHallByIdQuery(Guid Id) : ICachedQuery<GetHallByIdResult>
{
    public string Key => $"hall-id-{Id}";
    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}

public record GetHallByIdResult(HallDto Hall);