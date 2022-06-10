using System;
using System.Threading;
using System.Threading.Tasks;
using MessagingService.Domain.Aggregates;
using MessagingService.Domain.Constants;
using MessagingService.Domain.Repositories.Interfaces;
using MessagingService.Domain.Services.Dtos.Requests.User;
using MessagingService.Domain.Services.Dtos.Responses.User;
using MessagingService.Domain.Services.Interfaces;
using MessagingService.Infrastructure.Token.Interfaces;

namespace MessagingService.Domain.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IWriteUserRepository _writeUserRepository;
        private readonly IReadUserRepository _readUserRepository;
        private readonly ITokenFactory _tokenFactory;
        private readonly IWriteLoginHistoryRepository _writeLoginHistoryRepository;

        public UserService(IWriteUserRepository writeUserRepository, IReadUserRepository readUserRepository,
            ITokenFactory tokenFactory, IWriteLoginHistoryRepository writeLoginHistoryRepository)
        {
            _writeUserRepository = writeUserRepository;
            _readUserRepository = readUserRepository;
            _tokenFactory = tokenFactory;
            _writeLoginHistoryRepository = writeLoginHistoryRepository;
        }

        public async Task<CreateUserResponseDto> CreateUserAsync(CreateUserRequestDto request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return new CreateUserResponseDto() {IsSuccess = false, Message = DomainErrorCodes.Fail};
            }
            var isExist = await _readUserRepository.AnyAsync(x => x.Username == request.Username, cancellationToken);
            if (isExist)
            {
                return new CreateUserResponseDto() {IsSuccess = false, Message = DomainErrorCodes.UserExist};
            }

            var user = User.Create(request.FirstName, request.LastName, request.Username, request.Password);
            user.ChanceCreateTime(DateTime.UtcNow);
            user.ChangeUpdateTime(DateTime.UtcNow);

            await _writeUserRepository.AddAsync(user, cancellationToken);
            return new CreateUserResponseDto() {IsSuccess = true};
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return new LoginResponseDto() {IsSuccess = false, Message = DomainErrorCodes.Fail};
            }
            var user = await _readUserRepository.GetAsync(
                x => x.Username == request.UserName && x.Password == request.Password, cancellationToken);
            if (user == null)
            {
                var failedLog = LoginHistory.Create("Failed Login", "User does not exist.", request.UserName)
                    .ChanceCreateTime(DateTime.UtcNow).ChangeUpdateTime(DateTime.UtcNow);
                await _writeLoginHistoryRepository.AddAsync(failedLog, cancellationToken);
                return new LoginResponseDto() {IsSuccess = false, Message = DomainErrorCodes.UserNotExist};
            }

            var (jwt, expireDate) = _tokenFactory.GenerateToken(user.Id.ToString(), user.Username);
            var successLog = LoginHistory.Create("Success Login", "User login successfully.", user.Username)
                .ChanceCreateTime(DateTime.UtcNow).ChangeUpdateTime(DateTime.UtcNow);
            await _writeLoginHistoryRepository.AddAsync(successLog, cancellationToken);
            return new LoginResponseDto
            {
                IsSuccess = true,
                Token = jwt,
                Expiration = expireDate
            };
        }
    }
}