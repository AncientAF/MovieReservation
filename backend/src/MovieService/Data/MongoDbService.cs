using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Shared.Pagination;

namespace MovieService.Data;

public class MongoDbService
{
    private readonly IMongoCollection<Movie> _moviesCollection;

    public MongoDbService(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionUri);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _moviesCollection = database.GetCollection<Movie>(settings.Value.CollectionName);
    }

    public async Task PopulateIfEmpty()
    {
        var count = await _moviesCollection.EstimatedDocumentCountAsync();
        if (count == 0)
            await _moviesCollection.InsertManyAsync(InitialData.Movies);
    }

    public async Task<PaginatedResult<Movie>> GetAsync(PaginationRequest request, CancellationToken cancellationToken)
    {
        var movies = await _moviesCollection.Find(_ => true).SortBy(m => m.Name)
            .Skip(request.PageIndex * request.PageSize).Limit(request.PageSize).ToListAsync(cancellationToken);
        var count = await _moviesCollection.EstimatedDocumentCountAsync(cancellationToken: cancellationToken);

        var result = new PaginatedResult<Movie>(request.PageIndex, request.PageSize, count, movies);

        return result;
    }

    public async Task<Movie?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _moviesCollection.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Guid> CreateAsync(Movie newMovie, CancellationToken cancellationToken)
    {
        await _moviesCollection.InsertOneAsync(newMovie, cancellationToken: cancellationToken);

        return newMovie.Id;
    }

    public async Task<bool> UpdateAsync(Movie updatedMovie, CancellationToken cancellationToken)
    {
        var result = await _moviesCollection.ReplaceOneAsync(x => x.Id == updatedMovie.Id, updatedMovie,
            cancellationToken: cancellationToken);

        return result.IsAcknowledged;
    }

    public async Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _moviesCollection.DeleteOneAsync(x => x.Id == id, cancellationToken);
        
        return result.IsAcknowledged;
    }
}