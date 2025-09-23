// See https://aka.ms/new-console-template for more information
using System;
using System.Threading;




/* Train ausgabe 
currSector++;
sem[currSector].WaitOne();
SemaphoreSlim.screen.WaitOne();
Console.SetCursorPosition(0, 2+Nr);
Console.WriteLine($"Train nr {Nr}({Thread.CurrentThread.GetHashCode()}) entering section {currSector}");
*/
   
        string gleis = "============================================================="; 
        Random random = new Random();
        
        int zugLaenge = random.Next(3, 9);
        string zug = new string('|', zugLaenge);

        for (int pos = 0; pos <= gleis.Length; pos++)
        {
            Console.Clear();
            
            string vorne = new string('=', Math.Max(0, pos));

           
            int rest = Math.Max(0, gleis.Length - pos - zugLaenge);
            string hinten = new string('=', rest);

           
            Console.WriteLine(vorne + zug + hinten);

            Thread.Sleep(200);
        }
    

