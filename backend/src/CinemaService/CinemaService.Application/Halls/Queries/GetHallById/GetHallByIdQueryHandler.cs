using CinemaService.Application.Extensions;

namespace CinemaService.Application.Halls.Queries.GetHallById;

public class GetHallByIdQueryHandler(IHallRepository hallRepository)
    : IQueryHandler<GetHallByIdQuery, GetHallByIdResult>
{
    public async Task<GetHallByIdResult> Handle(GetHallByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await hallRepository.GetByIdAsync(HallId.Of(query.Id), cancellationToken);

        return new GetHallByIdResult(result.ToDto());
    }
}