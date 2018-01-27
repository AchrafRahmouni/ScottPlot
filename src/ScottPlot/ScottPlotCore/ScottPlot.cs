using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Diagnostics;

/*
 * This uses axis/pixel classes to create BITMAPS.
 * It draws a frame (axis labels, ticks, etc)
 * It can accumulate data drawings
 * It can render the accumulated data above a frame (returning a bitmap)
 * 
 */

namespace ScottPlot
{

    class ScottPlot
    {
        // axes
        FigureAxis axis;

        // bitmap and graphics
        private Bitmap bmpFrame;
        private Bitmap bmpData;
        private Graphics gfxFrame;
        private Graphics gfxData;

        // settings stored internally
        private int padL, padR, padT, padB;

        // settings which can be adjusted
        public Color color_figure_background = Color.LightGray;
        public Color color_axis_labels = Color.Black;
        public Color color_plot_background = Color.White;

        // misc objects useful at the class level
        private Random rand = new Random();

        /// <summary>
        /// ScottPlot manages axis/pixel relationships so you can plot data and retrieve a graph as a bitmap
        /// </summary>
        /// <param name="width">width of the figure (px)</param>
        /// <param name="height">height of the figure (px)</param>
        public ScottPlot(int width=640, int height=480)
        {
            SetPad(50,5,20,50); // default padding
            SetSize(width, height); // create bitmaps (depending on paddding)
            Axis(-10, 10, -10, 10); // default axis (pixel scaling based on data bitmap size)
            RerawFrame();
        }

        /// <summary>
        /// Set this to adjust the quality/speed balance. This influences the frame and the data.
        /// </summary>
        public bool antiAlias
        {
            set
            {
                if (value == true)
                {
                    gfxFrame.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    gfxData.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                } else
                {
                    gfxFrame.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                    gfxData.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                }
            }
        }

        /*
        public bool antiAlias
        {
            set
            {
                this.antiAlias = value;
                /*
                if (this.antiAlias == true)
                {
                    gfxFrame.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    gfxData.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                } else
                {
                    gfxFrame.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                    gfxData.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                }
            }
            get
            {
                return antiAlias;
            }
        }
        */

        /// <summary>
        /// Resize the axis (view area)
        /// </summary>
        /// <param name="x1">left (units)</param>
        /// <param name="x2">right (units)</param>
        /// <param name="y1">bottom (units)</param>
        /// <param name="y2">top (units)</param>
        public void Axis(double? x1=null, double? x2 = null, double? y1 = null, double? y2 = null)
        {
            if (x1 == null) x1 = axis.xAxis.min;
            if (x2 == null) x2 = axis.xAxis.max;
            if (y1 == null) y1 = axis.yAxis.min;
            if (y2 == null) y2 = axis.yAxis.max;
            axis = new FigureAxis((double)x1, (double)x2, (double)y1, (double)y2, bmpData.Width, bmpData.Height);
        }
        
        /// <summary>
        /// Get the bitmap of the final image (axis + data)
        /// </summary>
        /// <returns></returns>
        public Bitmap GetBitmap()
        {
            Bitmap bmpMerged = new Bitmap(bmpFrame);
            Graphics gfx = Graphics.FromImage(bmpMerged);
            gfx.DrawImage(bmpData, new Point(padL, padT));
            return bmpMerged;
        }

        /// <summary>
        /// If axis ticks and labels will be shown, you want to pad the data to make room for them.
        /// </summary>
        /// <param name="left">distance from the edge of the image (px)</param>
        /// <param name="right">distance from the edge of the image (px)</param>
        /// <param name="top">distance from the edge of the image (px)</param>
        /// <param name="bottom">distance from the edge of the image (px)</param>
        public void SetPad(int left, int right, int top, int bottom)
        {
            this.padL = left;
            this.padR = right;
            this.padB = bottom;
            this.padT = top;
            RerawFrame();
        }

        /// <summary>
        /// Set the size of the graph image
        /// </summary>
        /// <param name="width">width of the figure (px)</param>
        /// <param name="height">height of the figure (px)</param>
        public void SetSize(int width, int height)
        {
            bmpFrame = new Bitmap(Math.Max(1, width), Math.Max(1, height));
            gfxFrame = Graphics.FromImage(bmpFrame);
            int dataWidth = width - padR - padL;
            int dataHeight = height - padT - padB;
            bmpData = new Bitmap(Math.Max(1,dataWidth), Math.Max(1, dataHeight));
            gfxData = Graphics.FromImage(bmpData);
            RerawFrame();
        }

        /// <summary>
        /// Clear the data area without modifying the frame
        /// </summary>
        public void Clear()
        {
            gfxData.DrawImage(bmpFrame, new Point(-padL, -padT));
        }














        /*
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         *          EXPERIMENTAL FUNCTIONS
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         */


        private Color color_grid = Color.LightGray;

        public void RerawFrame()
        {
            if (gfxFrame == null || gfxData == null || axis == null) return;
            
            Clear();
            gfxFrame.Clear(color_figure_background);

            // prepare drawing objects
            Pen pen_axis = new Pen(color_axis_labels);
            Pen pen_grid = new Pen(color_grid) {DashPattern = new float[] { 4, 4 }};

            // prepare fonts
            Font font_axis_labels = new Font("arial", 9, FontStyle.Regular);
            StringFormat sf_center = new StringFormat();
            sf_center.Alignment = StringAlignment.Center;
            StringFormat sf_right = new StringFormat();
            sf_right.Alignment = StringAlignment.Far;

            // fill the data area and draw a rectangle around it
            Rectangle dataArea = new Rectangle(padL - 1, padT - 1, bmpFrame.Width - padL - padR + 1, bmpFrame.Height - padT - padB + 1);
            gfxFrame.FillRectangle(new SolidBrush(color_plot_background), dataArea);
            gfxFrame.DrawRectangle(pen_axis, dataArea);
            
            // re-derive axis scaling from bitmap size
            Axis();

            // tick settings
            double tick_density_x = bmpData.Width / 100;
            double tick_density_y = bmpData.Height / 150;
            int tick_size_major = 5;
            int tick_size_minor = 2;
            int dataT = padT;
            int dataB = padT + bmpData.Height;
            int dataL = padL;
            int dataR = padL + bmpData.Width;

            // minor ticks
            foreach (Tick tick in axis.xAxis.Ticks((int)(tick_density_x*5)))
            {
                gfxFrame.DrawLine(pen_axis, new Point(dataL + tick.pixel, dataB), new Point(dataL + tick.pixel, dataB + tick_size_minor));
            }
            foreach (Tick tick in axis.yAxis.Ticks((int)(tick_density_y * 5)))
            {
                gfxFrame.DrawLine(pen_axis, new Point(dataL-1, dataT + tick.pixel), new Point(dataL-1 - tick_size_minor, dataT + tick.pixel));
            }

            // major ticks
            foreach (Tick tick in axis.xAxis.Ticks((int)(tick_density_x)))
            {
                gfxFrame.DrawLine(pen_grid, new Point(dataL + tick.pixel, dataT), new Point(dataL + tick.pixel, dataB - 1));
                gfxFrame.DrawLine(pen_axis, new Point(dataL + tick.pixel, dataB), new Point(dataL + tick.pixel, dataB + tick_size_major));
                gfxFrame.DrawString(tick.label, font_axis_labels, new SolidBrush(color_axis_labels), new Point(dataL + tick.pixel, dataB + 8), sf_center);
            }
            foreach (Tick tick in axis.yAxis.Ticks((int)(tick_density_y * 5)))
            {
                gfxFrame.DrawLine(pen_grid, new Point(dataL, dataT + tick.pixel), new Point(dataR - tick_size_major, dataT + tick.pixel));
                gfxFrame.DrawLine(pen_axis, new Point(dataL - 1, dataT + tick.pixel), new Point(dataL - 1 - tick_size_major, dataT + tick.pixel));
                gfxFrame.DrawString(tick.label, font_axis_labels, new SolidBrush(color_axis_labels), new Point(dataL-7, dataT + tick.pixel-8), sf_right);
            }
            
        }


        /// <summary>
        /// Format a number for a tick label by limiting its precision.
        /// </summary>
        /// <param name="value">tick position</param>
        /// <param name="axisSpan">range of the axis in units (not pixels)</param>
        /// <returns>formatted tick label</returns>
        private string TickLabelFormat(double value, double axisSpan)
        {
            if (axisSpan < .01) return string.Format("{0:0.0000}", value);
            if (axisSpan < .1) return string.Format("{0:0.000}", value);
            if (axisSpan < 1) return string.Format("{0:0.00}", value);
            if (axisSpan < 10) return string.Format("{0:0.0}", value);
            return string.Format("{0:0}", value);
        }

        /// <summary>
        /// demo: random size and color lines
        /// </summary>
        public void PlotDemoConfetti(int pieces=100, int length=20)
        {
            while (pieces-->0)
            {
                Point p1 = new Point(rand.Next(bmpData.Width), rand.Next(bmpData.Height));
                Point p2 = new Point(p1.X+rand.Next(length), p1.Y + rand.Next(length));
                Pen pen = new Pen(new SolidBrush(Color.FromArgb(150, rand.Next(200), rand.Next(200), rand.Next(200))), 10);
                gfxData.DrawLine(pen, p1, p2);
            }
        }

        /// <summary>
        /// demo: a time-dependent sine wave the same dimensions as the data window
        /// </summary>
        public void PlotDemoSine()
        {
            double shiftX = rand.NextDouble() * bmpData.Width;
            double freq = 2+rand.Next(20);
            double amplitude = 20+rand.Next(bmpData.Height/2);
            double offset = rand.Next(bmpData.Height);
            Point[] points = new Point[bmpData.Width];

            for (int i=0; i<bmpData.Width; i++)
            {
                double y = Math.Sin(shiftX+(double)i*freq/bmpData.Width)*amplitude+offset;
                points[i] = new Point(i, (int)y);
            }

            Pen pen = new Pen(new SolidBrush(Color.FromArgb(150, rand.Next(200), rand.Next(200), rand.Next(200))), 5);
            gfxData.DrawLines(pen, points);
        }


    }
}
