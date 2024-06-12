namespace CinemaService.Application.Dtos;

public record SeatDto(Guid Id, Guid HallId, int Row, int Number);