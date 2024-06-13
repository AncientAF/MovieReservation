using Shared.Pagination;

namespace CinemaService.Application.Cinemas.Queries.GetCinemas;

public record GetCinemasQuery(PaginationRequest Request) : IQuery<GetCinemasResult>;

public record GetCinemasResult(PaginatedResult<CinemaDto> Cinemas);