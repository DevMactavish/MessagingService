using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MessagingService.Application.UseCases.Login.Dtos;
using MessagingService.Domain.Services.Dtos.Requests.User;
using MessagingService.Domain.Services.Interfaces;
using MessagingService.Infrastructure.Repositories.Mongo.Implementations;

namespace MessagingService.Application.UseCases.Login
{
    public class LoginCommandHandler:IRequestHandler<LoginCommand,LoginCommandResult>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public LoginCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<LoginCommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return new LoginCommandResult()
                    {IsSuccess = false, Message = string.Join(",", request.ValidationErrorList)};
            }

            var result = await _userService.LoginAsync(_mapper.Map<LoginRequestDto>(request), cancellationToken);
            return _mapper.Map<LoginCommandResult>(result);
        }
    }
}