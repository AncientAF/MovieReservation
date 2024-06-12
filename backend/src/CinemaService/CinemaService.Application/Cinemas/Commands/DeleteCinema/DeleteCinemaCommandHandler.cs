namespace CinemaService.Application.Cinemas.Commands.DeleteCinema;

public class DeleteCinemaCommandHandler(ICinemaRepository cinemaRepository)
    : ICommandHandler<DeleteCinemaCommand, DeleteCinemaResult>
{
    public async Task<DeleteCinemaResult> Handle(DeleteCinemaCommand command, CancellationToken cancellationToken)
    {
        var cinemaId = CinemaId.Of(command.Id);

        await cinemaRepository.DeleteAsync(cinemaId, cancellationToken);

        return new DeleteCinemaResult(true);
    }
}