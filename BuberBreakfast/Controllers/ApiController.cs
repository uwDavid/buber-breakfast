using ErrorOr;
using Microsoft.AspNetCore.Mvc;


namespace BuberBreakfast.Controllers;

[ApiController]
[Route("[controller]")]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        var firstError = errors[0];
        var statusCode = firstError.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError //all other error types
        };

        return Problem(statusCode: statusCode, title: firstError.Description);
    }
}

// All controllers will inherit from ApiController 
// Here, we defined our own Problem() behavior, based on Error type
// goal is to generalize our error handling
// see how it's used in GetBreakfast()