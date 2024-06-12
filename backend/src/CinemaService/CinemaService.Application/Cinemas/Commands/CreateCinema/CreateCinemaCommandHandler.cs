namespace CinemaService.Application.Cinemas.Commands.CreateCinema;

public class CreateCinemaCommandHandler(ICinemaRepository cinemaRepository)
    : ICommandHandler<CreateCinemaCommand, CreateCinemaResult>
{
    public async Task<CreateCinemaResult> Handle(CreateCinemaCommand command, CancellationToken cancellationToken)
    {
        var (name, addressDto, hallsDtos) = command;

        var cinemaToCreate = CinemaCreation.CreateCinema(addressDto, name, hallsDtos);

        var result = await cinemaRepository.CreateAsync(cinemaToCreate, cancellationToken);

        return new CreateCinemaResult(result.Value);
    }
}