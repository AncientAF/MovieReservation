namespace TicketService.Endpoints.CreateTicket;

public record CreateTicketRequest(Guid SessionId, Guid SeatId, int Number, int Row);

public record CreateTicketResponse(Guid Id);

public class CreateTicketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/tickets", async (CreateTicketRequest request, CancellationToken cancellationToken, ISender sender) =>
            {
                var command = request.Adapt<CreateTicketCommand>();

                var result = await sender.Send(command, cancellationToken);

                var response = result.Adapt<CreateTicketResponse>();

                return Results.Created($"/tickets/{response.Id}", response);
            })
            .WithName("CreateTicket")
            .Produces<CreateTicketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .IncludeInOpenApi();
    }
}