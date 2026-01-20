namespace FormelOneSemaphore;


using System;
using System.Threading;

public class Customer {
    public string Name { get; set; }

    public Customer(string name) {
        Name = name;
    }

    public void Run() {
        while (true) {
            Order();
            Pay();
        }
    }

    private void Order() {
        Console.WriteLine(
            $"{Name}: ordering food"
        );
        Thread.Sleep(1917);
    }

    private void Pay() {
        Console.WriteLine(
            $"{Name}: paying order"
        );
        Thread.Sleep(1989);
    }
}