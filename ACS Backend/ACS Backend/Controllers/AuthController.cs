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
            var res = _loginService.Check(loginModel);
            var genericResponseModel = new GenericResponseModel<LoginResultModel>
            {
                Data = res.Result
            };
            return Ok(res);
        }
        catch (FailedLoginException)
        {
            var res = new GenericResponseModel<string> { Message = "Failed login", QueryIsSuccess = false };
            return StatusCode(401, res);
        }
        catch (Exception e)
        {
            var res = new GenericResponseModel<string>{Message = e.Message, QueryIsSuccess = false};
            return StatusCode(500, res);
        }
    }
}