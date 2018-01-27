using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;

namespace ScottPlot
{
    public partial class Form1 : Form
    {
        private ScottPlot scottPlot;

        public Form1()
        {
            InitializeComponent();            
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            scottPlot = new ScottPlot(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = scottPlot.GetBitmap();
        }

        private void RedrawEverything()
        {
            if (scottPlot == null) return;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Restart();

            switch (combo_routine.Text)
            {
                case "confetti":
                    scottPlot.PlotDemoConfetti();
                    break;
                case "sine":
                    scottPlot.PlotDemoSine();
                    break;
                default:
                    scottPlot.Clear();
                    break;

            }
            
            pictureBox1.Image = scottPlot.GetBitmap();

            double timeMs = (double)stopwatch.ElapsedTicks / System.Diagnostics.Stopwatch.Frequency * 1000.0;
            richTextBox1.Text = string.Format("Render time: {0:0.00} ms ({1:0.00} Hz)", timeMs, 1000.0 / timeMs);
        }
        
        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            if (scottPlot == null) return;
            scottPlot.SetSize(pictureBox1.Width, pictureBox1.Height);
        }

        private void cb_animate_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_animate.Checked == true) timer1.Enabled = true;
            else timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            RedrawEverything();
            Application.DoEvents(); // freezes drawing while actively resizing
            if (cb_animate.Checked == true) timer1.Enabled = true;
        }

        private void combo_routine_SelectedIndexChanged(object sender, EventArgs e)
        {
            RedrawEverything();
        }

        private void btn_draw_Click(object sender, EventArgs e)
        {
            RedrawEverything();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            scottPlot.Clear();
            pictureBox1.Image = scottPlot.GetBitmap();
        }

        private void cb_quality_CheckedChanged(object sender, EventArgs e)
        {
            scottPlot.antiAlias = cb_quality.Checked;
        }
    }
}
