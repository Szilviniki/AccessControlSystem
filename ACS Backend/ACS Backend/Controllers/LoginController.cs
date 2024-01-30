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
    public async Task<IActionResult> DummyCheck(string email, string password)
    {
        try
        {
            if (!_sql.Faculties.Any(x => x.Email == email && x.Password == password)) return BadRequest("No match");
            {
                var role = await _sql.PersonRoles.SingleAsync(x =>
                    x.Users.Single(y => y.Email == email).RoleId == x.Id);
                return Ok(role);
            }
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("Get")]
    public async Task<IActionResult> Check(string email, string password)
    {
        try
        {
            var person = await _sql.Faculties.Include(faculty => faculty.Role).SingleAsync(x => x.Email == email);
            if (!BCrypt.Net.BCrypt.EnhancedVerify(password, person.Password)) return BadRequest("No match");
            var role = person.Role;
            return Ok(role);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}