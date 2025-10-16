// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Threading;
using TrainSim;


class Program
    {  
        static string track = "=============================================================";
        static Random rng = new Random();
        static List<Train> trains = new List<Train>();
        static int tickMs = 100;
        
        // Gates (semaphore) 
        static int gateCount = 8;             // 7 oder 8; hier 8
        static int[] gatePos;                 // Position jeder Schranke auf dem Track
        static SemaphoreSlim[] gateSem; 
        
        static void Main()
        {
            Console.CursorVisible = false;
            InitGates();

            while (true)
            {
                HandleInput();  // Zug Starten und beenden
                Update();       // Bewegen und Löschen
                Render();
                DrawGates();    // Schranken Zeichnen
               // DrawSections();// Zug Zeichnen
                Thread.Sleep(tickMs); 
            }
        }
        static void HandleInput()
        {
            while (Console.KeyAvailable)
            {
                var key = Console.ReadKey(intercept: true).Key;

                if (key == ConsoleKey.Escape)
                {
                    Console.CursorVisible = true;
                    Console.Clear();
                    Environment.Exit(0);
                }

                if (key == ConsoleKey.Spacebar)
                    AddNewTrain();

                // 1..8 (inkl. Numpad): Schranke für EINEN Zug öffnen
                if (key is >= ConsoleKey.D1 and <= ConsoleKey.D8
                    || key is >= ConsoleKey.NumPad1 and <= ConsoleKey.NumPad8)
                {
                    int idx =
                        key is >= ConsoleKey.D1 and <= ConsoleKey.D8
                            ? (int)key - (int)ConsoleKey.D1
                            : (int)key - (int)ConsoleKey.NumPad1;

                    if (idx >= 0 && idx < gateCount)
                    {
                        if (gateSem[idx].CurrentCount == 0)
                            gateSem[idx].Release();
                    }
                }
            }
        }
        static void AddNewTrain()
        {
            int length = rng.Next(3, 9);               
            trains.Add(new Train(length));
        }

        
        static void Update()
        {
            for (int i = 0; i < trains.Count; i++)
            {
                var t = trains[i];
                int next = t.Position + 1;

                // Rechts aus dem Bild fahren lassen (damit Cleanup greift)
                if (next >= track.Length)
                {
                    t.Step();
                    continue;
                }

                // Liegt auf 'next' eine Schranke?
                int gateIndex = -1;
                for (int g = 0; g < gateCount; g++)
                {
                    if (gatePos[g] == next) { gateIndex = g; break; }
                }

                if (gateIndex >= 0)
                {
                    // nicht blockierend: nur weiter, wenn ein Ticket vorhanden ist
                    if (gateSem[gateIndex].Wait(0))
                    {
                        t.Step();  // Ticket verbraucht -> weiterfahren
                    }
                    else
                    {
                        // Schranke zu -> stehen bleiben (kein Step)
                    }
                }
                else
                {
                    // keine Schranke -> normal fahren
                    t.Step();
                }
            }

            // entfernen, wenn tail >= track.Length
            trains.RemoveAll(t => (t.Position - (t.Length - 1)) >= track.Length);
        }

        static void Render()
        {
            char[] line = new char[track.Length];
            for (int i = 0; i < line.Length; i++) line[i] = '=';

            DrawTrains(line);

            Console.SetCursorPosition(0, 0);
            Console.WriteLine(new string(line));
            Console.WriteLine("[Space] start train   |   [Esc] exit".PadRight(track.Length));
            Console.WriteLine(new string(' ', track.Length));
        }
        
        static void DrawTrains(char[] line)
        {
            foreach (var t in trains)
            {
                int head = t.Position;
                int tail = head - t.Length + 1;
                for (int x = tail; x <= head; x++)
                    if (x >= 0 && x < line.Length)
                        line[x] = '|';
            }
        }
        
        static void InitGates()
        {
            gatePos = new int[gateCount];
            gateSem = new SemaphoreSlim[gateCount];

            // gleichmäßig verteilen: 1/(n+1) ... n/(n+1) der Track-Länge
            for (int i = 0; i < gateCount; i++)
            {
                gatePos[i] = (i + 1) * track.Length / (gateCount + 1);
                gateSem[i] = new SemaphoreSlim(0, 1); // Start: zu (0), max 1 Ticket
            }
        }
    
        static void DrawGates()
        {
            // Marker-Linie: '|' an Gate-Positionen
            char[] markers = new char[track.Length];
            for (int i = 0; i < markers.Length; i++) markers[i] = ' ';
            for (int g = 0; g < gateCount; g++)
                if (gatePos[g] >= 0 && gatePos[g] < markers.Length)
                    markers[gatePos[g]] = '|';
            Console.WriteLine(new string(markers).PadRight(track.Length));

            // Zustand: "--" (zu), "//" (Ticket liegt bereit)
            char[] states = new char[track.Length];
            for (int i = 0; i < states.Length; i++) states[i] = ' ';
            for (int g = 0; g < gateCount; g++)
            {
                int p = gatePos[g];
                if (p >= 0 && p < states.Length)
                {
                    bool open = gateSem[g].CurrentCount > 0;
                    char a = open ? '/' : '-';
                    char b = open ? '/' : '-';
                    states[p] = a;
                    if (p + 1 < states.Length) states[p + 1] = b;
                }
            }
            Console.WriteLine(new string(states).PadRight(track.Length));
        }

    }



