using CinemaService.Application.Extensions;

namespace CinemaService.Application.Cinemas.Queries.GetCinemaById;

public class GetCinemaByIdQueryHandler(ICinemaRepository cinemaRepository)
    : IQueryHandler<GetCinemaByIdQuery, GetCinemaByIdResult>
{
    public async Task<GetCinemaByIdResult> Handle(GetCinemaByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await cinemaRepository.GetByIdAsync(CinemaId.Of(query.Id), cancellationToken);

        return new GetCinemaByIdResult(result.ToDto());
    }
}