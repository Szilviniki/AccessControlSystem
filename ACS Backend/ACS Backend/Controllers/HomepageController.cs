using ACS_Backend.Interfaces;
using ACS_Backend.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ACS_Backend.Controllers;

[Controller]
[Route("api/v1/[controller]")]
[Authorize]
public class HomepageController : ControllerBase
{
    private IHomepageService _homepageService;

    public HomepageController(IHomepageService homepageService)
    {
        _homepageService = homepageService;
    }


    [HttpGet]
    public IActionResult Get()
    {
        var res = new GenericResponseModel<HomepageModel>
        {
            Data = _homepageService.GetHomepageData(),
            QueryIsSuccess = true
        };
        return Ok(res);
    }
}