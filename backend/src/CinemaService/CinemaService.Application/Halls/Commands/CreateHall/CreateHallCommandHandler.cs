namespace CinemaService.Application.Halls.Commands.CreateHall;

public class CreateHallCommandHandler(IHallRepository hallRepository)
    : ICommandHandler<CreateHallCommand, CreateHallResult>
{
    public async Task<CreateHallResult> Handle(CreateHallCommand command, CancellationToken cancellationToken)
    {
        var (cinemaId, name, seats) = command;

        var hall = HallCreation.CreateHall(cinemaId, name, seats);

        var result = await hallRepository.CreateAsync(hall, cancellationToken);

        return new CreateHallResult(result.Value);
    }
}