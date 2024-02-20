using ACS_Backend.Exceptions;
using ACS_Backend.Interfaces;
using ACS_Backend.Model;
using Microsoft.AspNetCore.Mvc;

namespace ACS_Backend.Controllers;

[Route("api/v1/[controller]")]
public class FacultyController : ControllerBase
{
    private IFacultyService _facultyService;

    public FacultyController(IFacultyService facultyService)
    {
        _facultyService = facultyService;
    }

    [HttpGet("Get/{id}")]
    public IActionResult Get(Guid id)
    {
        try
        {
            var res = new GenericResponseModel<Faculty>
            {
                Data = _facultyService.GetFaculty(id)
            };
            return Ok(res);
        }
        catch (ItemNotFoundException)
        {
            var res = new GenericResponseModel<Faculty>
            {
                Message = "Not found",
                QueryIsSuccess = false
            };
            return StatusCode(404, res);
        }
        catch (Exception e)
        {
            var res = new GenericResponseModel<Faculty>
            {
                QueryIsSuccess = false,
                Message = e.Message
            };
            return StatusCode(500, res);
        }
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var res = new GenericResponseModel<Array>();
        try
        {
            res.Data = _facultyService.GetAllFaculties();
            return Ok(res);
        }
        catch (Exception e)
        {
            res.QueryIsSuccess = false;
            res.Message = e.Message;
            return StatusCode(500, res);
        }
    }

    [HttpPut("Add")]
    public async Task<IActionResult> Add([FromBody] Faculty faculty)
    {
        var res = new GenericResponseModel<Faculty>();
        try
        {
            await _facultyService.AddFaculty(faculty);
            return Ok();
        }
        catch (ItemAlreadyExistsException)
        {
            res.QueryIsSuccess = false;
            res.Message = "Already exists";
            res.Data = faculty;
            return StatusCode(409, res);
        }
        catch (UniqueConstraintFailedException<List<string>> ux)
        {
            res.QueryIsSuccess = false;
            res.Message = "Failed on unique constraint";
            res.Data = faculty;
            return StatusCode(409, res);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("Update")]
    public async Task<IActionResult> Update([FromBody] Faculty faculty)
    {
        try
        {
            await _facultyService.UpdateFaculty(faculty);
            return Ok();
        }
        catch (ItemNotFoundException)
        {
            var res = new GenericResponseModel<List<string>>
            {
                QueryIsSuccess = false,
                Message = "User not found"
            };
            return StatusCode(404, res);
        }
        catch (UniqueConstraintFailedException<List<string>> ux)
        {
            var res = new GenericResponseModel<List<string>>
            {
                QueryIsSuccess = false,
                Message = "Failed on unique constraint",
                Data = ux.FailedOn
            };
            return StatusCode(409, res);
        }
        catch (Exception e)
        {
            var res = new GenericResponseModel<List<string>>
            {
                QueryIsSuccess = false,
                Message = e.Message
            };
            return StatusCode(500, res);
        }
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Update(Guid id)
    {
        try
        {
            await _facultyService.RemoveFaculty(id);
            return Ok("Deleted successfully");
        }
        catch (ItemNotFoundException)
        {
            return StatusCode(404);
        }
        catch (Exception e)
        {
            var res = new GenericResponseModel<string>
            {
                QueryIsSuccess = false,
                Message = e.Message
            };
            return StatusCode(500, res);
        }
    }
}