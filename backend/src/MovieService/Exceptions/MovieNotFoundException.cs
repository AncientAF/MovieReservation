using Shared.Exceptions;

namespace MovieService.Exceptions;

public class MovieNotFoundException(object key) : NotFoundException("Movie", key);