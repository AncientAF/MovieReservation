namespace CinemaService.Core.ValueObjects;

public class CinemaId
{
    private CinemaId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static CinemaId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty) throw new DomainException("CinemaId cannot be empty");

        return new CinemaId(value);
    }
}