namespace SkifahrenSemaphore;

public class Timekeeper((string name, SemaphoreSlim piste, SemaphoreSlim trigger, SemaphoreSlim done) args)
{


    public void Run()
    {
        while (true)
        {
                MeasureTime(step: 1);
                MeasureTime(step: 2);
                MeasureTime(step: 3);
        }
    }

    public void MeasureTime(int step)
    {
        Console.WriteLine($"\t\tStep {step}:It is now {DateTime.Now}");
    }
}