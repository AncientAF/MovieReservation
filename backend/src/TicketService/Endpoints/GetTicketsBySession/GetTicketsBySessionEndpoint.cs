namespace TicketService.Endpoints.GetTicketsBySession;

public record GetTicketsBySessionRequest(Guid Id);

public record GetTicketsBySessionResponse(IEnumerable<Ticket> Tickets);

public class GetTicketsBySessionEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/tickets/session/{id:guid}", async (Guid id, CancellationToken cancellationToken, ISender sender) =>
            {
                var query = new GetTicketsBySessionQuery(id);

                var result = await sender.Send(query, cancellationToken);

                var response = result.Adapt<GetTicketsBySessionResponse>();

                return Results.Ok(response);
            })
            .WithName("GetTicketsBySession")
            .Produces<GetTicketsBySessionResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .IncludeInOpenApi();
    }
}