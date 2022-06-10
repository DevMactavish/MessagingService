using AutoMapper;
using MessagingService.Application.UseCases.GetMessages.Dtos;
using MessagingService.Domain.Services.Dtos.Requests.Message;
using MessagingService.Domain.Services.Dtos.Responses.Message;

namespace MessagingService.Application.UseCases.GetMessages.Mapping
{
    public class GetMessagesMappingProfile:Profile
    {
        public GetMessagesMappingProfile()
        {
            CreateMap<GetMessagesQuery, GetMessagesRequestDto>();
            CreateMap<GetMessagesResponseDto, GetMessagesQueryResult>();
        }
    }
}