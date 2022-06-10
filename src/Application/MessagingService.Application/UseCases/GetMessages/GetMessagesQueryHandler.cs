using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MessagingService.Application.UseCases.GetMessages.Dtos;
using MessagingService.Application.UseCases.SendMessage.Dtos;
using MessagingService.Domain.Services.Dtos.Requests.Message;
using MessagingService.Domain.Services.Interfaces;

namespace MessagingService.Application.UseCases.GetMessages
{
    public class GetMessagesQueryHandler:IRequestHandler<GetMessagesQuery,GetMessagesQueryResult>
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        public GetMessagesQueryHandler(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }
        public async Task<GetMessagesQueryResult> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return new GetMessagesQueryResult()
                    {IsSuccess = false, Message = string.Join(",", request.ValidationErrorList)};
            }

            var result = await _messageService.GetMessagesAsync(_mapper.Map<GetMessagesRequestDto>(request), cancellationToken);
            return _mapper.Map<GetMessagesQueryResult>(result);
        }
    }
}