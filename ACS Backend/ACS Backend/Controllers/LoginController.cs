using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace ACS_Backend.Controllers;

[Route("api/v1/[controller]")]
public class LoginController : ControllerBase
{
    private SQL _sql;

    public LoginController(SQL sql)
    {
        _sql = sql;
    }

    [HttpGet("GetDummy")]
    public IActionResult DummyCheck(string email, string password)
    {
        try
        {
            if (!_sql.Faculties.Any(x => x.Email == email && x.Password == password)) return BadRequest("No match");
            {
                var role =  _sql.PersonRoles.Single(x =>
                    x.Users.Single(y => y.Email == email).RoleId == x.Id);
                return Ok(role);
            }
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("Check")]
    public IActionResult Check(string email, string password)
    {
        try
        {
            var person = _sql.Faculties.Single(x => x.Email == email);
            if (!BCrypt.Net.BCrypt.EnhancedVerify(password, person.Password)) return BadRequest("No match");
             var role = _sql.PersonRoles.Single(x => x.Id == person.RoleId);
            return Ok(role);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}