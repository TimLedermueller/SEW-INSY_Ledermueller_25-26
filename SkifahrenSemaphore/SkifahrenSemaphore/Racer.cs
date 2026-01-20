namespace SkifahrenSemaphore;

public class Racer(string args)
{

    Random r = new Random();
    string name;  
    
    public void Run()
    {
        Console.WriteLine($"{name} preparing");

        Console.WriteLine($"{name} started");
        
        Race();

        InterimTime();

        Race();

        Finished();
    }
    
    
    public void Race()
    {
        Console.WriteLine($"\t{name} is racing");
        Thread.Sleep( millisecondsTimeout: r.Next(2000,3000));
  
    }
    public void InterimTime()
    {
        Console.WriteLine($"\t{name} watch triggered at {DateTime.Now}");
    }

    public void Finished()
    {
        Console.WriteLine($"\t{name} Finished at {DateTime.Now}");
    }

}



