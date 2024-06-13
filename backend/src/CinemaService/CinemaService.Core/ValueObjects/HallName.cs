namespace CinemaService.Core.ValueObjects;

public record HallName
{
    private HallName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static HallName Of(string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);

        return new HallName(value);
    }
}