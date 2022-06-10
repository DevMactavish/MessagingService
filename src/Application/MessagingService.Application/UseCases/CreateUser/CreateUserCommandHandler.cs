using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MessagingService.Application.UseCases.CreateUser.Dtos;
using MessagingService.Domain.Repositories.Interfaces;
using MessagingService.Domain.Services.Dtos.Requests.User;
using MessagingService.Domain.Services.Dtos.Responses.User;
using MessagingService.Domain.Services.Interfaces;
using MongoDB.Driver;

namespace MessagingService.Application.UseCases.CreateUser
{
    
    public class CreateUserCommandHandler:IRequestHandler<CreateUserCommand,CreateUserCommandResult>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper; 
        public CreateUserCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<CreateUserCommandResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return new CreateUserCommandResult()
                    {IsSuccess = false, Message = string.Join(",", request.ValidationErrorList)};
            }
            var response = await _userService.CreateUserAsync(_mapper.Map<CreateUserRequestDto>(request), cancellationToken);
            return _mapper.Map<CreateUserCommandResult>(response);
        }
    }
}