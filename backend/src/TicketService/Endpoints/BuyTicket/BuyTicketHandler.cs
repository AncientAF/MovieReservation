using FluentValidation;

namespace TicketService.Endpoints.BuyTicket;

public record BuyTicketCommand(Guid TicketId, Guid UserId) : ICommand<BuyTicketResult>;

public record BuyTicketResult(bool IsSuccess);

public class BuyTicketCommandValidator : AbstractValidator<BuyTicketCommand>
{
    public BuyTicketCommandValidator()
    {
        RuleFor(t => t.UserId)
            .Must(i => i != Guid.Empty).WithMessage("SeatId cannot be empty");
    }
}

public class BuyTicketCommandHandler(NpgsqlConnectionFactory connectionFactory)
    : ICommandHandler<BuyTicketCommand, BuyTicketResult>
{
    public async Task<BuyTicketResult> Handle(BuyTicketCommand request, CancellationToken cancellationToken)
    {
        await using var connection = connectionFactory.Create();
        await connection.OpenAsync(cancellationToken);

        const string query = """
                             UPDATE Tickets
                             SET UserId = @UserId, Status = @Status
                             WHERE Id = @TicketId AND Status != @ReservedStatus
                             """;

        var queryArguments = new
        {
            request.UserId,
            Status = TicketStatus.Reserved,
            request.TicketId,
            ReservedStatus = TicketStatus.Reserved
        };

        var rowsAffected = await connection.ExecuteAsync(query, queryArguments);
        await connection.CloseAsync();

        return new BuyTicketResult(rowsAffected > 0);
    }
}