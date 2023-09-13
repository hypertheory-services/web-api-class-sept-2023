using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers;

public class CounterController : ControllerBase
{
    private static int counter = 0;
    [HttpPost("/counter")]
    public ActionResult IncrementCounter()
    {
        counter++; // increment it.
        return Ok(new { counter });
    }
}
