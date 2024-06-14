namespace TicketService.Endpoints.RefundTicket;

public record RefundTicketCommand(Guid Id) : ICommand<RefundTicketResult>;

public record RefundTicketResult(bool IsSuccess);

public class RefundTicketCommandHandler(NpgsqlConnectionFactory connectionFactory)
    : ICommandHandler<RefundTicketCommand, RefundTicketResult>
{
    public async Task<RefundTicketResult> Handle(RefundTicketCommand request, CancellationToken cancellationToken)
    {
        await using var connection = connectionFactory.Create();
        await connection.OpenAsync(cancellationToken);

        const string query =
            "UPDATE Tickets SET userid = @EmptyId, Status = @Unreserved WHERE Id = @Id AND Status = @Reserved";

        var queryArguments = new
        {
            EmptyId = Guid.Empty,
            TicketStatus.Unreserved,
            request.Id,
            TicketStatus.Reserved
        };

        var result = await connection.ExecuteAsync(query, queryArguments);
        await connection.CloseAsync();

        if (result == 0) throw new ArgumentException();

        return new RefundTicketResult(true);
    }
}