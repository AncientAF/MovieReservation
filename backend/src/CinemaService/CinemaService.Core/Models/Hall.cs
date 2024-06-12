namespace CinemaService.Core.Models;

public class Hall : Aggregate<HallId>
{
    private readonly List<Seat> _seats = [];
    public CinemaId CinemaId { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public IReadOnlyList<Seat> Seats => _seats.AsReadOnly();

    public static Hall Create(HallId id, CinemaId cinemaId, string name)
    {
        var hall = new Hall
        {
            Id = id,
            CinemaId = cinemaId,
            Name = name
        };

        hall.AddDomainEvent(new HallCreatedEvent(hall));

        return hall;
    }

    public void Update(CinemaId cinemaId, string name)
    {
        CinemaId = cinemaId;
        Name = name;

        AddDomainEvent(new HallUpdatedEvent());
    }

    public void Add(SeatId id, int row, int number)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(row);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(number);

        var seat = new Seat(id, this.Id, row, number);
        _seats.Add(seat);
    }


    public void Remove(SeatId seatId)
    {
        var seatToRemove = _seats.FirstOrDefault(s => s.Id == seatId);
        if (seatToRemove != null)
            _seats.Remove(seatToRemove);
    }
}