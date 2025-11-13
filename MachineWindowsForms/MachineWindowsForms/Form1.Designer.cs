namespace MachineWindowsForms
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            float s = 1.8f;   // Skalierung für 3.5k → wirkt wie 1920p

            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.SuspendLayout();

            // --- Crane (panel1) ---
            this.panel1.BackColor = System.Drawing.Color.Gold;
            this.panel1.Location = new System.Drawing.Point((int)(20 * s), (int)(20 * s));
            this.panel1.Size = new System.Drawing.Size((int)(100 * s), (int)(50 * s));

            // --- Machine A (panel2) ---
            this.panel2.BackColor = System.Drawing.Color.LightBlue;
            this.panel2.Location = new System.Drawing.Point((int)(200 * s), (int)(130 * s));
            this.panel2.Size = new System.Drawing.Size((int)(130 * s), (int)(130 * s));

            // --- Machine B (panel3) ---
            this.panel3.BackColor = System.Drawing.Color.LightGreen;
            this.panel3.Location = new System.Drawing.Point((int)(400 * s), (int)(130 * s));
            this.panel3.Size = new System.Drawing.Size((int)(130 * s), (int)(130 * s));

            // --- Piece (panel4) ---
            this.panel4.BackColor = System.Drawing.Color.LightGreen;
            this.panel4.Location = new System.Drawing.Point((int)(20 * s), (int)(70 * s));
            this.panel4.Size = new System.Drawing.Size((int)(60 * s), (int)(60 * s));
            
            this.ClientSize = new System.Drawing.Size(600, 200);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.Name = "Form1";
            this.Text = "Machine Simulation";
            this.Load += new System.EventHandler(this.Form1_Load);   // <--- WICHTIG
            this.ResumeLayout(false);

        }

        #endregion
    }
}
