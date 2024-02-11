using Microsoft.AspNetCore.Mvc;

namespace ACS_Backend.Controllers;
[Route("api/v1/[controller]")]
public class CheckInController : ControllerBase
{
    private SQL _sql;

    public CheckInController(SQL sql)
    {
        _sql = sql;
    }
    [HttpGet("IsWorking")]
    public IActionResult DbConnectionAlive()
    {
        return StatusCode(!_sql.Database.CanConnect() ? 503 : 204);
    }
}