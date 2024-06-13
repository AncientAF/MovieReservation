using CinemaService.Application.Extensions;
using Shared.Pagination;

namespace CinemaService.Application.Cinemas.Queries.GetCinemas;

public class GetCinemasQueryHandler(ICinemaRepository cinemaRepository)
    : IQueryHandler<GetCinemasQuery, GetCinemasResult>
{
    public async Task<GetCinemasResult> Handle(GetCinemasQuery query, CancellationToken cancellationToken)
    {
        var paginated = await cinemaRepository.GetPaginatedAsync(query.Request.PageSize,
            query.Request.PageIndex, cancellationToken);

        var result = new PaginatedResult<CinemaDto>(query.Request.PageIndex,
            query.Request.PageSize, paginated.count, paginated.cinemas.ToDto());

        return new GetCinemasResult(result);
    }
}