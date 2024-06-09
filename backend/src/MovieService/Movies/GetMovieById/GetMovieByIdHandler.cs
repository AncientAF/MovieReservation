using MovieService.Exceptions;
using Shared.Caching;
using Shared.CQRS;

namespace MovieService.Movies.GetMovieById;

public record GetMovieByIdQuery(Guid Id) : ICachedQuery<GetMovieByIdResult>
{
    public string Key => $"movie-by-id-{Id}";
    public TimeSpan? Expiration => default;
}

public record GetMovieByIdResult(
    Guid Id,
    string Name,
    string Description,
    string Length,
    IEnumerable<Genre> Genres,
    string PosterUrl);

public class GetMovieByIdQueryHandler(MongoDbService dbService)
    : IQueryHandler<GetMovieByIdQuery, GetMovieByIdResult>
{
    public async Task<GetMovieByIdResult> Handle(GetMovieByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await dbService.GetAsync(query.Id, cancellationToken);

        if (result == null)
            throw new MovieNotFoundException(query.Id);

        return result.Adapt<GetMovieByIdResult>();
    }
}