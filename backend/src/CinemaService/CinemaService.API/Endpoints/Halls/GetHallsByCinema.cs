using CinemaService.Application.Halls.Queries.GetHallsByCinema;

namespace CinemaService.API.Endpoints.Halls;

//public record GetHallsByCinemaRequest(Guid Id);
public record GetHallsByCinemaResponse(IEnumerable<HallDto> Halls);

public class GetHallsByCinema : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/cinemas/{id:guid}/halls", async (Guid id, ISender sender) =>
            {
                var query = new GetHallsByCinemaQuery(id);

                var result = await sender.Send(query);

                var response = result.Adapt<GetHallsByCinemaResponse>();

                return Results.Ok(response);
            })
            .IncludeInOpenApi()
            .WithName("GetHallsByCinema")
            .Produces<GetHallsByCinemaResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Halls By Cinema")
            .WithDescription("Get Halls By Cinema");
    }
}