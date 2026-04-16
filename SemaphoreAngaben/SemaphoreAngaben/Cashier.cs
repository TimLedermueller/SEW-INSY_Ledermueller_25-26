public class Cashier
{
    public string Name { get; set; }

    public Cashier(string name)
    {
        Name = name;
    }

    public void Run()
    {
        while (true)
        {
            RestaurantSync.KassaFrei.Wait();
            RestaurantSync.GeldDa.Wait();
            Confirm();
            RestaurantSync.Beleg.Release();
        }
    }

    public void Confirm()
    {
        Console.WriteLine($"{Name}: Confirming payment...");
        Thread.Sleep(3000);
    }
}