// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


public static SemaphoreSlim orderPlaced = new SemaphoreSlim(0, 1);
public static SemaphoreSlim mealReady   = new SemaphoreSlim(0, 1);
public static SemaphoreSlim paymentDone = new SemaphoreSlim(0, 1);
