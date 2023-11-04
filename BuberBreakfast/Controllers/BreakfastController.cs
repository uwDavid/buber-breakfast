using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using BuberBreakfast.Services;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers;

[ApiController]
[Route("breakfasts")]
public class BreakfastController : ApiController
{
    // internal variable
    private readonly IBreakfastService _breakfastService;
    // constructor
    public BreakfastController(IBreakfastService service)
    {
        _breakfastService = service;
    }
    // we injected the interface, but it's not instantiated 
    // to do that we, modify program.cs

    //[HttpPost("/breakfasts")]
    [HttpPost()]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {
        // We don't want to use the Api Contract directly, because it may not have the fields required
        // Instead, we define our own entity models
        // Here we map CreateBreakfastRequest to our internal Breakfast model
        var breakfast = new Breakfast(
            Guid.NewGuid(),
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet
        );

        // save to db, using the service
        _breakfastService.CreateBreakfast(breakfast);

        // response - again we map internal model back to Api Contract
        var response = new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.Savory,
            breakfast.Sweet
        );
        return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues: new { id = breakfast.Id },
            value: response);
    }

    /* Simple Implementation -> we will generalize this
    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {
        ErrorOr<Breakfast> result = _breakfastService.GetBreakfast(id);
        if (result.IsError && result.FirstError == Errors.Breakfast.NotFound)
        {
            return NotFound();
        }

        // result found
        var b = result.Value;

        // convert to api contract
        var res = new Breakfast(
            b.Id,
            b.Name,
            b.Description,
            b.StartDateTime,
            b.EndDateTime,
            b.LastModifiedDateTime,
            b.Savory,
            b.Sweet
        );

        return Ok(res);
    }
    */
    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {
        ErrorOr<Breakfast> result = _breakfastService.GetBreakfast(id);

        return result.Match(
                breakfast => Ok(MapBreakfastResponse(breakfast)), // see implementation below
                errors => Problem(errors)   // this is our custom Problem() in ApiController
            );
        // .Match() is an ErrorOr method: receives 2 func's
        // 1st func - onValue => how to handle the value
        // 2nd func - on Error => how to handle the errors
    }


    [HttpPut("{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
    {
        var b = new Breakfast(
            id,
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet
        );

        _breakfastService.Upsert(b);
        // if new id => create new breakfast, return 201
        // else => update, return no content

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        _breakfastService.DeleteBreakfast(id);
        return NoContent();
    }

    // Helper Methods
    private static BreakfastResponse MapBreakfastResponse(Breakfast b)
    {
        return new BreakfastResponse(
            b.Id,
            b.Name,
            b.Description,
            b.StartDateTime,
            b.EndDateTime,
            b.LastModifiedDateTime,
            b.Savory,
            b.Sweet

        );
    }
}