using CinemaService.Application.Cinemas.Commands.DeleteCinema;

namespace CinemaService.API.Endpoints.Cinemas;

//public record DeleteCinemaRequest(Guid Id);

public record DeleteCinemaResponse(bool IsSuccess);

public class DeleteCinema : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/cinemas/{id:guid}", async (Guid id, ISender sender) =>
            {
                var command = new DeleteCinemaCommand(id);

                var result = await sender.Send(command);

                var response = result.Adapt<DeleteCinemaResponse>();

                return Results.Ok(response);
            })
            .IncludeInOpenApi()
            .WithName("DeleteCinema")
            .Produces<DeleteCinemaResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Cinema")
            .WithDescription("Delete Cinema");
    }
}