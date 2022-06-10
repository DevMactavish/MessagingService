using System.Data;
using AutoMapper;
using MessagingService.Application.UseCases.CreateUser.Dtos;
using MessagingService.Domain.Services.Dtos.Requests.User;
using MessagingService.Domain.Services.Dtos.Responses.User;

namespace MessagingService.Application.UseCases.CreateUser.Mapping
{
    public class CreateUserMappingProfile:Profile
    {
        public CreateUserMappingProfile()
        {
            CreateMap<CreateUserCommand, CreateUserRequestDto>();
            CreateMap<CreateUserResponseDto, CreateUserCommandResult>();
        }
    }
}