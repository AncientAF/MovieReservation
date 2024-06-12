using Shared.Exceptions;

namespace CinemaService.Infrastructure.Exceptions;

public class HallNotFoundException(object id) : NotFoundException("Hall", id);