namespace MovieService.Movies.DeleteMovie;

public record DeleteMovieCommand(Guid Id) : ICommand<DeleteMovieResult>;
public record DeleteMovieResult(bool IsSuccess);

public class DeleteMovieCommandHandler(MongoDbService dbService)
    : ICommandHandler<DeleteMovieCommand, DeleteMovieResult>
{
    public async Task<DeleteMovieResult> Handle(DeleteMovieCommand command, CancellationToken cancellationToken)
    {
        await dbService.RemoveAsync(command.Id, cancellationToken);

        return new DeleteMovieResult(true);
    }
}