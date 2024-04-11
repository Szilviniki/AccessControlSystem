using ACS_Backend.Exceptions;
using ACS_Backend.Interfaces;
using ACS_Backend.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ACS_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuardianController : Controller
    {
        private IGuardianService _guardianService;

        public GuardianController(IGuardianService guardianService)
        {
            _guardianService = guardianService;
        }

        [HttpGet("get/{id}")]
        public IActionResult GetGuardian([FromRoute] Guid id)
        {
            var res = new GenericResponseModel<Guardian>
            {
                Data = _guardianService.GetGuardian(id),
                QueryIsSuccess = true
            };
            return Ok(res);
        }

        [HttpGet("getAll")]
        public IActionResult GetAllGuardians()
        {
            var res = new GenericResponseModel<Array>
            {
                Data = _guardianService.GetAllGuardians(),
                QueryIsSuccess = true
            };
            return Ok(res);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddGuardian([FromBody] Guardian guardian)
        {
            await _guardianService.AddGuardian(guardian);
            var res = new GenericResponseModel<string> { QueryIsSuccess = true };
            return Ok(res);
        }

        [HttpPost("update/{id:guid}")]
        public async Task<IActionResult> UpdateGuardian(Guid id, [FromBody] Guardian guardian)
        {
            await _guardianService.UpdateGuardian(guardian, id);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteGuardian([FromRoute] Guid id)
        {
            try
            {
                await _guardianService.DeleteGuardian(id);
                var res = new GenericResponseModel<string> { QueryIsSuccess = true };
                return Ok(res);
            }
            catch (ItemNotFoundException)
            {
                var res = new GenericResponseModel<string> { QueryIsSuccess = false, Message = "Guardian not found" };
                return NotFound(res);
            }
            catch (Exception e)
            {
                var res = new GenericResponseModel<string> { QueryIsSuccess = false, Message = e.Message };
                return StatusCode(500, res);
            }
        }
    }
}