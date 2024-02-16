using ACS_Backend.Exceptions;
using ACS_Backend.Model;
using ACS_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ACS_Backend.Controllers;

[Route("api/v1/[controller]")]
public class LoginController : ControllerBase
{
    private LoginService _loginService;

    public LoginController(LoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpGet("Check")]
    public IActionResult Check([FromBody] LoginModel loginModel)
    {
        try
        {
            var res = _loginService.Check(loginModel);
            return Ok();
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