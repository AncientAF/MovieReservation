namespace CinemaService.Application.Cinemas.Commands.UpdateCinema;

public class UpdateCinemaCommandHandler(ICinemaRepository cinemaRepository)
    : ICommandHandler<UpdateCinemaCommand, UpdateCinemaResult>
{
    public async Task<UpdateCinemaResult> Handle(UpdateCinemaCommand command, CancellationToken cancellationToken)
    {
        var (cinemaId, name, addressDto, halls) = command.CinemaDto;

        var cinema = CinemaCreation.CreateCinema(cinemaId, addressDto, name, halls);

        await cinemaRepository.UpdateAsync(cinema, cancellationToken);

        return new UpdateCinemaResult(true);
    }
}