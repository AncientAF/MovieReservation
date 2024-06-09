using Shared.Exceptions;

namespace MovieService.Exceptions;

public class MovieNotFoundException : NotFoundException
{
    public MovieNotFoundException(object key) : base("Movie", key)
    {
    }
}