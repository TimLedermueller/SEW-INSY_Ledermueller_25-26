namespace FormelOneSemaphore;

// ------------------------------------------
// Klasse: Cook
// ------------------------------------------
public class Cook {
    public string Name { get; set; }

    public Cook(string name) {
        Name = name;
    }

    public void Run() {
        while (true) {
            PrepareMeal();
        }
    }

    public void PrepareMeal() {
        Console.WriteLine("cooking food");
        Thread.Sleep(1818);
    }
}
