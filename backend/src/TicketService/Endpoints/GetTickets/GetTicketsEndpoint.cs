using Shared.Pagination;

namespace TicketService.Endpoints.GetTickets;

public record GetTicketsRequest(PaginationRequest Request);
public record GetTicketsResponse(PaginatedResult<Ticket> Tickets);

public class GetTicketsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/tickets", async ([AsParameters] PaginationRequest request, CancellationToken cancellationToken, ISender sender) =>
            {
                var query = new GetTicketsQuery(request);

                var result = await sender.Send(query, cancellationToken);

                var response = result.Adapt<GetTicketsResponse>();

                return Results.Ok(response);
            })
            .WithName("GetTickets")
            .Produces<GetTicketsResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .IncludeInOpenApi();
    }
}