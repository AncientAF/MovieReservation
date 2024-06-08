namespace MovieService.Movies.GetMovieById;

public record GetMovieByIdQuery(Guid Id) : IQuery<GetMovieByIdResult>;

public record GetMovieByIdResult(
    Guid Id,
    string Name,
    string Description,
    string Length,
    IEnumerable<Genre> Genres,
    string PosterUrl);

public class GetMovieByIdCommandHandler(MongoDbService dbService)
    : IQueryHandler<GetMovieByIdQuery, GetMovieByIdResult> 
{
    public async Task<GetMovieByIdResult> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await dbService.GetAsync(request.Id, cancellationToken);

        return result.Adapt<GetMovieByIdResult>();
    }
}