using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var response = new
        {
            message = "Hello, World!"
        };

        return Ok(response);
    }
}
