using AutoMapper;
using MessagingService.Domain.Aggregates;
using MessagingService.Domain.Dtos;

namespace MessagingService.Domain.Services.Dtos.Mapping
{
    public class DomainServicesMappingProfile:Profile
    {
        public DomainServicesMappingProfile()
        {
            CreateMap<Message, MessageDto>();
        }
    }
}