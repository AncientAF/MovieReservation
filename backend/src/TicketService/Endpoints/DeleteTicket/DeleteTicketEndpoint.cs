namespace TicketService.Endpoints.DeleteTicket;

public record DeleteTicketRequest(Guid Id);

public record DeleteTicketResponse(bool IsSuccess);

public class DeleteTicketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/tickets/{id:Guid}", async (Guid id, CancellationToken cancellationToken, ISender sender) =>
            {
                var command = new DeleteTicketCommand(id);

                var result = await sender.Send(command, cancellationToken);

                var response = result.Adapt<DeleteTicketResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteTicket")
            .Produces<DeleteTicketResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .IncludeInOpenApi();
    }
}