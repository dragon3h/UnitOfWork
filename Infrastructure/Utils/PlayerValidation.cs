using FluentValidation;
using Infrastructure.DTOs;


public class PlayerValidation : AbstractValidator<PlayerRequest>
{
    public PlayerValidation()
    {
        RuleFor(x => x).NotNull();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(30);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(100);
    }
}