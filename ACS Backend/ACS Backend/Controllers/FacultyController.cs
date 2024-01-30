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
    public async Task<IActionResult> Get(Guid id)
    {
        try
        {
            if (!_sql.Faculties.Any(x => x.Id == id)) return NotFound("No one was found with this id");
            var person = _sql.Faculties.Single(x => x.Id == id);
            return Ok(person);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}