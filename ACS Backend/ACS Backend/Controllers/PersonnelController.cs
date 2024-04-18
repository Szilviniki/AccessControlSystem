using ACS_Backend.Exceptions;
using ACS_Backend.Interfaces;
using ACS_Backend.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ACS_Backend.Controllers;

[ApiController]
[Route("api/v1/Faculty")]

public class PersonnelController : ControllerBase
{
    private IPersonnelService _facultyService;

    public PersonnelController(IPersonnelService facultyService)
    {
        _facultyService = facultyService;
    }

    [HttpGet("Get/{id:guid}")]
    public IActionResult Get(Guid id)
    {
        var res = new GenericResponseModel<Personnel>
        {
            Data = _facultyService.GetFaculty(id)
        };
        return Ok(res);
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var res = new GenericResponseModel<Array>
        {
            Data = _facultyService.GetAllFaculties()
        };
        return Ok(res);
    }

    [HttpPut("Add")]
    public async Task<IActionResult> Add([FromBody] Personnel faculty)
    {
        var res = new GenericResponseModel<Personnel>();
            await _facultyService.AddFaculty(faculty);
            res.QueryIsSuccess = true;
            res.Data = faculty;
            return Ok(res);
    }

    [HttpPost("Update/{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePersonnelModel faculty)
    {
        
        
        
        await _facultyService.UpdateFaculty(faculty, id);
        return Ok();
    }

    [HttpDelete("Delete/{id:guid}")]
    public async Task<IActionResult> Update(Guid id)
    {
        await _facultyService.RemoveFaculty(id);
        return Ok();
    }
}