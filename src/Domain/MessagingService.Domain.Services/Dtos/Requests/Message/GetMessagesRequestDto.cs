namespace MessagingService.Domain.Services.Dtos.Requests.Message
{
    public class GetMessagesRequestDto
    {
        public string UserFrom { get; set; }
        public string UserTo { get; set; }
    }
}