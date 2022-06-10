using System.Collections.Generic;
using MessagingService.Application.UseCases.Dtos;
using MessagingService.Domain.Dtos;

namespace MessagingService.Application.UseCases.GetMessages.Dtos
{
    public class GetMessagesQueryResult:BaseCommandResponse
    {
        public List<MessageDto> Messages { get; set; }
    }
}