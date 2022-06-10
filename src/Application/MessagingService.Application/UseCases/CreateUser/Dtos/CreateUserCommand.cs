using System.Collections.Generic;
using System.Linq;
using MediatR;
using MessagingService.Application.UseCases.Dtos;
using MessagingService.Domain.Services.Dtos.Responses.User;

namespace MessagingService.Application.UseCases.CreateUser.Dtos
{
    public class CreateUserCommand:BaseCommand,IRequest<CreateUserCommandResult>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsValid()
        {
            var validationResult = new CreateUserCommandValidator().Validate(this);
            if (validationResult.Errors.Any())
            {
                SetValidationErrorList(validationResult.Errors.Select(row=> row.ErrorMessage));
            }
            return validationResult.IsValid;
        }
    }
}