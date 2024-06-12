namespace CinemaService.Application.Cinemas.Commands.AddHallsToCinema;

public record AddHallsToCinemaCommand(Guid Id, List<HallDto> Halls) : ICommand<AddHallsToCinemaResult>;

public record AddHallsToCinemaResult(bool IsSuccess);