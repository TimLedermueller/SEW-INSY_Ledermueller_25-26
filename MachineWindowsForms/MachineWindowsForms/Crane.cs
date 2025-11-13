namespace MachineWindowsForms;

public class Crane(SemaphoreSlim A, SemaphoreSlim B, SemaphoreSlim Cr)
{
    private void MoveUp()
    {
        for (int i = 0; i < 20; i++)
        {
            GUIelements.crane.Top--;
            Thread.Sleep(50);
        }    
    }

    private void MoveDown()
    {
        for (int i = 0; i < 20; i++)
        {
            GUIelements.crane.Top++;
            Thread.Sleep(50);
        }    
    }
    
    private void Move1(string from, string to)
    {
        Thread.Sleep(500);
        Console.WriteLine($"moving from {from} to {to}");

        GUIelements.piece.Visible = true;
        GUIelements.piece.BackColor = Color.LightGreen;
        
        for (int i = 0; i < 400; i++)
        {
            Thread.Sleep(5);
            GUIelements.crane.Left++;
            GUIelements.piece.Left++;
        }
    }
    
    private void Move2(string from, string to)
    {
        Thread.Sleep(500);
        Console.WriteLine($"moving from {from} to {to}");

        GUIelements.piece.Visible = true;
        GUIelements.piece.BackColor = Color.LightBlue; 

        for (int i = 0; i < 350; i++)
        {
            Thread.Sleep(5);
            GUIelements.crane.Left++;
            GUIelements.piece.Left++;
        }
    }

    
    private void Move3(string from, string to)
{
    Thread.Sleep(500);
    Console.WriteLine($"moving from {from} to {to}");

    GUIelements.piece.Visible = true;
    GUIelements.piece.BackColor = Color.LightGreen; 

    for (int i = 0; i < 750; i++)
    {
        Thread.Sleep(1);
        GUIelements.crane.Left--;
        GUIelements.piece.Left--;
    }
}


    public void Run()
    {
        while (true)
        {
            Move1("Store", "MA");
            A.Release();
            MoveUp();

            Cr.Wait();
            MoveDown();
            Move2("MA", "MB");
            B.Release();
            MoveUp();

            Cr.Wait();
            MoveDown();
            Move3("MB", "Store");
            Console.WriteLine("--------------");
        }
    }
}