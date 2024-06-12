using Shared.Pagination;

namespace CinemaService.Application.Cinemas.Queries.GetCinemas;

public record GetCinemasQuery(PaginationRequest PaginationRequest) : IQuery<GetCinemasResult>;

public record GetCinemasResult(PaginatedResult<Cinema> Cinemas);