namespace CinemaService.Core.ValueObjects;

public record CinemaName
{
    private CinemaName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static CinemaName Of(string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);

        return new CinemaName(value);
    }
}