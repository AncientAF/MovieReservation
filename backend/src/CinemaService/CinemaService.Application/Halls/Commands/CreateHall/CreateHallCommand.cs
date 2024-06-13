using FluentValidation;

namespace CinemaService.Application.Halls.Commands.CreateHall;

public record CreateHallCommand(Guid CinemaId, string Name, List<SeatDto> Seats) : ICommand<CreateHallResult>;

public record CreateHallResult(Guid Id);

public class CreateHallCommandValidator : AbstractValidator<CreateHallCommand>
{
    public CreateHallCommandValidator()
    {
        RuleFor(c => c.CinemaId).NotEmpty().WithMessage("CinemaId is required");

        RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");

        RuleFor(c => c.Seats).NotEmpty().WithMessage("Seats is required");
    }
}