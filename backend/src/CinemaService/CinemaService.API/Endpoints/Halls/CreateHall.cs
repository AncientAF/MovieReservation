using CinemaService.Application.Halls.Commands.CreateHall;

namespace CinemaService.API.Endpoints.Halls;

public record CreateHallRequest(Guid CinemaId, string Name, List<SeatDto> Seats);

public record CreateHallResponse(Guid Id);

public class CreateHall : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/cinemas/halls", async (CreateHallRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateHallCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateHallResponse>();

                return Results.Created($"/cinemas/halls/{response.Id}", response);
            })
            .IncludeInOpenApi()
            .WithName("CreateHall")
            .Produces<CreateHallResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Hall")
            .WithDescription("Create Hall");
    }
}