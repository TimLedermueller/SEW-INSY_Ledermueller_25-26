// See https://aka.ms/new-console-template for more information
object egal = new object();

bool pingwartet = false;
bool pongwartet = false;

new Thread(new ThreadStart(() => Ping())).Start();
new Thread(new ThreadStart(()=> Pong())).Start();

void Ping()
{
Monitor.Enter(egal);
Monitor.Exit(egal);
}

void Pong()
{
    Monitor.Enter(egal);
    Monitor.Exit(egal);
}