using Microsoft.AspNetCore.Mvc;

namespace WebApplAsyncAwait.Controllers;

public class AvoidStateMachineController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Only uses `async` and `await` keyword when there is I/O Operation such as external sources.
    /// As soon as you use `async` you spawn a state machine (check ILSpy or IL Viewer)
    /// </summary>
    /// <returns>Task of string</returns>
    public Task<string> InputOutput()
    {
        var message = "Hello World";
        return Task.FromResult(message);
    }
    
    public Task InputOutputCompleted()
    {
        return Task.CompletedTask;
    }
    
    /// <summary>
    /// From this example Network Operation use `await` when you need to do something with the result  
    /// </summary>
    /// <returns></returns>
    public async Task<string> InputOutputNetworkAsync()
    {
        var client = new HttpClient();
        var content = await client.GetStringAsync("some site");
        // do something with the content
        
        return content;
    }
    
    /// <summary>
    /// When possible, try to avoid `async` keyword 
    /// </summary>
    /// <returns></returns>
    public Task<string> InputOutputNetworkTask()
    {
        var client = new HttpClient();
        return client.GetStringAsync("some site");
    }
}