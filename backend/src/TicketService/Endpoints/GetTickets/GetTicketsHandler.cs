using Shared.Pagination;

namespace TicketService.Endpoints.GetTickets;

public record GetTicketsQuery(PaginationRequest Request) : IQuery<GetTicketsResult>;
public record GetTicketsResult(PaginatedResult<Ticket> Tickets);

public class GetTicketsQueryHandler(NpgsqlConnectionFactory connectionFactory)
    : IQueryHandler<GetTicketsQuery, GetTicketsResult>
{
    public async Task<GetTicketsResult> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
    {
        var (pageIndex, pageSize) = request.Request;
        await using var connection = connectionFactory.Create();
        await connection.OpenAsync(cancellationToken);
        
        const string query = """
                             SELECT * FROM Tickets 
                             ORDER BY SessionId, Row, Number
                             LIMIT @PageSize OFFSET @Offset
                             """;
        var queryArguments = new
        {
            PageSize = pageSize,
            Offset = pageIndex * pageSize
        };
        
        const string countQuery = " SELECT COUNT(*) FROM Tickets;";

        var tickets = await connection.QueryAsync<Ticket>(query, queryArguments);
        var count = await connection.ExecuteScalarAsync<int>(countQuery);
        
        await connection.CloseAsync();

        return new GetTicketsResult(new PaginatedResult<Ticket>(pageIndex, pageSize, count, tickets));
    }
}