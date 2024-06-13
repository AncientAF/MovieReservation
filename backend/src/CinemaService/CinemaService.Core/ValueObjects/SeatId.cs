namespace CinemaService.Core.ValueObjects;

public record SeatId
{
    private SeatId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static SeatId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty) throw new DomainException("SeatId cannot be empty");

        return new SeatId(value);
    }
}