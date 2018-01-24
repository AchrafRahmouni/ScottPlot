using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScottPlotV2
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
            // init after form is fully loaded so the picturebox is the proper size
            scottPlot = new ScottPlot(pictureBox1.Width, pictureBox1.Height);
            button1_Click(null, null);
        }

        private void GenerateThePlot()
        {
            if (scottPlot == null) return;
            scottPlot.Resize(pictureBox1.Width, pictureBox1.Height);
            for (int i=0; i<5; i++)
            {
                scottPlot.PlotDemo2();
            }
            pictureBox1.Image = scottPlot.GetBitmap();
            richTextBox1.Text = scottPlot.Info();
        }

        // action click bindings

        private void button1_Click(object sender, EventArgs e)
        {
            GenerateThePlot();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            GenerateThePlot();
        }
    }
}
