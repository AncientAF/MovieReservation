using CinemaService.Application.Cinemas.Commands.AddHallsToCinema;

namespace CinemaService.API.Endpoints.Cinemas;

public record AddHallsToCinemaRequest(Guid Id, List<HallDto> Halls);

public record AddHallsToCinemaResponse(bool IsSuccess);

public class AddHallsToCinema : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch("/cinemas", async (AddHallsToCinemaRequest request, ISender sender) =>
            {
                var command = request.Adapt<AddHallsToCinemaCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<AddHallsToCinemaResponse>();

                return Results.Ok(response);
            })
            .IncludeInOpenApi()
            .WithName("AddHallsToCinema")
            .Produces<AddHallsToCinemaResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Add Halls To Cinema")
            .WithDescription("Add Halls To Cinema");
    }
}