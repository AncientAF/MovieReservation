namespace TicketService.Endpoints.GetTicketsByUser;

public record GetTicketsByUserRequest(Guid UserId);

public record GetTicketsByUserResponse(IEnumerable<Ticket> Tickets);

public class GetTicketsByUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/tickets/user/{id:guid}", async (Guid id, CancellationToken cancellationToken, ISender sender) =>
            {
                var query = new GetTicketsByUserQuery(id);

                var result = await sender.Send(query, cancellationToken);

                var response = result.Adapt<GetTicketsByUserResponse>();

                return Results.Ok(response);
            })
            .WithName("GetTicketsByUser")
            .Produces<GetTicketsByUserResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .IncludeInOpenApi();
    }
}