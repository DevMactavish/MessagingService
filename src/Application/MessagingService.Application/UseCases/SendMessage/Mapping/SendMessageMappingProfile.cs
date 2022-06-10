using AutoMapper;
using MessagingService.Application.UseCases.SendMessage.Dtos;
using MessagingService.Domain.Services.Dtos.Requests.Message;
using MessagingService.Domain.Services.Dtos.Responses.Message;

namespace MessagingService.Application.UseCases.SendMessage.Mapping
{
    public class SendMessageMappingProfile:Profile
    {
        public SendMessageMappingProfile()
        {
            CreateMap<SendMessageCommand, SendMessageRequestDto>();
            CreateMap<SendMessageResponseDto, SendMessageCommandResult>();
        }
    }
}