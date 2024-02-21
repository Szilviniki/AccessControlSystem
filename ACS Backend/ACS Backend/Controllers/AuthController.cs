using ACS_Backend.Exceptions;
using ACS_Backend.Interfaces;
using ACS_Backend.Model;
using Microsoft.AspNetCore.Mvc;

namespace ACS_Backend.Controllers;

[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private IAuthService _authService;

    public AuthController(IAuthService loginService)
    {
        _authService = loginService;
    }

    [HttpPost("Check")]
    public IActionResult Check([FromBody] LoginModel loginModel)
    {
        try
        {
            var res = _authService.Login(loginModel);
            return Ok(res.Result);
        }
        catch (FailedLoginException)
        {
            return StatusCode(401);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}