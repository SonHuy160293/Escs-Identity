
using Identity.Application.Features.Commands.Services;
using Identity.Application.Features.Queries.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PostService(CreateServiceCommand createServiceCommand)
        {
            var serviceCreatedResult = await _mediator.Send(createServiceCommand);

            return CreatedAtAction(nameof(PostService), serviceCreatedResult.Succeeded);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllService()
        {
            var serviceQuery = new GetAllServicesQuery();

            var serviceQueryResult = await _mediator.Send(serviceQuery);

            return Ok(serviceQueryResult.Data);
        }
    }
}
