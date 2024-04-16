using ACS_Backend.Exceptions;
using ACS_Backend.Model;
using ACS_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ACS_Backend.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class RestrictionController : Controller
{
    private LockRuleService _lockRuleService;

    public RestrictionController(LockRuleService lockRuleService)
    {
        _lockRuleService = lockRuleService;
    }

    [HttpGet("Get/{id:int}")]
    public IActionResult Index(int id)
    {
        try
        {
            var res = new GenericResponseModel<LockRule>
            {
                Data = _lockRuleService.GetRestrictionById(id),
                QueryIsSuccess = true
            };
            return Ok(res);
        }catch (ItemNotFoundException e)
        {
            var res = new GenericResponseModel<string> { QueryIsSuccess = false, Message = "Item not found" };
            return StatusCode(404, res);
        }

        catch (Exception e)
        {
            var res = new GenericResponseModel<string> { QueryIsSuccess = false, Message = e.Message };
            return StatusCode(500, res);
        }
    }
    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        try
        {
            var res = new GenericResponseModel<Array> { Data = _lockRuleService.GetRestrictions() };
            return Ok(res);
        }
        catch (Exception e)
        {
            var res = new GenericResponseModel<Array> { QueryIsSuccess = false, Message = e.Message };
            return StatusCode(500, res);
        }
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Create([FromBody] LockRule lockRule, Guid studentID)
    {
        try
        {
            await _lockRuleService.CreateRestriction(lockRule, studentID);
            var res = new GenericResponseModel<string> { QueryIsSuccess = true };
            return StatusCode(201, res);
        }
        catch (ItemAlreadyExistsException e)
        {
            var res = new GenericResponseModel<string> { QueryIsSuccess = false, Message = "Erre a diákra ez már ki van szabva"};
            return BadRequest(res);
        }
        catch (ReferredEntityNotFoundException e)
        {
            var res = new GenericResponseModel<string> { QueryIsSuccess = false, Message = "Nem található a diák"};
            return StatusCode(400,res);
        }
        catch (UnprocessableEntityException e)
        {
            var res = new GenericResponseModel<string> { QueryIsSuccess = false, Message = "Hibás adatok"};
            return StatusCode(412,res);
        }
    }

    [HttpDelete("Delete/{id:int}")]
    public async Task<IActionResult> Remove(int id)
    {
        try
        {
            await _lockRuleService.DeleteRestriction(id);
            var res = new GenericResponseModel<string> { QueryIsSuccess = true };
            return Ok(res);
        }

        catch (ItemNotFoundException e)
        {
            var res = new GenericResponseModel<string>{ QueryIsSuccess = false, Message = "Item not found" };
            return StatusCode(404, res);
        }
    }
}