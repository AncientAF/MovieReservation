using Shared.Caching;

namespace CinemaService.Application.Cinemas.Commands.DeleteCinema;

public record DeleteCinemaCommand(Guid Id) : IInvalidateCacheCommand<DeleteCinemaResult>
{
    public string[] Keys => [$"cinema-id-{Id}"];
}

public record DeleteCinemaResult(bool IsSuccess);