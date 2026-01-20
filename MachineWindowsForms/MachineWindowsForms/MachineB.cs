namespace MachineWindowsForms;

public class MachineB(SemaphoreSlim B, SemaphoreSlim Cr)
{
    public void Run()
    {
        while (true)
        {
            B.Wait(); 
            Console.WriteLine("Machine B: working...");

            Thread.Sleep(3000);

            GUIelements.piece.Invoke((MethodInvoker)(() =>
            {
                GUIelements.piece.BackColor = Color.LightGreen;
            }));

            Console.WriteLine("Machine B: done.");
            Cr.Release(); 
        }
    }
    
}