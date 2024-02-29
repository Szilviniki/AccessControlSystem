using ACS_Backend.Exceptions;
using ACS_Backend.Interfaces;
using ACS_Backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace ACS_Backend.Controllers;

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
    public bool ValidateToken(
  string token,
  string issuer,
  string audience,
  ICollection<SecurityKey> signingKeys,
  out JwtSecurityToken jwt
)
    {
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
                ValidateIssuerSigningKey = true,
            IssuerSigningKeys = signingKeys,
            ValidateLifetime = true
        };

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            jwt = (JwtSecurityToken)validatedToken;

            return true;
        }
        catch (SecurityTokenValidationException ex)
        {
            // Log the reason why the token is not valid
            jwt = null;
            return false;
        }
    }

    [HttpPost("CheckStudent")]
    public async Task<IActionResult> CheckStudent([FromBody] int cardId)
    {
        Console.WriteLine(Request.Headers);
        try
        {
            if (HttpContext.Request.Headers.TryGetValue("Authorization", out var headerAuth))
            {
                var jwtToken = headerAuth.First().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];
                
            }
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