using Mapster;

namespace MovieService.Movies.CreateMovie;

public record CreateMovieCommand(
    string Name,
    string Description,
    string Length,
    IEnumerable<Genre> Genres,
    string PosterUrl) : ICommand<CreateMovieResult>;
public record CreateMovieResult(Guid Id);

public class CreateMovieCommandHandler(MongoDbService dbService)
    : ICommandHandler<CreateMovieCommand, CreateMovieResult>
{
    public async Task<CreateMovieResult> Handle(CreateMovieCommand command, CancellationToken cancellationToken)
    {
        var result = await dbService.CreateAsync(command.Adapt<Movie>(), cancellationToken);

        return new CreateMovieResult(result);
    }
}

