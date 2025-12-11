namespace FormelOneSemaphore;

// ------------------------------------------
// Klasse: Cashier
// ------------------------------------------
public class Cashier {
    public string Name { get; set; }

    public Cashier(string name) {
        Name = name;
    }

    public void Run() {
        while (true) {
            Confirm();
        }
    }

    public void Confirm() {
        Console.WriteLine(
            $"{Name}: confirm payment"
        );
        Thread.Sleep(1818);
    }
}