using Microsoft.AspNetCore.Mvc;

namespace ACS_Backend.Controllers;

[Route("api/v1/[controller]")]
public class StudentsController : ControllerBase
{
    private SQL _sql;

    public StudentsController(SQL sql)
    {
        _sql = sql;
    }

    [HttpGet("Get")]
    public IActionResult Get(Guid id)
    {
        try
        {
            if (!_sql.Students.Any(x => id == x.Id))
            {
                return NotFound();
            }

            return Ok(_sql.Students.Single(x => x.Id == id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetExtended")]
    public IActionResult Details(Guid id)
    {
        try
        {
            if (!_sql.Students.Any(x => x.Id == id)) return NotFound();
            var info = _sql.ExtendedStudents.Where(x => x.StudentId == id);
            Console.WriteLine(info);
            return Ok(info);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        try
        {
            return Ok(_sql.Students);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPut("Add")]
    public async Task<IActionResult> Add([FromBody] Student student)
    {
        try
        {
            if (_sql.Students.Any(x => x.CardId == student.CardId)) return BadRequest("Already exists");

            student.Id = Guid.NewGuid();
            student.BirthDate = student.BirthDate.Date;
            _sql.Students.Add(student);
            await _sql.SaveChangesAsync();
            return StatusCode(201, "Created");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("Update")]
    public async Task<IActionResult> Update([FromBody] Student updatedStudent)
    {
        try
        {
            if (!_sql.Students.Any(x => x.Id == updatedStudent.Id)) return NotFound("Student not found!");
            {
                _sql.Students.Update(updatedStudent);
                await _sql.SaveChangesAsync();
                return Ok("Updated");
            }
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            if (!_sql.Students.Any(x => x.Id == id)) return BadRequest("Student not found!");

            _sql.Students.Remove(_sql.Students.Single(x => x.Id == id));
            await _sql.SaveChangesAsync();
            return Ok("Removed from database");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}