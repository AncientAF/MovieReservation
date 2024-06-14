namespace TicketService.Endpoints.DeleteTicket;

public record DeleteTicketCommand(Guid Id) : ICommand<DeleteTicketResult>;

public record DeleteTicketResult(bool IsSuccess);

public class DeleteTicketCommandHandler(NpgsqlConnectionFactory connectionFactory)
    : ICommandHandler<DeleteTicketCommand, DeleteTicketResult>
{
    public async Task<DeleteTicketResult> Handle(DeleteTicketCommand command, CancellationToken cancellationToken)
    {
        await using var connection = connectionFactory.Create();
        await connection.OpenAsync(cancellationToken);

        const string query = "DELETE FROM Tickets WHERE id = @Id";
        var queryArguments = new
        {
            command.Id
        };

        var rowsAffected = await connection.ExecuteAsync(query, queryArguments);
        await connection.CloseAsync();

        return new DeleteTicketResult(rowsAffected > 0);
    }
}