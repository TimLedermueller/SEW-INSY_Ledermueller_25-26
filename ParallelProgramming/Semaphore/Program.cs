// See https://aka.ms/new-console-template for more information


SemaphoreSlim ping = new SemaphoreSlim(1,1);
//Ping fahne ist oben
SemaphoreSlim pong = new SemaphoreSlim(0,1);
//ping fahne ist unten

new Thread(new ThreadStart(() =>Ping())).Start();
new Thread(new ThreadStart(() =>Pong())).Start();


void Ping()
{
    for (int i = 0; i < 10; i++)
    {
        ping.Wait();  //eigene fahne ping runter 
        Console.WriteLine("Ping");
        pong.Release();//ander fahne pong hinauf 
    }
}

void Pong()
{
    pong.Wait(); //eigene fagne pong runter 
    Console.WriteLine("Pong");
    ping.Release(); //eigen fahne ping hinauf
}