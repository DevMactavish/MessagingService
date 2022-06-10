using System;

namespace MessagingService.Domain.Services.Dtos.Requests.Message
{
    public class SendMessageRequestDto
    {
        public string MessageDetail { get; set; }
        public string UserFrom { get; set; }
        public string UserTo { get; set; }
    }
}