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
    public ActionResult<ValidateBinaryStringResponseDto> ValidateBinaryString([FromBody] ValidateBinaryStringRequestDto request)
    {
        try
        {
            return Ok(new ValidateBinaryStringResponseDto
            {
                Response = _binaryStringService.ValidateBinaryString(request.Value),   
            });
        }
        catch (BinaryStringUnbalancedBytesException e)
        {
            return Ok(new ValidateBinaryStringResponseDto
            {
                Response = e.Message
            });
        }
        catch (BinaryStringPrefixEvaluationFailedException e)
        {
            return Ok(new ValidateBinaryStringResponseDto
            {
                Response = e.Message
            });
        }
        catch (Exception e)
        {
            return BadRequest(new ValidateBinaryStringResponseDto
            {
                Response = e.Message
            });
        }
    }
}