using Identity.Application.Features.Commands.Roles;
using Identity.Application.Features.Queries.Roles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRole()
        {
            var roleQuery = new GetAllRolesQuery();

            var roleQueryResult = await _mediator.Send(roleQuery);
            return Ok(roleQueryResult.Data);
        }

        [HttpPost]
        public async Task<IActionResult> PostRole(CreateRoleCommand createRoleCommand)
        {
            var roleCreatedResult = await _mediator.Send(createRoleCommand);

            return CreatedAtAction(nameof(PostRole), roleCreatedResult.Succeeded);
        }


    }
}
