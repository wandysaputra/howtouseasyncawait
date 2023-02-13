using Microsoft.AspNetCore.Mvc;

namespace WebApplAsyncAwait.Controllers;

public class DontAsyncInConstructor : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    
}

public class SomeObject
{
    public SomeObject()
    {
        // never do Async here nor async void
    }
    
    // alternative way
    public static async Task<SomeObject> Create()
    {
        return new SomeObject();
    }
}

// alternative way
public class SomeObjectFactory
{
    public async Task<SomeObject> Create()
    {
        return new SomeObject();
    }
}