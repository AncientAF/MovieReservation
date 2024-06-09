namespace MovieService.Movies.UpdateMovie;

public record UpdateMovieRequest(
    Guid Id,
    string Name,
    string Description,
    string Length,
    IEnumerable<Genre> Genres,
    string PosterUrl);

public record UpdateMovieResponse(bool IsSuccess);

public class UpdateMovieEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/movies", async (UpdateMovieRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateMovieCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateMovieResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateMovie")
            .Produces<UpdateMovieResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .IncludeInOpenApi();
    }
}