using System.Threading.Tasks;
using MediatR;
using MessagingService.Application.UseCases.GetMessages.Dtos;
using MessagingService.Application.UseCases.SendMessage.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MessagingService.Api.Controllers.v1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MessageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> Post([FromBody] SendMessageCommand request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages([FromQuery] string userName)
        {
            var userFrom = User.FindFirst("username")?.Value;

            var response = await _mediator.Send(new GetMessagesQuery()
                {UserFrom = userFrom, UserTo = userName});
            return Ok(response);
        }
    }
}