using ACS_Backend.Exceptions;
using ACS_Backend.Interfaces;
using ACS_Backend.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace ACS_Backend.Controllers;

[ApiController]
[EnableCors]
[Route("api/v1/[controller]")]
[Authorize]
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
            var res = new GenericResponseModel<Array> { Data = _studentService.GetAllStudents() };
            return Ok(res);
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
        await _studentService.UpdateStudent(updatedStudent, id);
        return Ok(new GenericResponseModel<string>(){Message = "Student updated", QueryIsSuccess = true});
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _studentService.RemoveStudent(id);
        return Ok();
    }

    [HttpPut("AddWithParent")]
    public async Task<IActionResult> AddWithParent([FromBody] AddStudentWithParentModel model)
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
    
    [HttpPost("AddNote")]
    public async Task<IActionResult> AddNoteToStudent([FromBody] Note note)
    {
        await _studentService.AddNoteToStudent(note);
        return StatusCode(201);
    }
    
    [HttpDelete("RemoveNote/{noteId}")]
    public async Task<IActionResult> RemoveNoteFromStudent(int noteId)
    {
        await _studentService.RemoveNoteFromStudent(noteId);
        return Ok();
    }
}