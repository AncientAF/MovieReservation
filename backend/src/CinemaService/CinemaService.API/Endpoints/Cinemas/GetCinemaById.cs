using CinemaService.Application.Cinemas.Queries.GetCinemaById;

namespace CinemaService.API.Endpoints.Cinemas;

//public record GetCinemaByIdRequest(Guid Id);

public record GetCinemaByIdResponse(CinemaDto Cinema);

public class GetCinemaById : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/cinemas/{id:guid}", async (Guid id, ISender sender) =>
            {
                var query = new GetCinemaByIdQuery(id);

                var result = await sender.Send(query);

                var response = result.Adapt<GetCinemaByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetCinemaById")
            .Produces<GetCinemaByIdResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Cinema By Id")
            .WithDescription("Get Cinema By Id");
    }
}