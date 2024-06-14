namespace TicketService.Endpoints.UpdateTicket;

public record UpdateTicketRequest(
    Guid Id,
    Guid UserId,
    Guid SessionId,
    Guid SeatId,
    int Number,
    int Row,
    TicketStatus Status);

public record UpdateTicketResponse(bool IsSuccess);

public class UpdateTicketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/tickets", async (UpdateTicketRequest request, CancellationToken cancellationToken, ISender sender) =>
            {
                var command = request.Adapt<UpdateTicketCommand>();

                var result = await sender.Send(command, cancellationToken);

                var response = result.Adapt<UpdateTicketResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateTicket")
            .Produces<UpdateTicketResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .IncludeInOpenApi();
    }
}