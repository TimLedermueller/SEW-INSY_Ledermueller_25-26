// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

object egal = new object();

bool pingwartet = false;
bool pongwartet = false;

new Thread(new ThreadStart(() =>Ping())).Start();
new Thread(new ThreadStart(() =>Pong())).Start();


void Ping()
{
    Monitor.Enter(egal);
    for (int i = 0; i <= 10; i++) {
        pingwartet = true;
        if (pongwartet == false)
        {
            Monitor.Wait(egal);
        }
        Console.WriteLine("Ping");

        Monitor.Pulse(egal);
        pongwartet = false;
    }
    Monitor.Exit(egal);
}

void Pong()
{
    Monitor.Enter(egal);
    if(pingwartet)
        Monitor.Pulse(egal);
    pongwartet = true; 
    while(Monitor.Wait(egal)) { 
        Console.WriteLine("Pong");
        Monitor.Pulse(egal);
    }
    Monitor.Exit(egal);
}