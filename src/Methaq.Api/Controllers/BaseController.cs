using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Methaq.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    protected IActionResult HandleResult<T>(ErrorOr<T> result)
    {
        return result.Match<IActionResult>(
            response => Ok(response),
            errors => Problem(
                statusCode: GetStatusCode(errors),
                detail: errors.First().Description)
        );
    }

    protected IActionResult HandleResult(ErrorOr<Success> result)
    {
        return result.Match<IActionResult>(
            _ => NoContent(),
            errors => Problem(
                statusCode: GetStatusCode(errors),
                detail: errors.First().Description)
        );
    }

    private static int GetStatusCode(List<Error> errors)
    {
        return errors.First().Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}