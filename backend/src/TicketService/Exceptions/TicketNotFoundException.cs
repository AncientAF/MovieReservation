using Shared.Exceptions;

namespace TicketService.Exceptions;

public class TicketNotFoundException : NotFoundException
{
    public TicketNotFoundException(object key) : base("Ticket", key)
    {
    }

    public TicketNotFoundException(string keyName, object key) : base("Ticket", keyName, key)
    {
    }
}