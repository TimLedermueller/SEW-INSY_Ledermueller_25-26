namespace F1;

using System;
using System.Threading;

public class F1Race (SemeraphoreSlim raceReady, SemaphoreSlim carsReady,SemaphoreSlim Pitstop, SemaphoreSlim endRace )
{
    private readonly int _carCount;
    
    public void Run()
    {
        for (int i = 0; i < _carCount; i++)
        {
            carsReady.Wait();
        }
        raceReady.Release();
        Start();
        Pitstop.Release();
        Thread.Sleep(1000);
        
        for (int i = 0; i < _carCount; i++)
        {
            endRace.Wait();
        }
        End();
    }

    private void Start()
    {
        Console.WriteLine("Starting Race...");
        Thread.Sleep(1000);
    }

    private void End()
    {
        Console.WriteLine("Race finished");
    }
}


