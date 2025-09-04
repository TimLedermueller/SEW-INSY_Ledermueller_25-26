// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Diagnostics;

class Program
{
    const int limit = 10_000_000;

    static void Main()
    {
        var sw = Stopwatch.StartNew();

        // Hauptthread: Primzahlen berechnen
        var primes = SieveOfEratosthenes(limit);

        // Nebenbei: zweite Berechnung im Thread (Ergebnis ignoriert)
        Thread thread = new Thread(() => SieveOfEratosthenes(limit));
        thread.Start();

        sw.Stop();
        Console.WriteLine($"Gefundene Primzahlen: {primes.Count}");
        Console.WriteLine($"Dauer: {sw.ElapsedMilliseconds} ms");
    }

    static List<int> SieveOfEratosthenes(int limit)
    {
        var isPrime = new bool[limit + 1];
        Array.Fill(isPrime, true);
        isPrime[0] = isPrime[1] = false;

        for (int i = 2; i * i <= limit; i++)
        {
            if (isPrime[i])
            {
                for (int j = i * i; j <= limit; j += i)
                {
                    isPrime[j] = false;
                }
            }
        }

        var primes = new List<int>();
        for (int i = 2; i <= limit; i++)
        {
            if (isPrime[i])
                primes.Add(i);
        }

        return primes;
    }
}












