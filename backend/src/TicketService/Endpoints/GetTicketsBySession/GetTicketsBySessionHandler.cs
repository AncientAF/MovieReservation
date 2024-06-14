using TicketService.Exceptions;

namespace TicketService.Endpoints.GetTicketsBySession;

public record GetTicketsBySessionQuery(Guid SessionId) : IQuery<GetTicketsBySessionResult>;

public record GetTicketsBySessionResult(IEnumerable<Ticket> Tickets);

public class GetTicketsBySessionQueryHandler(NpgsqlConnectionFactory connectionFactory)
    : IQueryHandler<GetTicketsBySessionQuery, GetTicketsBySessionResult>
{
    public async Task<GetTicketsBySessionResult> Handle(GetTicketsBySessionQuery request,
        CancellationToken cancellationToken)
    {
        await using var connection = connectionFactory.Create();
        await connection.OpenAsync(cancellationToken);

        const string query = """
                             SELECT * FROM Tickets
                             WHERE SessionId = @SessionId
                             ORDER BY Row, Number
                             """;

        var queryArguments = new
        {
            request.SessionId
        };

        var tickets = (await connection.QueryAsync<Ticket>(query, queryArguments)).ToList();
        await connection.CloseAsync();

        if (tickets.Count == 0)
            throw new TicketNotFoundException(request.SessionId);

        return new GetTicketsBySessionResult(tickets);
    }
}