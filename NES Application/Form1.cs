using System;
using System.Threading;
using System.Windows.Forms;

namespace NES_Application
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer updateTimer;
        private NES nes;

        Thread t;

        public Form1()
        {
            InitializeComponent();

            nes = new NES("Tetris (U) [!].nes", this);

            t = new Thread(() => UpdateTimer_Tick(nes));
            t.Start();
        }

        private void InitializeUpdateTimer(NES nes)
        {
            updateTimer = new System.Windows.Forms.Timer();
            updateTimer.Interval = 16; // About 30 FPS
            updateTimer.Tick += new EventHandler((object sender, EventArgs e) => UpdateTimer_Tick(nes));
            updateTimer.Start();
        }

        private void UpdateTimer_Tick(NES nes)
        {
            foreach (int i in nes.Run())
            {
                // i
            }
        }

        private void memDump_Click(object sender, EventArgs e)
        {
            t.Abort();
            nes.MemoryDump();
        }

        private void printReg_Click(object sender, EventArgs e)
        {
            t.Abort();
            nes.RegisterPrint();
        }
    }
}
