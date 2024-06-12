namespace CinemaService.Application.Halls.Commands;

public static class HallCreation
{
    public static Hall CreateHall(Guid cinemaId, string name, List<SeatDto> seats)
    {
        return CreateHall(Guid.NewGuid(), cinemaId, name, seats);
    }

    public static Hall CreateHall(Guid hallId, Guid cinemaId, string name, List<SeatDto> seats)
    {
        var hall = Hall.Create(HallId.Of(hallId), CinemaId.Of(cinemaId), name);

        foreach (var seat in seats)
            hall.Add(SeatId.Of(Guid.NewGuid()), seat.Row, seat.Number);

        return hall;
    }
}