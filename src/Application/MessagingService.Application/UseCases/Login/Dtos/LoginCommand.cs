using System.Linq;
using MediatR;
using MessagingService.Application.UseCases.Dtos;

namespace MessagingService.Application.UseCases.Login.Dtos
{
    public class LoginCommand:BaseCommand,IRequest<LoginCommandResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        
        public bool IsValid()
        {
            var validationResult = new LoginCommandValidator().Validate(this);
            if (validationResult.Errors.Any())
            {
                SetValidationErrorList(validationResult.Errors.Select(row=> row.ErrorMessage));
            }
            return validationResult.IsValid;
        }
    }
}