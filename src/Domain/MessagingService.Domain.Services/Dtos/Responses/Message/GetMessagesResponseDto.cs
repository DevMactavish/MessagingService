using System.Collections.Generic;
using MessagingService.Domain.Dtos;

namespace MessagingService.Domain.Services.Dtos.Responses.Message
{
    public class GetMessagesResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<MessageDto> Messages { get; set; }
    }
}