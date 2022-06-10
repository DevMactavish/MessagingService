using AutoMapper;
using MessagingService.Application.UseCases.Login.Dtos;
using MessagingService.Domain.Services.Dtos.Requests.User;
using MessagingService.Domain.Services.Dtos.Responses.User;

namespace MessagingService.Application.UseCases.Login.Mapping
{
    public class LoginMappingProfile : Profile
    {
        public LoginMappingProfile()
        {
            CreateMap<LoginCommand, LoginRequestDto>();
            CreateMap<LoginResponseDto, LoginCommandResult>();
        }
    }
}