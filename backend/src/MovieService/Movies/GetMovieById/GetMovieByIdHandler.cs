using MovieService.Exceptions;
using Shared.CQRS;

namespace MovieService.Movies.GetMovieById;

public record GetMovieByIdQuery(Guid Id) : IQuery<GetMovieByIdResult>;

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