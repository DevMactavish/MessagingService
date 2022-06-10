using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using MessagingService.Domain.Aggregates;
using MessagingService.Domain.Constants;
using MessagingService.Domain.Repositories.Interfaces;
using MessagingService.Domain.Services.Dtos.Requests.Message;
using MessagingService.Domain.Services.Dtos.Requests.User;
using MessagingService.Domain.Services.Implementations;
using MessagingService.Infrastructure.Token.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Moq;

namespace MessagingService.Test
{
    public class Test
    {
        private readonly Mock<IWriteUserRepository> _writeUserRepositoryMock;
        private readonly Mock<IReadUserRepository> _readUserRepositoryMock;
        private readonly Mock<ITokenFactory> _tokenFactoryMock;
        private readonly Mock<IWriteLoginHistoryRepository> _writeLoginHistoryRepositoryMock;
        private readonly Mock<IWriteMessageRepository> _writeMessageRepositoryMock;
        private readonly Mock<IReadMessageRepository> _readMessageRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public Test()
        {
            _writeUserRepositoryMock = new Mock<IWriteUserRepository>();
            _readUserRepositoryMock = new Mock<IReadUserRepository>();
            _tokenFactoryMock = new Mock<ITokenFactory>();
            _writeLoginHistoryRepositoryMock = new Mock<IWriteLoginHistoryRepository>();
            _writeMessageRepositoryMock = new Mock<IWriteMessageRepository>();
            _readMessageRepositoryMock = new Mock<IReadMessageRepository>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task create_user_successfully()
        {
            //Given
            var fixture = new Fixture();
            var request = fixture.Create<CreateUserRequestDto>();

            //When
            _readUserRepositoryMock.Setup(repo =>
                    repo.AnyAsync(user => user.Username == request.Username, CancellationToken.None))
                .ReturnsAsync(false);
            UserService sut = new(_writeUserRepositoryMock.Object, _readUserRepositoryMock.Object,
                _tokenFactoryMock.Object, _writeLoginHistoryRepositoryMock.Object);
            var result = await sut.CreateUserAsync(request, CancellationToken.None);

            //Then
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task create_user_user_exist()
        {
            //Given
            var fixture = new Fixture();
            var request = fixture.Create<CreateUserRequestDto>();

            //When
            _readUserRepositoryMock.Setup(repo =>
                    repo.AnyAsync(user => user.Username == request.Username, CancellationToken.None))
                .ReturnsAsync(true);
            UserService sut = new(_writeUserRepositoryMock.Object, _readUserRepositoryMock.Object,
                _tokenFactoryMock.Object, _writeLoginHistoryRepositoryMock.Object);
            var result = await sut.CreateUserAsync(request, CancellationToken.None);

            //Then
            Assert.False(result.IsSuccess);
            Assert.Equal(DomainErrorCodes.UserExist, result.Message);
        }

        [Fact]
        public async Task create_user_fail()
        {
            //Given
            CreateUserRequestDto request = null;
            //When
            UserService sut = new(_writeUserRepositoryMock.Object, _readUserRepositoryMock.Object,
                _tokenFactoryMock.Object, _writeLoginHistoryRepositoryMock.Object);
            var result = await sut.CreateUserAsync(request, CancellationToken.None);

            //Then
            Assert.False(result.IsSuccess);
            Assert.Equal(DomainErrorCodes.Fail, result.Message);
        }

        [Fact]
        public async Task send_message_successfully()
        {
            //Given
            var fixture = new Fixture();
            var request = fixture.Create<SendMessageRequestDto>();

            //When
            _readUserRepositoryMock.Setup(repo =>
                    repo.AnyAsync(user => user.Username == request.UserFrom, CancellationToken.None))
                .ReturnsAsync(true);
            _readUserRepositoryMock.Setup(repo =>
                    repo.AnyAsync(user => user.Username == request.UserTo, CancellationToken.None))
                .ReturnsAsync(true);
            MessageService sut = new(_writeMessageRepositoryMock.Object, _readUserRepositoryMock.Object,
                _readMessageRepositoryMock.Object, _mapperMock.Object);
            var result = await sut.SendMessageAsync(request, CancellationToken.None);

            //Then
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task send_message_user_from_does_not_exist()
        {
            //Given
            var fixture = new Fixture();
            var request = fixture.Create<SendMessageRequestDto>();

            //When
            _readUserRepositoryMock.Setup(repo =>
                    repo.AnyAsync(user => user.Username == request.UserFrom, CancellationToken.None))
                .ReturnsAsync(false);
            _readUserRepositoryMock.Setup(repo =>
                    repo.AnyAsync(user => user.Username == request.UserTo, CancellationToken.None))
                .ReturnsAsync(true);
            MessageService sut = new(_writeMessageRepositoryMock.Object, _readUserRepositoryMock.Object,
                _readMessageRepositoryMock.Object, _mapperMock.Object);
            var result = await sut.SendMessageAsync(request, CancellationToken.None);

            //Then
            Assert.False(result.IsSuccess);
            Assert.Equal(DomainErrorCodes.UserNotExist, result.Message);
        }

        [Fact]
        public async Task send_message_user_to_does_not_exist()
        {
            //Given
            var fixture = new Fixture();
            var request = fixture.Create<SendMessageRequestDto>();

            //When
            _readUserRepositoryMock.Setup(repo =>
                    repo.AnyAsync(user => user.Username == request.UserFrom, CancellationToken.None))
                .ReturnsAsync(true);
            _readUserRepositoryMock.Setup(repo =>
                    repo.AnyAsync(user => user.Username == request.UserTo, CancellationToken.None))
                .ReturnsAsync(false);
            MessageService sut = new(_writeMessageRepositoryMock.Object, _readUserRepositoryMock.Object,
                _readMessageRepositoryMock.Object, _mapperMock.Object);
            var result = await sut.SendMessageAsync(request, CancellationToken.None);

            //Then
            Assert.False(result.IsSuccess);
            Assert.Equal(DomainErrorCodes.UserNotExist, result.Message);
        }

        [Fact]
        public async Task send_message_fail()
        {
            //Given
            SendMessageRequestDto request = null;

            //When
            MessageService sut = new(_writeMessageRepositoryMock.Object, _readUserRepositoryMock.Object,
                _readMessageRepositoryMock.Object, _mapperMock.Object);
            var result = await sut.SendMessageAsync(request, CancellationToken.None);

            //Then
            Assert.False(result.IsSuccess);
            Assert.Equal(DomainErrorCodes.Fail, result.Message);
        }


        [Fact]
        public async Task get_messages_successfully()
        {
            //Given
            var fixture = new Fixture();
            var request = fixture.Create<GetMessagesRequestDto>();

            //When
            _readUserRepositoryMock.Setup(repo =>
                    repo.AnyAsync(user => user.Username == request.UserFrom, CancellationToken.None))
                .ReturnsAsync(true);
            _readUserRepositoryMock.Setup(repo =>
                    repo.AnyAsync(user => user.Username == request.UserTo, CancellationToken.None))
                .ReturnsAsync(true);
            MessageService sut = new(_writeMessageRepositoryMock.Object, _readUserRepositoryMock.Object,
                _readMessageRepositoryMock.Object, _mapperMock.Object);
            var result = await sut.GetMessagesAsync(request, CancellationToken.None);

            //Then
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task get_messages_user_from_does_not_exist()
        {
            //Given
            var fixture = new Fixture();
            var request = fixture.Create<GetMessagesRequestDto>();

            //When
            _readUserRepositoryMock.Setup(repo =>
                    repo.AnyAsync(user => user.Username == request.UserFrom, CancellationToken.None))
                .ReturnsAsync(false);
            _readUserRepositoryMock.Setup(repo =>
                    repo.AnyAsync(user => user.Username == request.UserTo, CancellationToken.None))
                .ReturnsAsync(true);
            MessageService sut = new(_writeMessageRepositoryMock.Object, _readUserRepositoryMock.Object,
                _readMessageRepositoryMock.Object, _mapperMock.Object);
            var result = await sut.GetMessagesAsync(request, CancellationToken.None);

            //Then
            Assert.False(result.IsSuccess);
            Assert.Equal(DomainErrorCodes.UserNotExist, result.Message);
        }

        [Fact]
        public async Task? get_messages_user_to_does_not_exist()
        {
            //Given
            var fixture = new Fixture();
            var request = fixture.Create<GetMessagesRequestDto>();

            //When
            _readUserRepositoryMock.Setup(repo =>
                    repo.AnyAsync(user => user.Username == request.UserFrom, CancellationToken.None))
                .ReturnsAsync(true);
            _readUserRepositoryMock.Setup(repo =>
                    repo.AnyAsync(user => user.Username == request.UserTo, CancellationToken.None))
                .ReturnsAsync(false);
            MessageService sut = new(_writeMessageRepositoryMock.Object, _readUserRepositoryMock.Object,
                _readMessageRepositoryMock.Object, _mapperMock.Object);
            var result = await sut.GetMessagesAsync(request, CancellationToken.None);

            //Then
            Assert.False(result.IsSuccess);
            Assert.Equal(DomainErrorCodes.UserNotExist, result.Message);
        }

        [Fact]
        public async Task get_messages_fail()
        {
            //Given
            GetMessagesRequestDto request = null;

            //When
            MessageService sut = new(_writeMessageRepositoryMock.Object, _readUserRepositoryMock.Object,
                _readMessageRepositoryMock.Object, _mapperMock.Object);
            var result = await sut.GetMessagesAsync(request, CancellationToken.None);

            //Then
            Assert.False(result.IsSuccess);
            Assert.Equal(DomainErrorCodes.Fail, result.Message);
        }

        [Fact]
        public async Task login_successFully()
        {
            //Given
            var fixture = new Fixture();
            var request = fixture.Create<LoginRequestDto>();
            var userMock = fixture.Create<User>();

            //When
            _readUserRepositoryMock.Setup(repo =>
                    repo.GetAsync(user => user.Username == request.UserName && user.Password == request.Password,
                        CancellationToken.None))
                .ReturnsAsync(userMock);
            UserService sut = new(_writeUserRepositoryMock.Object, _readUserRepositoryMock.Object,
                _tokenFactoryMock.Object, _writeLoginHistoryRepositoryMock.Object);
            var result = await sut.LoginAsync(request, CancellationToken.None);

            //Then
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task login_user_does_not_exist()
        {
            //Given
            var fixture = new Fixture();
            var request = fixture.Create<LoginRequestDto>();
            var userMock = fixture.Create<User>();
            userMock = null;
            
            //When
            _readUserRepositoryMock.Setup(repo =>
                    repo.GetAsync(user => user.Username == request.UserName && user.Password == request.Password,
                        CancellationToken.None))
                .ReturnsAsync(userMock);
            UserService sut = new(_writeUserRepositoryMock.Object, _readUserRepositoryMock.Object,
                _tokenFactoryMock.Object, _writeLoginHistoryRepositoryMock.Object);
            var result = await sut.LoginAsync(request, CancellationToken.None);

            //Then
            Assert.False(result.IsSuccess);
            Assert.Equal(DomainErrorCodes.UserNotExist, result.Message);
        }

        [Fact]
        public async Task login_fail()
        {
            //Given
            LoginRequestDto request = null;
            //When
            UserService sut = new(_writeUserRepositoryMock.Object, _readUserRepositoryMock.Object,
                _tokenFactoryMock.Object, _writeLoginHistoryRepositoryMock.Object);
            var result = await sut.LoginAsync(request, CancellationToken.None);
        
            //Then
            Assert.False(result.IsSuccess);
            Assert.Equal(DomainErrorCodes.Fail, result.Message);
        }
    }
}