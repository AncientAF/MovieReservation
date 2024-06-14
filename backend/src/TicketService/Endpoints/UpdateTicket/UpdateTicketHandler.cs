using FluentValidation;

namespace TicketService.Endpoints.UpdateTicket;

public record UpdateTicketCommand(
    Guid Id,
    Guid UserId,
    Guid SessionId,
    Guid SeatId,
    int Number,
    int Row,
    TicketStatus Status) : ICommand<UpdateTicketResult>;

public record UpdateTicketResult(bool IsSuccess);

public class UpdateTicketCommandValidator : AbstractValidator<UpdateTicketCommand>
{
    public UpdateTicketCommandValidator()
    {
        RuleFor(t => t.Id)
            .NotEmpty().WithMessage("Ticket Id cannot be empty");
        
        RuleFor(t => t.SessionId)
            .NotEmpty().WithMessage("SessionId cannot be empty");

        RuleFor(t => t.SeatId)
            .NotEmpty().WithMessage("SeatId cannot be empty");

        RuleFor(t => t.Row)
            .GreaterThan(0).WithMessage("Row must be greater than zero");
        
        RuleFor(t => t.Number)
            .GreaterThan(0).WithMessage("Number must be greater than zero");

        RuleFor(t => t.Status)
            .Must(s => s == TicketStatus.Reserved).When(t => t.UserId != Guid.Empty)
            .WithMessage("Ticket must be reserved when UserId is present");
    }
}

public class UpdateTicketCommandHandler(NpgsqlConnectionFactory connectionFactory)
    : ICommandHandler<UpdateTicketCommand, UpdateTicketResult>
{
    public async Task<UpdateTicketResult> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
    {
        await using var connection = connectionFactory.Create();
        await connection.OpenAsync(cancellationToken);

        const string query = """
                             UPDATE Tickets
                             SET UserId = @UserId, SessionId = @SessionId, SeatId = @SeatId, Number = @Number, Row = @Row, Status = @Status
                             WHERE Id = @Id
                             """;

        var queryArguments = new
        {
            request.Id,
            request.UserId,
            request.SessionId,
            request.SeatId,
            request.Number,
            request.Row,
            request.Status
        };

        var result = await connection.ExecuteAsync(query, queryArguments);
        await connection.CloseAsync();

        if (result == 0) throw new ArgumentException();

        return new UpdateTicketResult(true);
    }
}