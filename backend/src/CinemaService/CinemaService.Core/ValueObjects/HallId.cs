namespace CinemaService.Core.ValueObjects;

public class HallId
{
    private HallId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static HallId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty) throw new DomainException("HallId cannot be empty");

        return new HallId(value);
    }
}