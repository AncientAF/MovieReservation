namespace CinemaService.Core.Events;

public record CinemaCreatedEvent(Cinema Cinema) : IDomainEvent;