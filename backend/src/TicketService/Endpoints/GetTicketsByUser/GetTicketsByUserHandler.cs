using TicketService.Exceptions;

namespace TicketService.Endpoints.GetTicketsByUser;

public record GetTicketsByUserQuery(Guid UserId) : IQuery<GetTicketsByUserResult>;

public record GetTicketsByUserResult(IEnumerable<Ticket> Tickets);

public class GetTicketsByUserQueryHandler(NpgsqlConnectionFactory connectionFactory)
    : IQueryHandler<GetTicketsByUserQuery, GetTicketsByUserResult>
{
    public async Task<GetTicketsByUserResult> Handle(GetTicketsByUserQuery request, CancellationToken cancellationToken)
    {
        await using var connection = connectionFactory.Create();
        await connection.OpenAsync(cancellationToken);

        const string query = """
                             SELECT * FROM Tickets 
                             WHERE userid = @UserId
                             ORDER BY SessionId, Row, Number
                             """;

        var queryArguments = new
        {
            request.UserId
        };

        var result = (await connection.QueryAsync<Ticket>(query, queryArguments)).ToList();
        await connection.CloseAsync();

        if (result.Count == 0) throw new TicketNotFoundException("UserId", request.UserId);

        return new GetTicketsByUserResult(result);
    }
}