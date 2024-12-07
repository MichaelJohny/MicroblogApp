using FluentValidation;

namespace Application.Features.Account.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(e => e.UserName).NotEmpty().NotNull();
        RuleFor(e => e.Email).NotEmpty().NotNull();
        RuleFor(e => e.Password).NotEmpty()
            .NotNull()
            .Must(MatchConfirmPassword)
            .WithMessage("Password should match confirm Password");
    }

    private bool MatchConfirmPassword(RegisterCommand model, string password)
        => model.ConfirmPassword == password;

}