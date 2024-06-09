using Shared.Caching;
using Shared.CQRS;

namespace MovieService.Movies.UpdateMovie;

public record UpdateMovieCommand(
    Guid Id,
    string Name,
    string Description,
    string Length,
    IEnumerable<Genre> Genres,
    string PosterUrl) : IInvalidateCacheCommand<UpdateMovieResult>
{
    public string[] Keys => [$"movie-by-id-{Id}"];
}

public record UpdateMovieResult(bool IsSuccess);

public class UpdateMovieCommandHandler(MongoDbService dbService)
    : ICommandHandler<UpdateMovieCommand, UpdateMovieResult>
{
    public async Task<UpdateMovieResult> Handle(UpdateMovieCommand command, CancellationToken cancellationToken)
    {
        var result = await dbService.UpdateAsync(command.Adapt<Movie>(), cancellationToken);

        return new UpdateMovieResult(result);
    }
}