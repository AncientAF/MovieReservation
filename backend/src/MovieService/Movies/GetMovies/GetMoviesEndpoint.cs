using Shared.Pagination;

namespace MovieService.Movies.GetMovies;

public record GetMoviesRequest(PaginationRequest Request);

public record GetMoviesResponse(PaginatedResult<Movie> Movies);

public class GetMoviesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/movies", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var query = new GetMoviesQuery(request);

                var result = await sender.Send(query);

                var response = result.Adapt<GetMoviesResponse>();

                return Results.Ok(response);
            })
            .WithName("GetMovies")
            .Produces<GetMoviesResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .IncludeInOpenApi();
    }
}