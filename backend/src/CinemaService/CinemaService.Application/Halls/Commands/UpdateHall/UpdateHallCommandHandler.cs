namespace CinemaService.Application.Halls.Commands.UpdateHall;

public class UpdateHallCommandHandler(IHallRepository hallRepository)
    : ICommandHandler<UpdateHallCommand, UpdateHallResult>
{
    public async Task<UpdateHallResult> Handle(UpdateHallCommand command, CancellationToken cancellationToken)
    {
        var (hallId, cinemaId, name, seats) = command.HallDto;

        var hall = HallCreation.CreateHall(hallId, cinemaId, name, seats);

        await hallRepository.UpdateAsync(hall, cancellationToken);

        return new UpdateHallResult(true);
    }
}