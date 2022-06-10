using FluentValidation;

namespace MessagingService.Application.UseCases.CreateUser.Dtos
{
    public class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(row => row.FirstName)
                .NotNull().WithMessage("FirstName can not be null")
                .NotEmpty().WithMessage("FirstName can not be empty");
            
            RuleFor(c => c.Username)
                .NotNull().WithMessage("Username can not be null")
                .NotEmpty().WithMessage("Username can not be empty");

            RuleFor(c => c.Password)
                .NotNull().WithMessage("Password can not be not null")
                .NotEmpty().WithMessage("Password can not be not empty");

            RuleFor(c => c.LastName)
                .NotNull().WithMessage("LastName can not be not null")
                .NotEmpty().WithMessage("LastName can not be not empty");
        }
    }
}