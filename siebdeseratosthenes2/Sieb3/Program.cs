using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

class Program {
    static void Main() {
        int n = 100_000_000;
        int threadCount = 24
            ; 
        var sw = Stopwatch.StartNew();

        int sqrtN = (int)Math.Sqrt(n);
        bool[] smallPrime = new bool[sqrtN + 1];
        for (int i = 2; i <= sqrtN; i++) smallPrime[i] = true;
        for (int p = 2; p * p <= sqrtN; p++)
            if (smallPrime[p])
                for (int j = p * p; j <= sqrtN; j += p)
                    smallPrime[j] = false;

        List<int> basePrimes = new List<int>();
        for (int i = 2; i <= sqrtN; i++) if (smallPrime[i]) basePrimes.Add(i);

        int count = 0;
        object lockObj = new object();

        int segmentSize = n / threadCount;
        Thread[] threads = new Thread[threadCount];

        for (int t = 0; t < threadCount; t++) {
            int start = t * segmentSize + 1;
            int end = (t == threadCount - 1) ? n : (t + 1) * segmentSize;

            threads[t] = new Thread(() => {
                bool[] local = new bool[end - start + 1];
                for (int i = 0; i < local.Length; i++) local[i] = true;

                foreach (var p in basePrimes) {
                    int first = Math.Max(p * p, ((start + p - 1) / p) * p);
                    for (int j = first; j <= end; j += p)
                        local[j - start] = false;
                }

                int c = 0;
                for (int i = 0; i < local.Length; i++)
                    if ((i + start) >= 2 && local[i]) c++;

                lock (lockObj) count += c;
            });

            threads[t].Start();
        }

        foreach (var th in threads) th.Join();

        sw.Stop();
        Console.WriteLine($"Primes: {count}, Threads: {threadCount}, Time: {sw.ElapsedMilliseconds}ms");
    }
}