using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MessagingService.Domain.Aggregates;
using MessagingService.Domain.Constants;
using MessagingService.Domain.Dtos;
using MessagingService.Domain.Repositories.Interfaces;
using MessagingService.Domain.Services.Dtos.Requests.Message;
using MessagingService.Domain.Services.Dtos.Responses.Message;
using MessagingService.Domain.Services.Interfaces;

namespace MessagingService.Domain.Services.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly IWriteMessageRepository _writeMessageRepository;
        private readonly IReadUserRepository _readUserRepository;
        private readonly IReadMessageRepository _readMessageRepository;
        private readonly IMapper _mapper;
        public MessageService(IWriteMessageRepository writeMessageRepository, IReadUserRepository readUserRepository, IReadMessageRepository readMessageRepository, IMapper mapper)
        {
            _writeMessageRepository = writeMessageRepository;
            _readUserRepository = readUserRepository;
            _readMessageRepository = readMessageRepository;
            _mapper = mapper;
        }

        public async Task<SendMessageResponseDto> SendMessageAsync(SendMessageRequestDto requestDto,
            CancellationToken cancellationToken)
        {
            if (requestDto == null)
            {
                return new SendMessageResponseDto() {IsSuccess = false, Message = DomainErrorCodes.Fail};   
            }
            var userFromExist = await _readUserRepository.AnyAsync(
                x => x.Username == requestDto.UserFrom, cancellationToken);
            var userToExist = await _readUserRepository.AnyAsync(
                x => x.Username == requestDto.UserTo, cancellationToken);
            if (!userFromExist || !userToExist)
            {
                return new SendMessageResponseDto() {IsSuccess = false, Message = DomainErrorCodes.UserNotExist};
            }


            var message = Message.Create(requestDto.MessageDetail, requestDto.UserFrom, requestDto.UserTo);
            message.ChanceCreateTime(DateTime.UtcNow);
            message.ChangeUpdateTime(DateTime.UtcNow);
            await _writeMessageRepository.AddAsync(message, cancellationToken);

            return new SendMessageResponseDto() {IsSuccess = true};
        }

        public async Task<GetMessagesResponseDto> GetMessagesAsync(GetMessagesRequestDto requestDto, CancellationToken cancellationToken)
        {
            if (requestDto == null)
            {
                return new GetMessagesResponseDto() {IsSuccess = false, Message = DomainErrorCodes.Fail};
            }
            var userFromExist = await _readUserRepository.AnyAsync(
                x => x.Username == requestDto.UserFrom, cancellationToken);
            var userToExist = await _readUserRepository.AnyAsync(
                x => x.Username == requestDto.UserTo, cancellationToken);
            if (!userFromExist || !userToExist)
            {
                return new GetMessagesResponseDto() {IsSuccess = false, Message = DomainErrorCodes.UserNotExist};
            }

            var messages = await _readMessageRepository.GetListAsync(
                x => x.UserFrom == requestDto.UserFrom && x.UserTo == requestDto.UserTo, cancellationToken);
            return new GetMessagesResponseDto() {IsSuccess = true, Messages = _mapper.Map<List<MessageDto>>(messages)};
        }
    }
}