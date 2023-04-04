using EmployeesAPI.Data.Models;
using EmployeesAPI.RestAPI.Application.Commands;
using EmployeesAPI.RestAPI.Application.Queries;
using EmployeesAPI.RestAPI.Common;
using EmployeesAPI.RestAPI.Exceptions;
using EmployeesAPI.RestAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesAPI.RestAPI.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(HttpError),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(HttpError),StatusCodes.Status500InternalServerError)]
        [ActionName(nameof(GetEmployeeByIdAsync))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeByIdAsync([FromRoute] Guid id)
        {
            var query = new GetEmployeeByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(HttpError),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(HttpError),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(HttpError),StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployeeAsync([FromBody] CreateEmployeeCommand request)
        {
            var result = await _mediator.Send(request);
            
            var actionName = nameof(GetEmployeeByIdAsync);
            var routeValues = new { id = result };
            
            return CreatedAtAction(actionName, routeValues:routeValues, null);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(HttpError),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(HttpError),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(HttpError),StatusCodes.Status500InternalServerError)]
        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> UpdateEmployeeAsync([FromRoute] Guid id,[FromBody] UpdateEmployeeCommand command)
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
        [ProducesResponseType(typeof(HttpError),StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeByIdAsync([FromRoute] Guid id)
        {
            var command = new DeleteEmployeeCommand { Id = id };
            
            await _mediator.Send(command);
        
            return NoContent();
        }
    }
}
