namespace CinemaService.Application.Dtos;

public record CinemaDto(Guid Id, string Name, AddressDto Address, List<HallDto> Halls);