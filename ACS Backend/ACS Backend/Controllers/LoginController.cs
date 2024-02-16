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

    [HttpPost("TestLogin")]
    public IActionResult DummyCheck([FromBody] LoginData data)
    {
        try
        {
            if (!_sql.Faculties.Any(x => x.Email == data.Email && x.Password == data.Password)) return BadRequest("No match");
            {
                var person = _sql.Faculties.Single(x => x.Email == data.Email && x.Password == data.Password);
                var role = _sql.PersonRoles.Single(x => x.Id == person.RoleId);

                return Ok(role);
            }
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("Check")]
    public IActionResult Check([FromBody] LoginData data)
    {
        try
        {
            var person = _sql.Faculties.Single(x => x.Email == data.Email);
            Console.WriteLine(person.ToString());
            if (!BCrypt.Net.BCrypt.EnhancedVerify(data.Password, person.Password)) return BadRequest("No match");
            var role = _sql.PersonRoles.Single(x => x.Id == person.RoleId);
            return Ok(person);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}