using ACS_Backend.Interfaces;
using ACS_Backend.Model;
using Microsoft.AspNetCore.Mvc;

namespace ACS_Backend.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CheckInController : Controller
{
    private readonly ICheckInService _checkInService;

    public CheckInController(ICheckInService checkInService)
    {
        _checkInService = checkInService;
    }

    [HttpPost("CheckFaculty")]
    public async Task<IActionResult> CheckFaculty([FromBody] int cardId)
    {
        await _checkInService.CheckPersonnel(cardId);
        var res = new GenericResponseModel<string> { QueryIsSuccess = true };
        return Ok(res);
    }

    [HttpPost("CheckStudent")]
    public async Task<IActionResult> CheckStudent([FromBody] int cardId)
    {
        if (HttpContext.Request.Headers.TryGetValue("Authorization", out var headerAuth))
        {
            var jwtToken = headerAuth.First().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];
        }

        await _checkInService.CheckStudent(cardId);
        var res = new GenericResponseModel<string> { QueryIsSuccess = true };
        return Ok(res);
    }
}