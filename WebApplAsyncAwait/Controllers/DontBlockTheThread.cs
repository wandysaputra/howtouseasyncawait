using Microsoft.AspNetCore.Mvc;

namespace WebApplAsyncAwait.Controllers;

public class DontBlockTheThread : Controller
{
    // GET
    public IActionResult Index()
    {
        var task = InputOutput();
        
        // BAD
        var a = task.Result;
        
        // BAD
        task.Wait();
        
        // BAD
        task.GetAwaiter().GetResult();
        
        // GOOD
        // to propagate async await task through your codes
        
        return View();
    }

    public Task<string> InputOutput()
    {
        var client = new HttpClient();
        return client.GetStringAsync("some site");
    }
}