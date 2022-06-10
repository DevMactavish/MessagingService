using FluentValidation;

namespace MessagingService.Application.UseCases.Login.Dtos
{
    public class LoginCommandValidator:AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(c => c.UserName)
                .NotNull().WithMessage("Username can not be null")
                .NotEmpty().WithMessage("Name can not be empty.");
        
            RuleFor(c => c.Password)
                .NotNull().WithMessage("Password can not be null")
                .NotEmpty().WithMessage("Password can not be empty.");
        }
    }
}