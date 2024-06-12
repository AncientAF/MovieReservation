namespace CinemaService.Application.Extensions;

public static class CinemaExtensions
{
    public static CinemaDto ToDto(this Cinema cinema)
    {
        return new CinemaDto
        (
            cinema.Id.Value,
            cinema.Name,
            cinema.Address.ToDto(),
            cinema.Halls.ToDto()
        );
    }
}