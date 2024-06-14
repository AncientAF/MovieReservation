namespace TicketService.Endpoints.BuyTicket;

public record BuyTicketRequest(Guid TicketId, Guid UserId);

public record BuyTicketResponse(bool IsSuccess);

public class BuyTicketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch("/tickets/buy", async ([AsParameters] BuyTicketRequest request, CancellationToken cancellationToken, ISender sender) =>
            {
                var command = request.Adapt<BuyTicketCommand>();

                var result = await sender.Send(command, cancellationToken);

                var response = result.Adapt<BuyTicketResponse>();

                return Results.Ok(response);
            })
            .WithName("BuyTicket")
            .Produces<BuyTicketResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .IncludeInOpenApi();
    }
}