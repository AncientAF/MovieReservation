namespace CinemaService.Application.Dtos;

public record HallDto(Guid Id, Guid CinemaId, string Name, List<SeatDto> Seats);