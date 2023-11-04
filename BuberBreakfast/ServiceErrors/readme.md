# Error Handling using ErrorOr Package

1. Update `program.cs` to define a Error handler

```cs
app.UseExceptionHandler("/error");
```

2. Create `ErrorsController.cs`

```cs
public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem(); // returns http 500, internal server error
    }
}
```

3. `Errors.Breakfast.cs` defines all errors that can occur for the `Breakfast` resource
