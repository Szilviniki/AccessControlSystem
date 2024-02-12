using System.Data;
using ACS_Backend.Exceptions;
using ACS_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ACS_Backend.Controllers;

[Route("api/v1/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly StudentService _studentService;

    public StudentsController(StudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet("Get/{id}")]
    public IActionResult Get(Guid id)
    {
        try
        {
            var student = _studentService.GetStudent(id);
            return Ok(student);
        }
        catch (ItemNotFoundException)
        {
            return StatusCode(404);
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
            return Ok(_studentService.GetAllStudents());
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
            await _studentService.AddStudent(student);
            return StatusCode(201);
        }
        catch (ConstraintException)
        {
            return StatusCode(409);
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
            await _studentService.UpdateStudent(updatedStudent);
            return Ok();
        }
        catch (ItemNotFoundException)
        {
            return StatusCode(404);
        }
        catch (ConstraintException)
        {
            return StatusCode(409);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _studentService.RemoveStudent(id);
            return Ok();
        }
        catch (ItemNotFoundException)
        {
            return StatusCode(404);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("GetExtended/{id:int}")]
    public IActionResult GetExtendedStudent(int id)
    {
        try
        {
            var info = _studentService.GetExtendedStudent(id);
            return Ok(info);
        }
        catch (ItemNotFoundException)
        {
            return StatusCode(404);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetAllExtended")]
    public IActionResult GetAllExtended()
    {
        return Ok(_studentService.GetAllExtendedStudents());
    }
}