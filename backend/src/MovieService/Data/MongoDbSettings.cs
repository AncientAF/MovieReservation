namespace MovieService.Data;

public class MongoDbSettings
{
    public required string ConnectionUri { get; set; }
    public required string DatabaseName { get; set; }
    public required string CollectionName { get; set; }
}