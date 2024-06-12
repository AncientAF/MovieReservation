namespace CinemaService.Core.Models;

public class Seat : Entity<SeatId>
{
    internal Seat(SeatId id, HallId hallId, int row, int number)
    {
        Id = id;
        HallId = hallId;
        Row = row;
        Number = number;
    }

    public int Row { get; private set; }
    public int Number { get; private set; }
    public HallId HallId { get; private set; }
}