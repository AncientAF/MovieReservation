namespace CinemaService.Application.Extensions;

public static class HallExtensions
{
    public static List<HallDto> ToDto(this IEnumerable<Hall> halls)
    {
        return halls.Select(h => new HallDto(
            h.Id.Value,
            h.CinemaId.Value,
            h.Name,
            h.Seats.ToDto())
        ).ToList();
    }

    public static HallDto ToDto(this Hall hall)
    {
        return new HallDto(hall.Id.Value, hall.CinemaId.Value, hall.Name, hall.Seats.ToDto());
    }

    private static List<SeatDto> ToDto(this IEnumerable<Seat> seats)
    {
        return seats.Select(s => new SeatDto(
            s.Id.Value,
            s.HallId.Value,
            s.Row,
            s.Number)).ToList();
    }
}