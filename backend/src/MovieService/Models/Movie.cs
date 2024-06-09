using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MovieService.Models;

public class Movie
{
    [BsonId]
    [BsonRequired]
    public Guid Id { get; set; }

    [BsonRepresentation(BsonType.String)]
    [BsonRequired]
    public required string Name { get; set; }

    [BsonRepresentation(BsonType.String)]
    [BsonRequired]
    public required string Description { get; set; }

    [BsonRepresentation(BsonType.String)]
    [BsonRequired]
    public required string Length { get; set; }

    public IEnumerable<Genre>? Genres { get; set; }

    [BsonRepresentation(BsonType.String)] public string? PosterUrl { get; set; }
}