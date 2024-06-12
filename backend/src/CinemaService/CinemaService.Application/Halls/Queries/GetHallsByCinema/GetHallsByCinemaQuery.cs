namespace CinemaService.Application.Halls.Queries.GetHallsByCinema;

public record GetHallsByCinemaQuery(Guid CinemaId) : IQuery<GetHallsByCinemaResult>;

public record GetHallsByCinemaResult(List<HallDto> Halls);