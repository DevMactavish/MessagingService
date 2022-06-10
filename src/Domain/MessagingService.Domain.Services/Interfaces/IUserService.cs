using System.Threading;
using System.Threading.Tasks;
using MessagingService.Domain.Services.Dtos.Requests.User;
using MessagingService.Domain.Services.Dtos.Responses.User;

namespace MessagingService.Domain.Services.Interfaces
{
    public interface IUserService
    {
        Task<CreateUserResponseDto> CreateUserAsync(CreateUserRequestDto request, CancellationToken cancellationToken);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken);
        
    }
}