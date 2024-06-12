using Shared.Pagination;

namespace CinemaService.Application.Cinemas.Queries.GetCinemas;

public class GetCinemasQueryHandler(ICinemaRepository cinemaRepository)
    : IQueryHandler<GetCinemasQuery, GetCinemasResult>
{
    public async Task<GetCinemasResult> Handle(GetCinemasQuery query, CancellationToken cancellationToken)
    {
        var paginated = await cinemaRepository.GetPaginatedAsync(query.PaginationRequest.PageSize,
            query.PaginationRequest.PageIndex, cancellationToken);

        var result = new PaginatedResult<Cinema>(query.PaginationRequest.PageIndex,
            query.PaginationRequest.PageSize, paginated.count, paginated.cinemas);

        return new GetCinemasResult(result);
    }
}