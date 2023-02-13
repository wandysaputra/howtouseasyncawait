using Microsoft.AspNetCore.Mvc;

namespace WebApplAsyncAwait.Controllers;

public class DoesntMatterWhichThread : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
    public async Task<string> InputOutput()
    {
        var client = new HttpClient();
        // Thread 1
        var content = await client.GetStringAsync("some site")
            // `ConfigureAwait(false)` we don't care if return to the different Thread, or any thread can handle the continuation of this function
            // For WPF need to be careful with UI/Main Thread
            .ConfigureAwait(false);
        // Thread 3 
        
        // do something with the content
        
        return content;
    }
}