using System.Net;
using System.Threading.Tasks;
using MediatR;
using MessagingService.Application.UseCases.CreateUser.Dtos;
using MessagingService.Application.UseCases.Login.Dtos;
using MessagingService.Domain.Services.Dtos.Responses.User;
using Microsoft.AspNetCore.Mvc;

namespace MessagingService.Api.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}