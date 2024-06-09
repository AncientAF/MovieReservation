namespace MovieService.Data;

public class InitialData
{
    public static IEnumerable<Movie> Movies => new List<Movie>
{
    new Movie
    {
        Id = new Guid("970e45a7-2d1a-4ea3-b271-ac8ce1ed72f2"),
        Name = "The Shawshank Redemption",
        Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
        Length = "2h 22m",
        Genres = new List<Genre> { Genre.Drama },
        PosterUrl = "https://example.com/shawshank-redemption-poster.jpg"
    },
    new Movie
    {
        Id = new Guid("a763cc96-e65c-443a-8acb-8208706fd974"),
        Name = "The Godfather",
        Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
        Length = "2h 55m",
        Genres = new List<Genre> { Genre.Crime, Genre.Drama },
        PosterUrl = "https://example.com/the-godfather-poster.jpg"
    },
    new Movie
    {
        Id = new Guid("109ba2d4-0571-4999-a6f6-3c59e498f379"),
        Name = "The Dark Knight",
        Description = "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
        Length = "2h 32m",
        Genres = new List<Genre> { Genre.Action, Genre.Crime, Genre.Drama },
        PosterUrl = "https://example.com/the-dark-knight-poster.jpg"
    },
    new Movie
    {
        Id = new Guid("48263d82-b145-4ab0-948b-ce0db9a15760"),
        Name = "Inception",
        Description = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea in the mind of a CEO.",
        Length = "2h 28m",
        Genres = new List<Genre> { Genre.Action, Genre.SciFi },
        PosterUrl = "https://example.com/inception-poster.jpg"
    },
    new Movie
    {
        Id = new Guid("8c418660-c399-45cc-9504-530e99ade85d"),
        Name = "The Lord of the Rings: The Fellowship of the Ring",
        Description = "A meek Hobbit from the Shire and eight companions set out on a journey to destroy the powerful One Ring and save Middle-earth from the Dark Lord Sauron.",
        Length = "2h 58m",
        Genres = new List<Genre> { Genre.Adventure, Genre.Fantasy },
        PosterUrl = "https://example.com/the-fellowship-of-the-ring-poster.jpg"
    }
};
}