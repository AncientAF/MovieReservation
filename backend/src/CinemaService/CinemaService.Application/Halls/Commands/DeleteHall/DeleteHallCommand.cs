using Shared.Caching;

namespace CinemaService.Application.Halls.Commands.DeleteHall;

public record DeleteHallCommand(Guid Id) : IInvalidateCacheCommand<DeleteHallResult>
{
    public string[] Keys => [$"hall-id-{Id}"];
}

public record DeleteHallResult(bool IsSuccess);