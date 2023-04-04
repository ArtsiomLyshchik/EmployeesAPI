using Microsoft.AspNetCore.Mvc;
using EmployeesAPI.Data.Models;
using EmployeesAPI.RestAPI.Application.Commands;
using EmployeesAPI.RestAPI.Application.Queries;
using EmployeesAPI.RestAPI.Common;
using EmployeesAPI.RestAPI.Exceptions;
using EmployeesAPI.RestAPI.Models;
using MediatR;

namespace EmployeesAPI.RestAPI.Controllers
{
    [Route("api/v{version:apiVersion}/jobTitles")]
    [ApiController]
    public class JobTitlesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobTitlesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [ProducesResponseType(typeof(JobTitleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(HttpError),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(HttpError),StatusCodes.Status500InternalServerError)]
        [ActionName(nameof(GetJobTitleByIdAsync))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobTitleByIdAsync([FromRoute] Guid id)
        {
            var query = new GetJobTitleByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        
        [ProducesResponseType(typeof(HttpError),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(HttpError),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(HttpError),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(HttpError),StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<JobTitle>> CreateJobTitleAsync([FromBody] CreateJobTitleCommand request)
        {
            var result = await _mediator.Send(request);
            
            var actionName = nameof(GetJobTitleByIdAsync);
            var routeValues = new { id = result };
            return CreatedAtAction(actionName, routeValues, null);
        }
        
        
        [ProducesResponseType(typeof(HttpError),StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(HttpError),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(HttpError),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(HttpError),StatusCodes.Status500InternalServerError)]
        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> UpdateJobTitleAsync([FromRoute] Guid id,[FromBody] UpdateJobTitleCommand command)
        {
            if (command.Id != id)
            {
                throw new HttpBadRequestException("Inconsistent id was provided");
            }
            
            await _mediator.Send(command);
            
            return NoContent();
        }
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(HttpError),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(HttpError),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(HttpError),StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobTitleByIdAsync([FromRoute] Guid id)
        {
            var command = new DeleteJobTitleCommand { Id = id };
            
            await _mediator.Send(command);
        
            return NoContent();
        }
    }
}
