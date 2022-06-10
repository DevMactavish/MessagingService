namespace MessagingService.Application.UseCases.Dtos
{
    public abstract class BaseCommandResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}