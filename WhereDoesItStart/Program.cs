namespace WhereDoesItStart;

class Program
{
    static void Main(string[] args)
    {
        StartApp1();
        // OR
        StartApp2().GetAwaiter().GetResult();
    }

    static void StartApp1()
    {
        var collectTask = Collect();
        var processTask = Process();

        Task.WaitAll(new[] { collectTask, processTask });
    }
    
    static Task StartApp2()
    {
        var collectTask = Collect();
        var processTask = Process();

        return Task.WhenAll(new[] { collectTask, processTask });
    }

     static Task Collect()
    {
        while (true)
        {
            // doing some internet stuff
        }
    }

     static Task Process()
     {
         while (true)
         {
             // doing some database stuff
             if (true)
             {
                 // fire and forget, might get cancelled/incomplete if main function throw exception, can put in List variable and pop-it-up if needed.
                 Task.Run(() => Notify("data"));
             }
         }
         
     }

     static Task Notify(string data)
     {
         // some network stuff
         return Task.CompletedTask;
     }
}