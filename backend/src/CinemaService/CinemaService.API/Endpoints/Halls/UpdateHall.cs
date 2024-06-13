using CinemaService.Application.Halls.Commands.UpdateHall;

namespace CinemaService.API.Endpoints.Halls;

public record UpdateHallRequest(HallDto Hall);

public record UpdateHallResponse(bool IsSuccess);

public class UpdateHall : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/cinemas/halls", async (UpdateHallRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateHallCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateHallResponse>();

                return Results.Ok(response);
            })
            .IncludeInOpenApi()
            .WithName("UpdateHall")
            .Produces<UpdateHallResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Hall")
            .WithDescription("Update Hall");
    }
}