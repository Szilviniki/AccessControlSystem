using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;

namespace ACS_Backend.Controllers;

[Route("api/v1/[controller]")]
public class FacultyController : ControllerBase
{
    private SQL _sql;

    public FacultyController(SQL sql)
    {
        _sql = sql;
    }

    [HttpGet("Get")]
    public IActionResult Get(Guid id)
    {
        try
        {
            if (!_sql.Faculties.Any(x => x.Id == id)) return NotFound("User not found");
            var person = _sql.Faculties.Single(x => x.Id == id);
            return Ok(person);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        try
        {
            return Ok(_sql.Faculties);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("Add")]
    public async Task<IActionResult> Add([FromBody]Faculty faculty)
    {
        try
        {
            faculty.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(faculty.Password);
            _sql.Faculties.Add(faculty);
            await _sql.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }

    }
    [HttpPost("Update")]
    public async Task<IActionResult> Update([FromBody] Faculty faculty)
    {
        try {
            if (!_sql.Faculties.Any(x => x.Id == faculty.Id)) return NotFound("User not found");
            _sql.Faculties.Update(faculty);
            await _sql.SaveChangesAsync();
            return Ok("Updated");
            
        }catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Update(Guid id)
    {
        try
        {
            if (!_sql.Faculties.Any(x => x.Id == id)) return NotFound("User not found");
            var user = _sql.Faculties.FirstOrDefault(x => x.Id == id);
            _sql.Faculties.Remove(user);
            await _sql.SaveChangesAsync();
            return Ok("Deleted successfully");
        }
        catch (Exception e)
        {

            return StatusCode(500, e.Message);
        }
    }
}