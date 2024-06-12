using Shared.Exceptions;

namespace CinemaService.Infrastructure.Exceptions;

public class CinemaNotFoundException(object key) : NotFoundException("Cinema", key);