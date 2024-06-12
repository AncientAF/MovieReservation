﻿using MediatR;

namespace CinemaService.Infrastructure.Data.Interceptors;
public class DispatchDomainEventsInterceptor(IPublisher publisher) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await DispatchDomainEvents(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task DispatchDomainEvents(DbContext? context)
    {
        if (context == null) return;

        var aggregates = context.ChangeTracker.Entries<IAggregate>()
                                              .Where(a => a.Entity.DomainEvents.Any())
                                              .Select(a => a.Entity)
                                              .ToList();

        var domainEvents = aggregates.SelectMany(a => a.DomainEvents)
                                     .ToList();

        aggregates.ForEach(a => a.ClearDomainEvents());

        foreach(var domainEvent in domainEvents)
            await publisher.Publish(domainEvent);
    }
}