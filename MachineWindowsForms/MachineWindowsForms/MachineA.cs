namespace MachineWindowsForms;

public class MachineA(SemaphoreSlim A, SemaphoreSlim Cr)
{
    public void Run()
    {
        while (true)
        {
            A.Wait(); 
            Console.WriteLine("Machine A: working...");

            Thread.Sleep(2000);
            
            GUIelements.piece.Invoke((MethodInvoker)(() =>
            {
                GUIelements.piece.BackColor = Color.LightBlue;
            }));

            Console.WriteLine("Machine A: done.");
            Cr.Release();
        }
    }
    
}