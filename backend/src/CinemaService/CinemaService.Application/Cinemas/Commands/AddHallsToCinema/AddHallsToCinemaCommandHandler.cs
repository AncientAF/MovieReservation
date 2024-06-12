namespace CinemaService.Application.Cinemas.Commands.AddHallsToCinema;

public class AddHallsToCinemaCommandHandler(ICinemaRepository cinemaRepository)
    : ICommandHandler<AddHallsToCinemaCommand, AddHallsToCinemaResult>
{
    public async Task<AddHallsToCinemaResult> Handle(AddHallsToCinemaCommand command,
        CancellationToken cancellationToken)
    {
        var (cinemaId, halls) = command;

        var hallsToAdd = halls.Select(h => Hall.Create(HallId.Of(Guid.NewGuid()), CinemaId.Of(cinemaId), h.Name))
            .ToList();

        await cinemaRepository.AddHallsToCinemaAsync(CinemaId.Of(cinemaId), hallsToAdd, cancellationToken);

        return new AddHallsToCinemaResult(true);
    }
}