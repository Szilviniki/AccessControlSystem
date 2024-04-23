using ACS_Backend.Exceptions;
using ACS_Backend.Interfaces;
using ACS_Backend.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ACS_Backend.Controllers;

[ApiController]
[EnableCors]
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
        var res = _authService.Login(loginModel);
        var genericResponseModel = new GenericResponseModel<LoginResponseModel>
        {
            Data = res
        };
        return Ok(res);
    }
}