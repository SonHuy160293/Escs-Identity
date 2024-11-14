using Identity.Application.Features.Commands.EmailServiceConfig;
using Identity.Application.Features.Commands.Tokens;
using Identity.Application.Features.Commands.Users;
using Identity.Application.Features.Queries.EmailServiceConfigs;
using Identity.Application.Features.Queries.Tokens;
using Identity.Application.Features.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PostUser(CreateUserCommand createUserCommand)
        {
            var userCreatedResult = await _mediator.Send(createUserCommand);

            return CreatedAtAction(nameof(PostUser), userCreatedResult.Succeeded);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var userQuery = new GetAllUsersQuery();

            var userQueryResult = await _mediator.Send(userQuery);

            return Ok(userQueryResult.Data);
        }

        [HttpPost("api-key")]
        public async Task<IActionResult> CreateUserApiKey(CreateUserApiKeyCommand createUserApiKeyCommand)
        {
            var userApiKeyCreatedResult = await _mediator.Send(createUserApiKeyCommand);
            return CreatedAtAction(nameof(CreateUserApiKey), userApiKeyCreatedResult.Succeeded);
        }

        [HttpGet("api-key")]
        public async Task<IActionResult> GetAllUserApiKey()
        {
            var userApiKeyQuery = new GetAllUserApiKeyQuery();

            var userApiKeyQueryResult = await _mediator.Send(userApiKeyQuery);
            return Ok(userApiKeyQueryResult.Data);
        }


        [HttpPost("email-config")]
        public async Task<IActionResult> PostEmailServiceConfig(CreateEmailServiceConfigCommand createEmailServiceConfigCommand)
        {
            var createEmailServiceConfigResult = await _mediator.Send(createEmailServiceConfigCommand);
            return CreatedAtAction(nameof(PostEmailServiceConfig), createEmailServiceConfigResult.Succeeded);
        }

        [HttpGet("email-config")]
        public async Task<IActionResult> GetEmailServiceConfig()
        {
            var emailServiceConfigQuery = new GetAllEmailServiceConfigsQuery();

            var emailServiceConfigQueryResult = await _mediator.Send(emailServiceConfigQuery);
            return Ok(emailServiceConfigQueryResult.Data);
        }
    }
}
