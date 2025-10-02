// See https://aka.ms/new-console-template for more information


using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

class Program
{
    static readonly int SectionCount = 10;
    static readonly int SectionWidth = 8;

    // Sektor-Semaphoren (1 Zug pro Sektor)
    static SemaphoreSlim[] sectionLocks = new SemaphoreSlim[SectionCount];

    // Zusätzliche "Schranken": manuell öffnende Gates je Sektor (start = geschlossen)
    static ManualResetEventSlim[] gates = new ManualResetEventSlim[SectionCount];

    static List<Train> trains = new List<Train>();
    static object trainsLock = new object();
    static object consoleLock = new object();
    static Random rand = new Random();

    static int trainCounter = 0;
    static Dictionary<int, string> trainStatus = new Dictionary<int, string>();

    // Schranken-Öffnungsdauer (ms), wenn per Taste 1..7 geöffnet
    const int GateOpenMillis = 2000;

    static void Main()
    {
        for (int i = 0; i < SectionCount; i++)
        {
            sectionLocks[i] = new SemaphoreSlim(1, 1);
            gates[i] = new ManualResetEventSlim(false); // false = geschlossen
        }

        // Eingabe-Thread (Space = Zug starten, 1..7 = Schranke öffnen)
        new Thread(InputThread) { IsBackground = true }.Start();

        while (true)
        {
            lock (consoleLock)
            {
                Console.Clear();
                DrawTrack();
                DrawSections();
                DrawTrainStatus();
                Console.WriteLine();
                Console.WriteLine("[Space] Zug starten | [1..7] Schranke öffnen | [Esc] Ende");
            }
            Thread.Sleep(150);
        }
    }

    static void InputThread()
    {
        while (true)
        {
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.Spacebar)
            {
                int len = rand.Next(1, 7);
                int id = Interlocked.Increment(ref trainCounter);
                var train = new Train(id, len);
                lock (trainsLock) trains.Add(train);
                UpdateTrainStatus(id, $"Train {id}({len}) waiting to start");
                new Thread(train.Run) { IsBackground = true }.Start();
            }
            else if (key >= ConsoleKey.D1 && key <= ConsoleKey.D7)
            {
                int idx = (int)key - (int)ConsoleKey.D1;      // 0..6
                if (idx < SectionCount)
                {
                    // Schranke idx kurz öffnen
                    gates[idx].Set();
                    UpdateTrainStatus(-1, $"Gate {idx + 1} opened for {GateOpenMillis} ms");
                    new Thread(() =>
                    {
                        Thread.Sleep(GateOpenMillis);
                        gates[idx].Reset();
                        UpdateTrainStatus(-1, $"Gate {idx + 1} closed");
                    }) { IsBackground = true }.Start();
                }
            }
            else if (key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
        }
    }

    static void DrawTrack()
    {
        var track = new StringBuilder(SectionCount * SectionWidth);
        for (int i = 0; i < SectionCount * SectionWidth; i++) track.Append('=');

        lock (trainsLock)
        {
            foreach (var t in trains)
            {
                // Kopf..Schluss (inklusive) = Position .. Position - (Length-1)
                for (int i = 0; i < t.Length; i++)
                {
                    int pos = t.Position - i;
                    if (pos >= 0 && pos < track.Length) track[pos] = '|';
                }
            }
        }

        Console.WriteLine(track.ToString());
    }

    static void DrawSections()
    {
        var line1 = new StringBuilder();
        var line2 = new StringBuilder();

        for (int i = 0; i < SectionCount; i++)
        {
            line1.Append("|".PadRight(SectionWidth, ' '));

            // Anzeige: '--' = geschlossen, '//' = offen (Gate), zusätzlich
            // kann die Belegung durch Semaphore (CurrentCount==0) visualisiert werden.
            bool gateOpen = gates[i].IsSet;
            string symbol = gateOpen ? "//" : "--";

            // Optional: belegte Sektoren zusätzlich mit '!' markieren
            bool occupied = sectionLocks[i].CurrentCount == 0;
            if (occupied) symbol = gateOpen ? "/!" : "-!";

            line2.Append(symbol.PadRight(SectionWidth, ' '));
        }

        Console.WriteLine(line1.ToString());
        Console.WriteLine(line2.ToString());
    }

    static void DrawTrainStatus()
    {
        lock (trainStatus)
        {
            foreach (var status in trainStatus.Values)
                Console.WriteLine(status);
        }
    }

    static void UpdateTrainStatus(int id, string status)
    {
        lock (trainStatus)
        {
            // id = -1 → Systemmeldung (Gate-Status) wird mit Zeitstempel geloggt
            if (id < 0) trainStatus[Environment.TickCount] = $"[Gate] {status}";
            else trainStatus[id] = status;
        }
    }

    class Train
    {
        public int Id { get; }
        public int Length { get; }
        public int Position { get; private set; } = -1;

        // Welchen Sektor hält dieser Zug aktuell (Semaphore)? -1 = keiner
        private int heldSection = -1;
        // Bis zu welchem Sektor wurde der "alte" Sektor schon freigegeben?
        private int releasedUpTo = -1;

        public Train(int id, int len)
        {
            Id = id;
            Length = len;
        }

        public void Run()
        {
            while (true)
            {
                int next = Position + 1;
                if (next >= SectionCount * SectionWidth) break;

                int nextSection = next / SectionWidth;

                // 1) Schranke: warten bis Gate offen ist
                gates[nextSection].Wait();

                // 2) Sektor exklusiv belegen, wenn neu
                if (heldSection != nextSection)
                {
                    sectionLocks[nextSection].Wait();
                    heldSection = nextSection;
                    Program.UpdateTrainStatus(Id, $"Train {Id}({Length}) entering section {nextSection + 1}");
                }

                // 3) Bewegung
                Position = next;
                Thread.Sleep(200);

                // 4) Alten Sektor freigeben, wenn der Schluss den Sektor verlassen hat
                //    Schluss = Position - (Length - 1)
                int tail = Position - (Length - 1);
                if (tail >= 0)
                {
                    // Wenn Schluss exakt an Sektorgrenze steht, ist der vorangegangene Sektor frei
                    if (tail % SectionWidth == 0)
                    {
                        int sectionToRelease = (tail / SectionWidth) - 1;
                        if (sectionToRelease >= 0 && sectionToRelease > releasedUpTo)
                        {
                            sectionLocks[sectionToRelease].Release();
                            releasedUpTo = sectionToRelease;
                            Program.UpdateTrainStatus(Id, $"Train {Id}({Length}) releasing section {sectionToRelease + 1}");
                        }
                    }
                }
            }

            // Endaufräumung: falls der letzte gehaltene Sektor noch nicht freigegeben wurde
            if (heldSection >= 0 && heldSection > releasedUpTo)
            {
                sectionLocks[heldSection].Release();
                Program.UpdateTrainStatus(Id, $"Train {Id}({Length}) releasing section {heldSection + 1}");
            }

            Program.UpdateTrainStatus(Id, $"Train {Id}({Length}) finished");

            lock (trainsLock) trains.Remove(this);
        }
    }
}
