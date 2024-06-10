namespace CinemaService.Core.Events;

public record CinemaUpdatedEvent(Cinema Cinema) : IDomainEvent;