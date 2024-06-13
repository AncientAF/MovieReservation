namespace CinemaService.API.Endpoints.Cinemas;

public record CreateCinemaRequest(string Name, AddressDto Address, List<HallDto>? Halls);

public record CreateCinemaResponse(Guid Id);

public class CreateCinema : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/cinemas", async (CreateCinemaRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateCinemaRequest>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateCinemaResponse>();

                return Results.Created($"/cinemas/{response.Id}", response);
            })
            .IncludeInOpenApi()
            .WithName("CreateCinema")
            .Produces<CreateCinemaResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Cinema")
            .WithDescription("Create Cinema");
        ;
    }
}