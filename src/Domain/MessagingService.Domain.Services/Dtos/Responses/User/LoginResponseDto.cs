using System;

namespace MessagingService.Domain.Services.Dtos.Responses.User
{
    public class LoginResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}