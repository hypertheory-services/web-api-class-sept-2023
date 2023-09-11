using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers;

public class InfoController : ControllerBase
{
    [HttpGet("/info")]
    public async Task<ActionResult> GetTheInfo()
    {
        return Ok($"The Controller is working just fine. created at { DateTime.Now.ToLongTimeString()}");
    }
}
