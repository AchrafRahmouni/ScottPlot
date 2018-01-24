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

        // action click bindings

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = scottPlot.PlotDemo1();
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
            if (scottPlot == null) return;
            scottPlot.FigSize(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = scottPlot.PlotDemo1();
        }
    }
}
