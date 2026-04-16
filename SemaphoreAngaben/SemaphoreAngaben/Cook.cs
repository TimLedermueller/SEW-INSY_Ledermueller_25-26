namespace SemaphoreAngaben;


public class Cook
{
    public string Name { get; set; }

    public Cook(string name)
    {
        Name = name;
    }

    public void Run()
    {
        while (true)
        {
            RestaurantSync.KochFrei.Release();
            RestaurantSync.BestFertig.Wait();
            PrepareMeal();
            RestaurantSync.EssenFertig.Release();
            RestaurantSync.KochFrei.Release();
        }
    }

    public void PrepareMeal()
    {
        Console.WriteLine($"{Name}: cooking food");
        Thread.Sleep(1818);
    }
}