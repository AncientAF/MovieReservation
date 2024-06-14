namespace TicketService.Endpoints.RefundTicket;

public record RefundTicketRequest(Guid Id);

public record RefundTicketResponse(bool IsSuccess);

public class RefundTicketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch("/tickets/refund/{id:guid}", async (Guid id, CancellationToken cancellationToken, ISender sender) =>
            {
                var command = new RefundTicketCommand(id);

                var result = await sender.Send(command, cancellationToken);

                var response = result.Adapt<RefundTicketResponse>();

                return Results.Ok(response);
            })
            .WithName("RefundTicket")
            .Produces<RefundTicketResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .IncludeInOpenApi();
    }
}