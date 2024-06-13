using CinemaService.Application.Cinemas.Commands.UpdateCinema;

namespace CinemaService.API.Endpoints.Cinemas;

public record UpdateCinemaRequest(CinemaDto Cinema);

public record UpdateCinemaResponse(bool IsSuccess);

public class UpdateCinema : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/cinemas", async (UpdateCinemaRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateCinemaCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateCinemaResponse>();

                return Results.Ok(response);
            })
            .IncludeInOpenApi()
            .WithName("UpdateCinema")
            .Produces<UpdateCinemaResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Cinema")
            .WithDescription("Update Cinema");
    }
}