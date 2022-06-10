using System;

namespace MessagingService.Domain.Dtos
{
    public class MessageDto
    {
        public string MessageDetail { get; set; }
        public string UserFrom { get; set; }
        public string UserTo { get; set; }
        public DateTime SendedAt { get; set; }
    }
}