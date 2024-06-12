using FluentValidation;

namespace CinemaService.Application.Halls.Commands.UpdateHall;

public record UpdateHallCommand(HallDto HallDto) : ICommand<UpdateHallResult>;

public record UpdateHallResult(bool IsSuccess);

public class UpdateHallCommandValidator : AbstractValidator<UpdateHallCommand>
{
    public UpdateHallCommandValidator()
    {
        RuleFor(c => c.HallDto.Id).NotEmpty().WithMessage("Id is required");
        
        RuleFor(c => c.HallDto.CinemaId).NotEmpty().WithMessage("CinemaId is required");
        
        RuleFor(c => c.HallDto.Name).NotEmpty().WithMessage("Name is required");

        RuleFor(c => c.HallDto.Seats).NotEmpty().WithMessage("Seats is required");
    }
}