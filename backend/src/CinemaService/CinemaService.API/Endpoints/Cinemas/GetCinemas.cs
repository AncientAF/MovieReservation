using CinemaService.Application.Cinemas.Queries.GetCinemas;
using Shared.Pagination;

namespace CinemaService.API.Endpoints.Cinemas;

public record GetCinemasRequest(PaginationRequest Request);

public record GetCinemasResponse(PaginatedResult<CinemaDto> Cinemas);

public class GetCinemas : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/cinemas", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var query = new GetCinemasQuery(request);

                var result = await sender.Send(query);

                var response = result.Adapt<GetCinemasResponse>();

                return Results.Ok(response);
            })
            .IncludeInOpenApi()
            .WithName("GetCinemasPaginated")
            .Produces<GetCinemasResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Cinemas Paginated")
            .WithDescription("Get Cinemas Paginated");
    }
}