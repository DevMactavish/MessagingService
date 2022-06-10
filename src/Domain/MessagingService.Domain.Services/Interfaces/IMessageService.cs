using System.Threading;
using System.Threading.Tasks;
using MessagingService.Domain.Services.Dtos.Requests.Message;
using MessagingService.Domain.Services.Dtos.Responses.Message;

namespace MessagingService.Domain.Services.Interfaces
{
    public interface IMessageService
    {
        Task<SendMessageResponseDto> SendMessageAsync(SendMessageRequestDto requestDto, CancellationToken cancellationToken);

        Task<GetMessagesResponseDto> GetMessagesAsync(GetMessagesRequestDto requestDto, CancellationToken cancellationToken);
    }
}