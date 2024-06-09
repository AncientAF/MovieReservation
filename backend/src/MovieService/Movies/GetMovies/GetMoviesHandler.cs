using Shared.CQRS;
using Shared.Pagination;

namespace MovieService.Movies.GetMovies;

public record GetMoviesQuery(PaginationRequest Request) : IQuery<GetMoviesResult>;

public record GetMoviesResult(PaginatedResult<Movie> Movies);

public class GetMoviesQueryHandler(MongoDbService dbService)
    : IQueryHandler<GetMoviesQuery, GetMoviesResult>
{
    public async Task<GetMoviesResult> Handle(GetMoviesQuery query, CancellationToken cancellationToken)
    {
        var result = await dbService.GetAsync(query.Request, cancellationToken);

        return new GetMoviesResult(result);
    }
}