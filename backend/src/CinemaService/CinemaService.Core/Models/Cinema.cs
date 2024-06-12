namespace CinemaService.Core.Models;

public class Cinema : Aggregate<CinemaId>
{
    private readonly List<Hall> _halls = [];
    public string Name { get; private set; } = null!;
    public Address Address { get; private set; } = null!;
    public IReadOnlyList<Hall> Halls => _halls.AsReadOnly();

    public static Cinema Create(CinemaId id, string name, Address address)
    {
        var cinema = new Cinema
        {
            Id = id,
            Name = name,
            Address = address
        };

        cinema.AddDomainEvent(new CinemaCreatedEvent(cinema));

        return cinema;
    }

    public void Update(string name, Address address, IEnumerable<Hall> halls)
    {
        Name = name;
        Address = address;
        
        _halls.Clear();
        _halls.AddRange(halls);

        AddDomainEvent(new CinemaUpdatedEvent(this));
    }

    public void Add(Hall hall)
    {
        _halls.Add(hall);
    }

    public void Remove(HallId hallId)
    {
        var hallToRemove = _halls.FirstOrDefault(h => h.Id == hallId);
        if (hallToRemove != null)
            _halls.Remove(hallToRemove);
    }
}