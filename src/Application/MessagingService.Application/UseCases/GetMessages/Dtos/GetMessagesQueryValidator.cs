using FluentValidation;
using MessagingService.Application.UseCases.GetMessages.Dtos;

namespace MessagingService.Application.UseCases.GetMessages.Dtos
{
    public class GetMessagesQueryValidator:AbstractValidator<GetMessagesQuery>
    {
        public GetMessagesQueryValidator()
        {
            RuleFor(x => x.UserFrom)
                .NotNull().WithMessage("UserFrom can not be null.")
                .NotEmpty().WithMessage("UserFrom can not be empty.");
            RuleFor(x => x.UserTo)
                .NotNull().WithMessage("UserTo can not be null.")
                .NotEmpty().WithMessage("UserTo can not be empty.");
        }
    }
}