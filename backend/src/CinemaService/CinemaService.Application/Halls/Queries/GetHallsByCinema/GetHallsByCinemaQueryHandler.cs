using CinemaService.Application.Extensions;

namespace CinemaService.Application.Halls.Queries.GetHallsByCinema;

public class GetHallsByCinemaQueryHandler(IHallRepository hallRepository)
    : IQueryHandler<GetHallsByCinemaQuery, GetHallsByCinemaResult>
{
    public async Task<GetHallsByCinemaResult> Handle(GetHallsByCinemaQuery query, CancellationToken cancellationToken)
    {
        var halls = await hallRepository.GetByCinemaAsync(CinemaId.Of(query.CinemaId), cancellationToken);

        var result = halls.ToDto();

        return new GetHallsByCinemaResult(result);
    }
}