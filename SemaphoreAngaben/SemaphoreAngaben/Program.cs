// See https://aka.ms/new-console-template for more information
using System.Threading;
using SemaphoreAngaben;

var customer = new Customer("Customer");
var cook     = new Cook("Cook");
var cashier  = new Cashier("Cashier");

var customerThread = new Thread(customer.Run);
var cookThread     = new Thread(cook.Run);
var cashierThread  = new Thread(cashier.Run);

customerThread.Start();
cookThread.Start();
cashierThread.Start();

Console.WriteLine("Press Enter to exit...");
Console.ReadLine();

public static class RestaurantSync
{
    public static readonly SemaphoreSlim KochFrei = new SemaphoreSlim(1, 1); // Koch anfangs frei
    public static readonly SemaphoreSlim BestFertig = new SemaphoreSlim(0, 1); // noch keine Bestellung
    public static readonly SemaphoreSlim EssenFertig = new SemaphoreSlim(0, 1); // noch kein Essen
    
    public static readonly SemaphoreSlim KassaFrei = new SemaphoreSlim(1, 1); // Kassa anfangs frei
    public static readonly SemaphoreSlim GeldDa = new SemaphoreSlim(0, 1); // noch kein Geld
    public static readonly SemaphoreSlim Beleg = new SemaphoreSlim(0, 1); // noch kein Beleg
}