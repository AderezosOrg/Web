using Microsoft.AspNetCore.Mvc;

namespace Work;


public class ExampleController : Controller
{
    
    [HttpGet]
    public ActionResult<string> Details(string id)
    {
        return id;
    }
}