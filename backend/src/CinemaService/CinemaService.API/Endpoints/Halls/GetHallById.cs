using CinemaService.Application.Halls.Queries.GetHallById;

namespace CinemaService.API.Endpoints.Halls;

//public record GetHallByIdRequest(Guid Id);
public record GetHallByIdResponse(HallDto Hall);

public class GetHallById : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/cinemas/halls/{id:guid}", async (Guid id, ISender sender) =>
            {
                var query = new GetHallByIdQuery(id);

                var result = await sender.Send(query);

                var response = result.Adapt<GetHallByIdResponse>();

                return Results.Ok(response);
            })
            .IncludeInOpenApi()
            .WithName("GetHallById")
            .Produces<GetHallByIdResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Hall By Id")
            .WithDescription("Get Hall By Id");
    }
}