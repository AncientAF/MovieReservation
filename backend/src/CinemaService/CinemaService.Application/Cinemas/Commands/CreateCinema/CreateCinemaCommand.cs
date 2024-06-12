using FluentValidation;

namespace CinemaService.Application.Cinemas.Commands.CreateCinema;

public record CreateCinemaCommand(string Name, AddressDto Address, List<HallDto>? Halls) : ICommand<CreateCinemaResult>;

public record CreateCinemaResult(Guid Id);

public class CreateCinemaCommandValidator : AbstractValidator<CreateCinemaCommand>
{
    public CreateCinemaCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");

        RuleFor(c => c.Address).NotEmpty().WithMessage("Address is required");
    }
}