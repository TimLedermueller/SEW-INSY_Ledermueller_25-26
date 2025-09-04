using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        const int limit = 1_000_000;
        var sw = Stopwatch.StartNew();

        var primes = SieveOfEratosthenes(limit);

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
}using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        const int limit = 1_000_000;
        var sw = Stopwatch.StartNew();

        var primes = SieveOfEratosthenes(limit);

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