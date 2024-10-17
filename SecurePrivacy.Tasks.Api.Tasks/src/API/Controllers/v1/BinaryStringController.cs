using API.DTOs.v1;
using Asp.Versioning;
using Domain.Exceptions;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/binary-string")]
public class BinaryStringController : ControllerBase
{
    private readonly IBinaryStringService _binaryStringService;

    public BinaryStringController(IBinaryStringService binaryStringService)
    {
        _binaryStringService = binaryStringService;
    }

    [HttpPost]
    public ActionResult<string> ValidateBinaryString([FromBody] ValidateBinaryStringRequestDto request)
    {
        try
        {
            return Ok(_binaryStringService.ValidateBinaryString(request.Value));
        }
        catch (BinaryStringUnbalancedBytesException e)
        {
            return Ok(e.Message);
        }
        catch (BinaryStringPrefixEvaluationFailedException e)
        {
            return Ok(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}