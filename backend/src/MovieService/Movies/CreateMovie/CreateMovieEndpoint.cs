namespace MovieService.Movies.CreateMovie;

public record CreateMovieRequest(
    string Name,
    string Description,
    string Length,
    IEnumerable<Genre> Genres,
    string PosterUrl);

public record CreateMovieResponse(Guid Id);

public class CreateMovieEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateMovieRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateMovieCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateMovieResponse>();

                return Results.Created($"/movies/{response.Id}", response);
            })
            .WithName("CreateMovie")
            .Produces<CreateMovieResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .IncludeInOpenApi();
    }
}