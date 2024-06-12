using FluentValidation;
using Shared.Caching;

namespace CinemaService.Application.Cinemas.Commands.UpdateCinema;

public record UpdateCinemaCommand(CinemaDto CinemaDto) : IInvalidateCacheCommand<UpdateCinemaResult>
{
    public string[] Keys => [$"cinema-id-{CinemaDto.Id}"];
}

public record UpdateCinemaResult(bool IsSuccess);

public class UpdateCinemaCommandValidator : AbstractValidator<UpdateCinemaCommand>
{
    public UpdateCinemaCommandValidator()
    {
        RuleFor(c => c.CinemaDto.Id).NotEmpty().WithMessage("Id is required");
        
        RuleFor(c => c.CinemaDto.Name).NotEmpty().WithMessage("Name is required");

        RuleFor(c => c.CinemaDto.Address).NotEmpty().WithMessage("Address is required");
    }
}