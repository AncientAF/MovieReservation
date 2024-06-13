namespace CinemaService.Application.Halls.Commands.DeleteHall;

public class DeleteHallCommandHandler(IHallRepository hallRepository)
    : ICommandHandler<DeleteHallCommand, DeleteHallResult>
{
    public async Task<DeleteHallResult> Handle(DeleteHallCommand command, CancellationToken cancellationToken)
    {
        await hallRepository.DeleteAsync(HallId.Of(command.Id), cancellationToken);

        return new DeleteHallResult(true);
    }
}