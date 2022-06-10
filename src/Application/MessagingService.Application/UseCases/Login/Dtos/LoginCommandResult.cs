using System;
using MessagingService.Application.UseCases.Dtos;

namespace MessagingService.Application.UseCases.Login.Dtos
{
    public class LoginCommandResult:BaseCommandResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public DateTime? Expiration { get; set; }
    }
}