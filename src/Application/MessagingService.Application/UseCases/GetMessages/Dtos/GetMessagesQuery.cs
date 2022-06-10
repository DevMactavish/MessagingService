using System.Linq;
using MediatR;
using MessagingService.Application.UseCases.Dtos;

namespace MessagingService.Application.UseCases.GetMessages.Dtos
{
    public class GetMessagesQuery:BaseCommand,IRequest<GetMessagesQueryResult>
    {
        public string UserFrom { get; set; }
        public string UserTo { get; set; }
        
        public bool IsValid()
        {
            var validationResult = new GetMessagesQueryValidator().Validate(this);
            if (validationResult.Errors.Any())
            {
                SetValidationErrorList(validationResult.Errors.Select(row=> row.ErrorMessage));
            }
            return validationResult.IsValid;
        }
    }
    
}