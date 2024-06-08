namespace MovieService.Movies.UpdateMovie;

public record UpdateMovieCommand(
    Guid Id,
    string Name,
    string Description,
    string Length,
    IEnumerable<Genre> Genres,
    string PosterUrl) : ICommand<UpdateMovieResult>;
public record UpdateMovieResult(bool IsSuccess);

public class UpdateMovieCommandHandler(MongoDbService dbService)
    : ICommandHandler<UpdateMovieCommand, UpdateMovieResult>
{
    public async Task<UpdateMovieResult> Handle(UpdateMovieCommand command, CancellationToken cancellationToken)
    {
        await dbService.UpdateAsync(command.Adapt<Movie>(), cancellationToken);

        return new UpdateMovieResult(true);
    }
}