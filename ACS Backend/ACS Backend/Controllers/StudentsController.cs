using System.Data;
using ACS_Backend.Exceptions;
using ACS_Backend.Model;
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
            var res = new GenericResponseModel<Student> { Data = student, QueryIsSuccess = true };
            return Ok(res);
        }
        catch (ItemNotFoundException)
        {
            var res = new GenericResponseModel<Student> { QueryIsSuccess = false, Message = "Not found" };
            return StatusCode(404, res);
            return BadRequest(e.Message);
        }
    }



    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        try
        {
            var res = new GenericResponseModel<Array> { Data = _studentService.GetAllStudents() };
            return Ok(res);
        }
        catch (Exception e)
        {
            var res = new GenericResponseModel<Array> { QueryIsSuccess = false, Message = e.Message };
            return StatusCode(500, res);
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
        catch (UniqueConstraintFailedException<List<string>> e)
        {
            var res = new GenericResponseModel<List<string>>
                { QueryIsSuccess = false, Message = "Unique constraint failed", Data = e.FailedOn };
            return StatusCode(409);
        }
        catch (Exception e)
        {
            var res = new GenericResponseModel<List<string>> { QueryIsSuccess = false, Message = e.Message };
            return StatusCode(500, res);
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
            var res = new GenericResponseModel<List<string>> { QueryIsSuccess = false, Message = "Not found" };
            return StatusCode(404, res);
        }
        catch (UniqueConstraintFailedException<List<string>> e)
        {
            var res = new GenericResponseModel<List<string>>
            {
                Data = e.FailedOn,
                QueryIsSuccess = false
            };
            return StatusCode(409, res);
        }
        catch (Exception e)
        {
            var res = new GenericResponseModel<List<string>> { QueryIsSuccess = false, Message = e.Message };
            return StatusCode(500, res);
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
            var res = new GenericResponseModel<string> { QueryIsSuccess = false, Message = "Not found" };
            return StatusCode(404, res);
        }
        catch (Exception e)
        {
            var res = new GenericResponseModel<string> { QueryIsSuccess = false, Message = e.Message };
            return StatusCode(500, res);
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
            var res = new GenericResponseModel<string> { QueryIsSuccess = false, Message = "Not found" };
            return StatusCode(404, res);
        }
        catch (Exception e)
        {
            var res = new GenericResponseModel<string> { QueryIsSuccess = false, Message = e.Message };
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("GetAllExtended")]
    public IActionResult GetAllExtended()
    {
        try
        {
            return Ok(_studentService.GetAllExtendedStudents());
        }
        catch (Exception e)
        {
            var res = new GenericResponseModel<string> { QueryIsSuccess = false, Message = e.Message };
            return StatusCode(500, res);
        }
    }
}