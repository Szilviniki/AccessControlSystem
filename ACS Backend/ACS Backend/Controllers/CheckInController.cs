using ACS_Backend.Exceptions;
using ACS_Backend.Model;
using ACS_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ACS_Backend.Controllers;

[Route("api/v1/[controller]")]
public class CheckInController : Controller
{
    private readonly CheckInService _checkInService;

    public CheckInController(CheckInService checkInService)
    {
        _checkInService = checkInService;
    }

    [HttpPost("CheckFaculty")]
    public async Task<IActionResult> CheckFaculty([FromBody] int cardId)
    {
        try
        {
            await _checkInService.CheckFaculty(cardId);
            var res = new GenericResponseModel<string> { QueryIsSuccess = true };
            return Ok(res);
        }
        catch (ItemNotFoundException)
        {
            var res = new GenericResponseModel<string> { QueryIsSuccess = false, Message = "Not found" };
            return StatusCode(404, res);
        }
        catch (Exception e)
        {
            var res = new GenericResponseModel<string> { QueryIsSuccess = false, Message = e.Message };
            return StatusCode(500, res);
        }
    }

    [HttpPost("CheckStudent")]
    public async Task<IActionResult> CheckStudent([FromBody] int cardId)
    {
        try
        {
            await _checkInService.CheckStudent(cardId);
            var res = new GenericResponseModel<string> { QueryIsSuccess = true };
            return Ok(res);
        }
        catch (ItemNotFoundException)
        {
            var res = new GenericResponseModel<string> { QueryIsSuccess = false, Message = "Not found" };
            return StatusCode(404, res);
        }
        catch (Exception e)
        {
            var res = new GenericResponseModel<string> { QueryIsSuccess = false, Message = e.Message };
            return StatusCode(500, res);
        }
    }
}