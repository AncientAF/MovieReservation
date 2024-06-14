using FluentValidation;

namespace TicketService.Endpoints.CreateTicket;

public record CreateTicketCommand(Guid SessionId, Guid SeatId, int Number, int Row) : ICommand<CreateTicketResult>;

public record CreateTicketResult(Guid Id);

public class CreateTicketCommandValidator : AbstractValidator<CreateTicketCommand>
{
    public CreateTicketCommandValidator()
    {
        RuleFor(t => t.SessionId)
            .NotEmpty().WithMessage("SessionId cannot be empty");

        RuleFor(t => t.SeatId)
            .NotEmpty().WithMessage("SeatId cannot be empty");

        RuleFor(t => t.Row)
            .GreaterThan(0).WithMessage("Row must be greater than zero");
        
        RuleFor(t => t.Number)
            .GreaterThan(0).WithMessage("Number must be greater than zero");
    }
}

public class CreateTicketCommandHandler(NpgsqlConnectionFactory connectionFactory)
    : ICommandHandler<CreateTicketCommand, CreateTicketResult>
{
    public async Task<CreateTicketResult> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        await using var connection = connectionFactory.Create();
        await connection.OpenAsync(cancellationToken);

        const string query = """
                             INSERT INTO Tickets (Id, UserId, SessionId, SeatId, Number, Row, Status)
                             VALUES (@Id, @UserId, @SessionId, @SeatId, @Number, @Row, @Status)
                             ON CONFLICT (Id) DO NOTHING;
                             """;

        var id = Guid.NewGuid();
        var ticket = new Ticket
        {
            Id = id,
            UserId = Guid.Empty,
            Number = request.Number,
            Row = request.Row,
            SessionId = request.SessionId,
            SeatId = request.SeatId,
            Status = TicketStatus.Unreserved
        };

        var res = await connection.ExecuteAsync(query, ticket);
        await connection.CloseAsync();

        if (res == 0) throw new ArgumentException();

        return new CreateTicketResult(id);
    }
}