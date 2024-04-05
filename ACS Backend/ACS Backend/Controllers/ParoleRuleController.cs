using ACS_Backend.Exceptions;
using ACS_Backend.Model;
using ACS_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ACS_Backend.Controllers;

public class ParoleRuleController : Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RestrictionController : Controller
    {
        private ParoleRuleService _paroleRuleService;

        public RestrictionController(ParoleRuleService paroleRuleService)
        {
            _paroleRuleService = paroleRuleService;
        }

        [HttpGet("Get/{id:int}")]
        public IActionResult Index(int id)
        {
            try
            {
                var res = new GenericResponseModel<ParoleRule>
                {
                    Data = _paroleRuleService.GetParoleRuleById(id),
                    QueryIsSuccess = true
                };
                return Ok(res);
            }
            catch (ItemNotFoundException e)
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
                var res = new GenericResponseModel<Array> { Data = _paroleRuleService.GetParoleRules() };
                return Ok(res);
            }
            catch (Exception e)
            {
                var res = new GenericResponseModel<Array> { QueryIsSuccess = false, Message = e.Message };
                return StatusCode(500, res);
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Create([FromBody] ParoleRule paroleRule)
        {
            try
            {
                await _paroleRuleService.CreateParoleRule(paroleRule);
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
                await _paroleRuleService.DeleteParoleRule(id);
                var res = new GenericResponseModel<string> { QueryIsSuccess = true };
                return Ok(res);
            }

            catch (ItemNotFoundException e)
            {
                var res = new GenericResponseModel<string> { QueryIsSuccess = false, Message = "Item not found" };
                return StatusCode(404, res);
            }
        }
    }
}