namespace F1;

public class Car (SemeraphoreSlim raceReady, SemaphoreSlim carsReady,SemaphoreSlim Pitstop, SemaphoreSlim endRace )

{
    public string Racer { get; set; }
    
    public Car(string racer)
    {
        Racer = racer;
    }

    public void Run()
    {
        carsReady.Release();  
        WaitForSignal();
        Race();
        Pitstop.Wait();
        TakingPitstop();
        Race();
    }
    private void WaitForSignal()
    {
        Console.WriteLine($"{Racer}: Waiting for start signal...");
        _startSemaphore.Wait();   // wartet auf Freigabe
        Console.WriteLine($"{Racer}: GO!");
    }

    private void Race()
    {
        Console.WriteLine($"{Racer}: Racing...");
        Thread.Sleep(1500);
    }

    private void TakingPitstop()
    {
        Console.WriteLine($"{Racer}: Taking pit stop...");
        Thread.Sleep(500);
    }

}