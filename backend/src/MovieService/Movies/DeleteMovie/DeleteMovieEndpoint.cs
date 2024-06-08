namespace MovieService.Movies.DeleteMovie;

public record DeleteMovieRequest(Guid Id);
public record DeleteMovieResponse(bool IsSuccess);

public class DeleteMovieEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/movies/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteMovieCommand(id));

            var response = result.Adapt<DeleteMovieResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteMovie")
        .Produces<DeleteMovieResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound);
    }
}