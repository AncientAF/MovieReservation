namespace CinemaService.Core.Models;

public class Seat : Entity<SeatId>
{
    internal Seat(SeatId id, int row, int number)
    {
        Id = id;
        Row = row;
        Number = number;
    }

    public int Row { get; private set; }
    public int Number { get; private set; }
}