namespace MovieService.Movies.GetMovieById;

//public record GetMovieByIdRequest(Guid Id);
public record GetMovieByIdResponse(
    Guid Id,
    string Name,
    string Description,
    string Length,
    IEnumerable<Genre> Genres,
    string PosterUrl);

public class GetMovieByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/movies/{id:guid}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetMovieByIdQuery(id));

                var response = result.Adapt<GetMovieByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetMovieById")
            .Produces<GetMovieByIdResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .IncludeInOpenApi();
    }
}