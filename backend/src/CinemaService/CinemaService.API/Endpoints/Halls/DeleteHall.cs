using CinemaService.Application.Halls.Commands.DeleteHall;

namespace CinemaService.API.Endpoints.Halls;

//public record DeleteHallRequest(Guid Id);

public record DeleteHallResponse(bool IsSuccess);

public class DeleteHall : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/cinemas/halls/{id:guid}", async (Guid id, ISender sender) =>
            {
                var command = new DeleteHallCommand(id);

                var result = await sender.Send(command);

                var response = result.Adapt<DeleteHallResponse>();

                return Results.Ok(response);
            })
            .IncludeInOpenApi()
            .WithName("DeleteHall")
            .Produces<DeleteHallResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Hall")
            .WithDescription("Delete Hall");
    }
}