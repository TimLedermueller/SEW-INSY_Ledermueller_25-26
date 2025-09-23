// See https://aka.ms/new-console-template for more information
using System;
using System.Threading;
using System.Collections.Generic;  




/* Train ausgabe 
currSector++;
sem[currSector].WaitOne();
SemaphoreSlim.screen.WaitOne();
Console.SetCursorPosition(0, 2+Nr);
Console.WriteLine($"Train nr {Nr}({Thread.CurrentThread.GetHashCode()}) entering section {currSector}");
*/
   
        string gleis = "============================================================="; 
        Random random = new Random();
        var zuege = new List<(int pos, int len )>();

        while (true)
        {

            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo taste = Console.ReadKey(intercept: true);

                if (taste.Key == ConsoleKey.Escape)
                {
                    Console.CursorVisible = true;
                    Console.Clear();
                    return;
                }

                if (taste.Key == ConsoleKey.Spacebar)
                {
                    int zugLaenge = random.Next(3, 9);
                    zuege.Add((pos: -zugLaenge, len: zugLaenge));
                }

            }

            for (int i = 0; i < zuege.Count; i++)
                zuege[i] = (zuege[i].pos + 1, zuege[i].len);
            
            zuege.RemoveAll(z => z.pos - z.len > gleis.Length);
            
            char[] zeile = new char[gleis.Length];
            for (int i = 0; i < zeile.Length; i++) zeile[i] = '=';
            
       
            foreach (var z in zuege)
            {
                int head = z.pos;
                int tail = head - z.len + 1;
                for (int x = tail; x <= head; x++)
                    if (x >= 0 && x < zeile.Length) zeile[x] = '|';
            }


            Console.SetCursorPosition(0, 0);
            Console.WriteLine(new string(zeile).PadRight(gleis.Length));
            
            Thread.Sleep(100);
        }
        

  
    

