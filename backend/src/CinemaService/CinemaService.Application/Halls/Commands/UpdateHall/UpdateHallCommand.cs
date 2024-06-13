using FluentValidation;

namespace CinemaService.Application.Halls.Commands.UpdateHall;

public record UpdateHallCommand(HallDto Hall) : ICommand<UpdateHallResult>;

public record UpdateHallResult(bool IsSuccess);

public class UpdateHallCommandValidator : AbstractValidator<UpdateHallCommand>
{
    public UpdateHallCommandValidator()
    {
        RuleFor(c => c.Hall.Id).NotEmpty().WithMessage("Id is required");

        RuleFor(c => c.Hall.CinemaId).NotEmpty().WithMessage("CinemaId is required");

        RuleFor(c => c.Hall.Name).NotEmpty().WithMessage("Name is required");

        RuleFor(c => c.Hall.Seats).NotEmpty().WithMessage("Seats is required");
    }
}