using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ScottPlotV2
{
    class ScottPlot
    {
        // TRY TO MINIMIZE SPECIAL OBJECTS, BUT LEAVE OPTION OPEN FOR EASY TRANSITION LATER
        // try to do TOP, LEFT when possible (prepare for Point)
        // try to do WIDTH, HEIGHT when possible (prepare for Size)
        // try to do TOP, LEFT, WIDTH, HEIGHT when possible (prepare for Shape)
        // always use left/right + width/height, NEVER use x1/x2 and y1/y2

        private int figure_width, figure_height;
        private int pad_left, pad_top, pad_right, pad_bottom;
        private int plot_left, plot_top, plot_width, plot_height;
        private double view_left, view_top, view_width, view_height;
        private double axis_left, axis_top, axis_width, axis_height;

        private Bitmap bmp_frame; // starting point for any data rendering

        // customization
        private Color color_figure_background = Color.LightGray;
        private Color color_axis_labels = Color.Black;
        private Color color_data_background = Color.White;
        private Color color_grid = Color.LightGray;
        private Color color_data = Color.Blue;

        // settings
        private System.Drawing.Drawing2D.SmoothingMode smoothing_mode_frame = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        private System.Drawing.Drawing2D.SmoothingMode smoothing_mode_data = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        public string Info(bool display_in_console = true)
        {
            string msg = "\n";
            msg += $"Figure: ({figure_width}, {figure_height})\n";
            if (display_in_console) System.Console.WriteLine(msg);
            return msg;
        }

        /// <summary>
        /// Create a new ScottPlot
        /// </summary>
        /// <param name="figure_width">size of the image (pixels)</param>
        /// <param name="figure_height">size of the image (pixels)</param>
        public ScottPlot(int figure_width, int figure_height)
        {
            FigSize(figure_width, figure_height);
            FigPad(50, 10, 10, 50);
            AxisLimit(-1000,1000,-500,500);
            AxisView(40, 5, 10, 20);
        }
        
        public Bitmap Render()
        {
            // TODO: only redraw what is needed.
            return bmp_frame;
        }

        public void RedrawAxis()
        {
            Graphics gfx = Graphics.FromImage(bmp_frame);
            gfx.SmoothingMode = smoothing_mode_frame;

            // clear the background
            gfx.Clear(color_figure_background);

            // draw a black box around the data area 1 pixel wider than needed to account for line width
            Rectangle rect_plot = new Rectangle(plot_left-1, plot_top-1, plot_width+1, plot_height+1);
            gfx.FillRectangle(new SolidBrush(color_data_background), rect_plot);
            gfx.DrawRectangle(new Pen(color_axis_labels), rect_plot);

            gfx.Dispose();
        }

        public Bitmap PlotDemo1()
        {
            Bitmap bmp_live = new Bitmap(bmp_frame); // start with the pre-drawn axis
            Graphics gfx = Graphics.FromImage(bmp_live);
            gfx.SmoothingMode = smoothing_mode_data;

            Random rand = new Random();
            Point[] points = new Point[plot_width];

            for (int i=0; i<points.Length; i++)
            {
                double y = Math.Sin((double)i/20) * plot_height*.5 + plot_height*.5 + plot_top;
                int x = plot_left + i;
                points[i] = new Point(x, (int)y);
            }

            Pen pen = new Pen(color_data);
            gfx.DrawLines(pen, points);
            gfx.Dispose();
            return bmp_live;
        }

        /// <summary>
        /// Set the limits of the AXIS (useful so scrollbars know where to stop)
        /// </summary>
        /// <param name="left">edge of the scrollable area (units)</param>
        /// <param name="right">edge of the scrollable area (units)</param>
        /// <param name="bottom">edge of the scrollable area (units)</param>
        /// <param name="top">edge of the scrollable area (units)</param>
        public void AxisLimit(int left, int right, int bottom, int top)
        {
            axis_left = left;
            axis_top = top;
            axis_width = right - left;
            axis_height = top - bottom;
        }

        /// <summary>
        /// Set the limits of the VIEW (the part of the axis the plot is framing)
        /// </summary>
        /// <param name="left">edge of the visible area (units)</param>
        /// <param name="right">edge of the visible area (units)</param>
        /// <param name="bottom">edge of the visible area (units)</param>
        /// <param name="top">edge of the visible area (units)</param>
        public void AxisView(int left, int right, int bottom, int top)
        {
            view_left = left;
            view_top = top;
            view_width = right - left;
            view_height = top - bottom;

            RedrawAxis();
        }

        /// <summary>
        /// Change the padding (distance between the edge of the image and edge of the plot)
        /// </summary>
        /// <param name="left">distance from the side (pixels)</param>
        /// <param name="top">distance from the side (pixels)</param>
        /// <param name="right">distance from the side (pixels)</param>
        /// <param name="bottom">distance from the side (pixels)</param>
        public void FigPad(int left, int top, int right, int bottom)
        {
            pad_left = left;
            pad_top = top;
            pad_right = right;
            pad_bottom = bottom;
            PlotSize();
        }

        public void PlotSize()
        {
            // run this after resizing the image or changing the padding
            plot_left = pad_left;
            plot_top = pad_top;
            plot_width = figure_width - pad_left - pad_right;
            plot_height = figure_height - pad_top - pad_bottom;
            RedrawAxis();
        }

        /// <summary>
        /// Resize the figure image
        /// </summary>
        /// <param name="figure_width">size of the image (pixels)</param>
        /// <param name="figure_height">size of the image (pixels)</param>
        public void FigSize(int figure_width, int figure_height)
        {
            this.figure_width = figure_width;
            this.figure_height = figure_height;
            bmp_frame = new Bitmap(this.figure_width, this.figure_height);
            PlotSize();
        }
    }
}
