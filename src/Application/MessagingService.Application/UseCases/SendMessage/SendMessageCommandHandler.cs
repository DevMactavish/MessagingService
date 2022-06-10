using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MessagingService.Application.UseCases.SendMessage.Dtos;
using MessagingService.Domain.Services.Dtos.Requests.Message;
using MessagingService.Domain.Services.Interfaces;

namespace MessagingService.Application.UseCases.SendMessage
{
    public class SendMessageCommandHandler:IRequestHandler<SendMessageCommand,SendMessageCommandResult>
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        public SendMessageCommandHandler(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }
        public async Task<SendMessageCommandResult> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return new SendMessageCommandResult()
                    {IsSuccess = false, Message = string.Join(",", request.ValidationErrorList)};
            }

            var result = await _messageService.SendMessageAsync(_mapper.Map<SendMessageRequestDto>(request), cancellationToken);
            return _mapper.Map<SendMessageCommandResult>(result);
        }
    }
}