using FluentValidation;

namespace MessagingService.Application.UseCases.SendMessage.Dtos
{
    public class SendMessageCommandValidator:AbstractValidator<SendMessageCommand>
    {
        public SendMessageCommandValidator()
        {
            RuleFor(x => x.UserFrom)
                .NotNull().WithMessage("UserFrom can not be null.")
                .NotEmpty().WithMessage("UserFrom can not be empty.");
            RuleFor(x => x.UserTo)
                .NotNull().WithMessage("UserTo can not be null.")
                .NotEmpty().WithMessage("UserTo can not be empty.");
            RuleFor(x => x.MessageDetail)
                .NotNull().WithMessage("MessageDetail can not be null.")
                .NotEmpty().WithMessage("MessageDetail can not be empty.");
        }
    }
}