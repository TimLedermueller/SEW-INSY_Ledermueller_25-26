using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MachineWindowsForms
{
    public static class GUIelements
    {
        public static Panel crane;
        public static Panel piece;
        public static Panel machinea;
        public static Panel machineb;
    }

    public partial class Form1 : Form
    {
        private SemaphoreSlim A = new SemaphoreSlim(0, 1);
        private SemaphoreSlim B = new SemaphoreSlim(0, 1);
        private SemaphoreSlim Cr = new SemaphoreSlim(0, 1);

        public Form1()
        {
            InitializeComponent();
            GUIelements.crane = this.panel1;
            GUIelements.machinea = this.panel2;
            GUIelements.machineb = this.panel3;
            GUIelements.piece = this.panel4;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MachineA a = new MachineA(A,Cr);
            MachineB b = new MachineB(B,Cr);
            Crane c = new Crane(A,B,Cr);
            
            new Task(()=>a.Run()).Start();
            new Task(()=>b.Run()).Start();
            new Task(()=>c.Run()).Start();
        }
    }
}
