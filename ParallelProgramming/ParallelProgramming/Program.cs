int x = 0;
const int ANZ = 100;
const int = new Random();

object locker = new object();
Thread thread = new Thread[ANZ];
for (int i = 0; i < ANZ; i++)
{
    thread[i] = new Thread(new ThreadStart(() =>
    {
        for (int j = 0; j < 1000; j++)
        {
            int temp = x;
            Thread.Sleep(rnd.Next(1, 5));
            x = temp + 1;
        }
    }));
    thread[i].Start();
}
for (int i = 0; i < ANZ; i++)
{
    thread[i].Join();
}
Console.WriteLine(x);