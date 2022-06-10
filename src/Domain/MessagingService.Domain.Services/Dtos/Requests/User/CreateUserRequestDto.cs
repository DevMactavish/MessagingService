namespace MessagingService.Domain.Services.Dtos.Requests.User
{
    public class CreateUserRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}