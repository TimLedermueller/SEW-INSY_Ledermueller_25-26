using System;
using System.Threading;

class Program
{
    const int ANZ = 10;
    const int Start = 1; 
    const int Runden = 3; 

    static SemaphoreSlim[] semaphoren = new SemaphoreSlim[ANZ];

    static void Main()
    {

        for (int i = 0; i < ANZ; i++) // Semaphoren initialisieren
            semaphoren[i] = new SemaphoreSlim(i == Start ? 1 : 0, 1);

        // Threads starten
        for (int i = 0; i < ANZ; i++)
        {
            int id = i; // lokale Kopie
            new Thread(() => Spieler(id)).Start();
        }
    }

    static void Spieler(int id)
    {
        for (int r = 0; r < Runden; r++)
        {
            semaphoren[id].Wait();                     
            Console.WriteLine($"Spieler {id}");        
            semaphoren[(id + 1) % ANZ].Release();      
        }
    }
}