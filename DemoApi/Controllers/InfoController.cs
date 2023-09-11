using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers;

public class InfoController : ControllerBase
{
    [HttpGet("/info")]
    public async Task<ActionResult> GetTheInfo()
    {
        return Ok($"The Controller is working just fine. created at { DateTime.Now.ToLongTimeString()}");
    }

    // GET /blog/2023/9/11
    [HttpGet("/blog/{year:int:min(2005)}/{month:int:min(1):max(12)}/{day:int}")]
    public async Task<ActionResult> GetTheBlogStuff([FromRoute]int year, int month, int day)
    {
        if(month == 2 && day > 29)
        {
            return NotFound();
        }
        return Ok($"Showing the blog stuff for {year} {month} {day}");
    }

    // GET /colors?color=Green
    [HttpGet("/colors")]
    public async Task<ActionResult> GetColors([FromQuery] string color = "Blue")
    {
        return Ok($"You Picked {color}");
    }
    // GET /employees
    [HttpGet("/employees")]
    public async Task<ActionResult> GetEmployees([FromQuery] string department = "All")
    {
        var employees = new List<Employee>
        {
            new Employee("Bob Smith", "dev"),
            new Employee("Joe Jones", "dev"),
            new Employee("Sue Blue", "ceo")
        };
        if(department != "All")
        {
            var response = employees.Where(e => e.Department == department).ToList();
            return Ok(new ResponseType<List<Employee>>(response, department));
        }
        return Ok(new ResponseType<List<Employee>>(employees, department));
    }

    [HttpGet("/whoami")]
    public async Task<ActionResult> WhoAmI([FromHeader(Name ="User-Agent")]string userAgent, [FromHeader(Name ="X-Tacos")] string tacos)
    {
        return Ok($"You are running {userAgent} and you think tacos are {tacos}");
    }

    [HttpPost("/bug-reports")]
    public async Task<ActionResult> AddBugReport([FromBody] CreateBugReportRequest request)
    {
        return Ok(request);
    }
}


public record ResponseType<T>(T data, string Filter);
public record Employee(string Name, string Department);

public record CreateBugReportRequest
{
    public string Application { get; set; } = string.Empty;
    public string Issue { get; set; } = string.Empty;

    public int Priority { get; set; }
}