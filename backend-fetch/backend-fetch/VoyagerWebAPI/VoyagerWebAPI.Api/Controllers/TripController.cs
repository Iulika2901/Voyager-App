using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VoyagerWebAPI.Application.Commands.CreateTrip;
using VoyagerWebAPI.Application.Commands.UpdateTrip;
using VoyagerWebAPI.Application.Commands;
using VoyagerWebAPI.Application.Commands.AddExpense;
using VoyagerWebAPI.Application.Queries.GetAll;
using VoyagerWebAPI.Application.Queries.GetById;
using VoyagerWebAPI;
using VoyagerWebAPI.Application.Commands.DeleteTrip;
using VoyagerWebAPI.Application.Queries.GetFilteredTrips;

namespace VoyagerWebAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripController : ControllerBase
{
    private readonly IMediator _mediator;

    public TripController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<IActionResult> GetTrips()
    {
        var result = await _mediator.Send(new GetAllTripsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTrip([FromRoute] int id)
    {
        var result = await _mediator.Send(new GetTripByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTrip([FromBody] CreateTripCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTrip), new { id = result.Id }, result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTrip(int id, [FromBody] UpdateTripCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(new { error = "Route ID must match Command ID." });
        }

        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTrip(int id)
    {
        var success = await _mediator.Send(new DeleteTripCommand(id));
        return success ? NoContent() : NotFound();
    }
    
    [HttpPost("{id}/expenses")]
    public async Task<IActionResult> AddExpense(int id, [FromBody] AddExpenseRequest request)
    {
        try
        {
            var command = new AddExpenseCommand(id, request.Description, request.Amount, request.Category, request.Date);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetTrips([FromQuery] GetFilteredTripsQuery query)
    {
         query ??= new GetFilteredTripsQuery();
    
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
}

