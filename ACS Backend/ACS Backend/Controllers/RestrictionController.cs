using ACS_Backend.Exceptions;
using ACS_Backend.Model;
using ACS_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ACS_Backend.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RestrictionController : Controller
{
    private RestrictionService _restrictionService;

    public RestrictionController(RestrictionService restrictionService)
    {
        _restrictionService = restrictionService;
    }

    [HttpGet("Get/{id:int}")]
    public IActionResult Index(int id)
    {
        try
        {
            var res = new GenericResponseModel<Restriction>
            {
                Data = _restrictionService.GetRestrictionById(id),
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
            var res = new GenericResponseModel<Array> { Data = _restrictionService.GetRestrictions() };
            return Ok(res);
        }
        catch (Exception e)
        {
            var res = new GenericResponseModel<Array> { QueryIsSuccess = false, Message = e.Message };
            return StatusCode(500, res);
        }
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Create([FromBody] Restriction restriction)
    {
        try
        {
            await _restrictionService.CreateRestriction(restriction);
            var res = new GenericResponseModel<string> { QueryIsSuccess = true };
            return StatusCode(201, res);
        }
        catch (ItemAlreadyExistsException e)
        {
            var res = new GenericResponseModel<string> { QueryIsSuccess = false, Message = "Item already exists" };
            return BadRequest(res);
        }
        catch (Exception e)
        {
            var res = new GenericResponseModel<string> { QueryIsSuccess = false, Message = e.Message };
            return StatusCode(500, res);
        }
    }

    [HttpDelete("Delete/{id:int}")]
    public async Task<IActionResult> Remove(int id)
    {
        try
        {
            await _restrictionService.DeleteRestriction(id);
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