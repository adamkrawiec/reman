using Microsoft.AspNetCore.Mvc;

namespace reman.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    public HomeController()
    {
    }

    [HttpGet(Name = "Home")]
    public string Index()
    {
        return "Hello";
    }
}
