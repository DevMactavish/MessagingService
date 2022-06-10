using System.Linq;
using MediatR;
using MessagingService.Application.UseCases.Dtos;

namespace MessagingService.Application.UseCases.SendMessage.Dtos
{
    public class SendMessageCommand:BaseCommand,IRequest<SendMessageCommandResult>
    {
        public string MessageDetail { get; set; }
        public string UserFrom { get; set; }
        public string UserTo { get; set; }
        
        public bool IsValid()
        {
            var validationResult = new SendMessageCommandValidator().Validate(this);
            if (validationResult.Errors.Any())
            {
                SetValidationErrorList(validationResult.Errors.Select(row=> row.ErrorMessage));
            }
            return validationResult.IsValid;
        }
    }
    
}