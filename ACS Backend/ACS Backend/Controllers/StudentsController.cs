using ACS_Backend.Exceptions;
using ACS_Backend.Interfaces;
using ACS_Backend.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace ACS_Backend.Controllers;

[ApiController]
[EnableCors]
[Route("api/v1/[controller]")]
public class StudentsController : ControllerBase
{
    private IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet("Get/{id}")]
    public IActionResult Get(Guid id)
    {
        var student = _studentService.GetStudent(id);
        var res = new GenericResponseModel<Student> { Data = student, QueryIsSuccess = true };
        return Ok(res);
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
        await _studentService.AddStudent(student);
        return StatusCode(201);
    }

    [HttpPost("Update/{id:Guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateStudentModel updatedStudent)
    {
        // try
        // {
            await _studentService.UpdateStudent(updatedStudent, id);
            return Ok();
        // }
        // catch (ItemNotFoundException)
        // {
        //     var res = new GenericResponseModel<List<string>> { QueryIsSuccess = false, Message = "Not found" };
        //     return StatusCode(404, res);
        // }
        // catch (UniqueConstraintFailedException<List<string>> e)
        // {
        //     var res = new GenericResponseModel<List<string>>
        //     {
        //         Data = e.FailedOn,
        //         QueryIsSuccess = false
        //     };
        //     return StatusCode(409, res);
        // }
        // catch (BadFormatException)
        // {
        //     var res = new GenericResponseModel<string> { QueryIsSuccess = false, Message = "Bad format" };
        //     return StatusCode(422, res);
        // }
        // catch (UnprocessableEntityException)
        // {
        //     var res = new GenericResponseModel<string> { QueryIsSuccess = false, Message = "Unprocessable entity" };
        //     return StatusCode(422, res);
        // }
        // catch (ReferredEntityNotFoundException)
        // {
        //     var res = new GenericResponseModel<string>
        //         { QueryIsSuccess = false, Message = "Guardian entity not found" };
        //     return BadRequest(res);
        // }
        // catch (Exception e)
        // {
        //     var res = new GenericResponseModel<List<string>> { QueryIsSuccess = false, Message = e.Message };
        //     return StatusCode(500, res);
        // }
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

    [HttpPut("AddWithParent")]
    public async Task<IActionResult> AddWithParent([FromBody] AddStudentWithParentModel model)
    {
        try
        {
            var parent = new Guardian()
            {
                Email = model.ParentEmail,
                Phone = model.ParentPhone,
                Name = model.ParentName,
                Id = Guid.NewGuid()
            };

            Random random = new Random();

            var cardID = random.Next(100000, 999999);

            var student = new Student()
            {
                Name = model.StudentName,
                Email = model.StudentEmail,
                Phone = model.StudentPhone,
                BirthDate = model.StudentBirthDate,
                CardId = cardID
            };

            await _studentService.AddStudentWithParent(student, parent);
            return StatusCode(201);
        }
        catch (UniqueConstraintFailedException<List<string>> e)
        {
            var res = new GenericResponseModel<List<string>>
                { QueryIsSuccess = false, Message = "Unique constraint failed", Data = e.FailedOn };
            return StatusCode(409);
        }
        catch (NotAddedException)
        {
            var res = new GenericResponseModel<string> { QueryIsSuccess = false, Message = "Not added" };
            return StatusCode(400, res);
        }
        catch (BadFormatException)
        {
            var res = new GenericResponseModel<string> { QueryIsSuccess = false, Message = "Bad format" };
            return StatusCode(422, res);
        }
        catch (ReferredEntityNotFoundException)
        {
            var res = new GenericResponseModel<string>
                { QueryIsSuccess = false, Message = "Guardian entity not found" };
            return BadRequest(res);
        }
        catch (ArgumentException e)
        {
            var res = new GenericResponseModel<List<string>> { QueryIsSuccess = false, Message = e.Message };
            return StatusCode(422, res);
        }
    }
}