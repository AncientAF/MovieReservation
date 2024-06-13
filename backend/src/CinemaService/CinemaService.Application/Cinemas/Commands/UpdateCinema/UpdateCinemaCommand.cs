using FluentValidation;
using Shared.Caching;

namespace CinemaService.Application.Cinemas.Commands.UpdateCinema;

public record UpdateCinemaCommand(CinemaDto Cinema) : IInvalidateCacheCommand<UpdateCinemaResult>
{
    public string[] Keys => [$"cinema-id-{Cinema.Id}"];
}

public record UpdateCinemaResult(bool IsSuccess);

public class UpdateCinemaCommandValidator : AbstractValidator<UpdateCinemaCommand>
{
    public UpdateCinemaCommandValidator()
    {
        RuleFor(c => c.Cinema.Id).NotEmpty().WithMessage("Id is required");

        RuleFor(c => c.Cinema.Name).NotEmpty().WithMessage("Name is required");

        RuleFor(c => c.Cinema.Address).NotEmpty().WithMessage("Address is required");
    }
}