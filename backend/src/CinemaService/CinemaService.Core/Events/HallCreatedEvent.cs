namespace CinemaService.Core.Events;

public record HallCreatedEvent(Hall Hall) : IDomainEvent;