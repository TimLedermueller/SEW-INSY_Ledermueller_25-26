public class Customer
{
    public string Name { get; set; }

    public Customer(string name)
    {
        Name = name;
    }

    public void Run()
    {
        RestaurantSync.KochFrei.Wait();
        Order();
        RestaurantSync.BestFertig.Release();
        RestaurantSync.EssenFertig.Wait();

        Pay();
        RestaurantSync.GeldDa.Release();
        RestaurantSync.Beleg.Wait();
    }

    private void Order()
    {
        Console.WriteLine($"{Name}: Ordering food...");
        Thread.Sleep(2000);
    }

    private void Pay()
    {
        Console.WriteLine($"{Name}: Paying...");
        Thread.Sleep(1500);
    }
}