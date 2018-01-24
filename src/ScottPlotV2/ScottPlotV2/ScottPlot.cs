using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

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
        private int pad_left, pad_top, pad_right, pad_bot;
        private int plot_left, plot_top, plot_bot, plot_right, plot_width, plot_height;
        private double view_left, view_top, view_bot, view_right, view_width, view_height;
        private double axis_left, axis_top, axis_width, axis_height;
        private double px_per_unit_y, px_per_unit_x, units_per_px_x, units_per_px_y;

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


        /// <summary>
        /// Create a new ScottPlot object.
        /// </summary>
        /// <param name="figure_width">size of the image (pixels)</param>
        /// <param name="figure_height">size of the image (pixels)</param>
        public ScottPlot(int figure_width, int figure_height)
        {
            //HighQuality(false);
            Resize(figure_width, figure_height);
            Pad(50, 10, 10, 50);
            AxisLimit(-1000,1000,-500,500);
            AxisView(-1000, 1000, -200, 200);
        }

        /// <summary>
        /// Debugging information about the ScottPlot
        /// </summary>
        /// <param name="display_in_console"></param>
        /// <returns>debug message</returns>
        public string Info(bool display_in_console = false)
        {
            string msg = "\n";
            msg += $"Figure: ({figure_width}, {figure_height})\n";
            msg += string.Format("Axis render time: {0:0.000} ms ({1:0.00} Hz)\n", time_redraw_axis_ms, (1e3/time_redraw_axis_ms));
            msg += string.Format("Data render time: {0:0.000} ms ({1:0.00} Hz)\n", time_redraw_data_ms, (1e3/time_redraw_data_ms));
            double renderTime = time_redraw_data_ms + time_redraw_axis_ms;
            msg += string.Format("Total render time: {0:0.000} ms ({1:0.00} Hz)\n", renderTime, (1e3 / renderTime));
            if (display_in_console) System.Console.WriteLine(msg);
            return msg;
        }

        /// <summary>
        /// Switch between high quality anti-aliased plots (pretty, but slow) vs. highspeed (grainy) plotting.
        /// </summary>
        /// <param name="antiAlias"></param>
        public void HighQuality(bool antiAlias)
        {
            if (antiAlias)
            {
                smoothing_mode_frame = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                smoothing_mode_data = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            }else{
                smoothing_mode_frame = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                smoothing_mode_data = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            }
        }

        private double time_redraw_axis_ms;
        private double time_redraw_data_ms;

        /// <summary>
        /// Repaint the figure backdrop, square around the data, axis labels, ticks, and gridlines.
        /// This is usually done automatically after resizing or adjusting padding.
        /// </summary>
        public void RedrawAxis()
        {
            // be sure all axis calculations are up to date
            RecalcAxis();

            // prepare pens for grid lines and tick marks
            Pen penAxis = new Pen(color_axis_labels);
            Pen penGrid = new Pen(color_grid);
            penGrid.DashPattern = new float[] { 4, 4 };
            
            // prepare colors and fonts
            Font font_axis_labels = new Font("arial", 9, FontStyle.Regular);
            StringFormat string_format_center = new StringFormat();
            string_format_center.Alignment = StringAlignment.Center;
            StringFormat string_format_right = new StringFormat();
            string_format_right.Alignment = StringAlignment.Far;

            int minor_tick_size = 2;
            int major_tick_size = 5;

            // tick densities might benefit from scaling to the window size
            int minor_tick_density = 12;
            int major_tick_density = 4;

            /// DRAWING STARTS ///

            // prepare to time the drawing
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Restart();

            // now we start drawing
            Graphics gfx = Graphics.FromImage(bmp_frame);
            gfx.SmoothingMode = smoothing_mode_frame;
            gfx.Clear(color_figure_background);

            // draw a black box around the data area 1 pixel wider than needed to account for line width
            Rectangle rect_plot = new Rectangle(plot_left - 1, plot_top - 1, plot_width + 1, plot_height + 1);
            gfx.FillRectangle(new SolidBrush(color_data_background), rect_plot);
            gfx.DrawRectangle(new Pen(color_axis_labels), rect_plot);

            // horizontal axis
            foreach (double tickValX in TickGen(view_left, view_right, plot_width, minor_tick_density))
            {
                // minor ticks
                int xPx = UnitToPx_X(tickValX);
                gfx.DrawLine(penAxis, new Point(xPx, plot_bot), new Point(xPx, plot_bot + minor_tick_size));
            }
            foreach (double tickValX in TickGen(view_left, view_right, plot_width, major_tick_density))
            {
                // major ticks
                int xPx = UnitToPx_X(tickValX);
                gfx.DrawLine(penGrid, new Point(xPx, plot_top), new Point(xPx, plot_bot));
                gfx.DrawLine(penAxis, new Point(xPx, plot_bot), new Point(xPx, plot_bot + major_tick_size));
                gfx.DrawString(TickString(tickValX, view_width), font_axis_labels, new SolidBrush(color_axis_labels), new Point(xPx, plot_bot + 8), string_format_center);
            }

            // vertical axis
            foreach (double tickValY in TickGen(view_bot, view_top, plot_height, minor_tick_density))
            {
                // minor ticks
                int yPx = UnitToPx_Y(tickValY);
                gfx.DrawLine(penAxis, new Point(plot_left-1, yPx), new Point(plot_left-1-minor_tick_size, yPx));
            }
            foreach (double tickValY in TickGen(view_bot, view_top, plot_height, major_tick_density))
            {
                // major ticks
                int yPx = UnitToPx_Y(tickValY);
                gfx.DrawLine(penAxis, new Point(plot_left - 1, yPx), new Point(plot_left - 1 - major_tick_size, yPx));
                gfx.DrawLine(penGrid, new Point(plot_left, yPx), new Point(plot_right - 1, yPx));
                gfx.DrawString(TickString(tickValY, view_height), font_axis_labels, new SolidBrush(color_axis_labels), new Point(plot_left - major_tick_size - 1, yPx - 8), string_format_right);            }

            gfx.Dispose();

            stopwatch.Stop();
            time_redraw_axis_ms = (double)stopwatch.ElapsedTicks / System.Diagnostics.Stopwatch.Frequency * 1000.0;
        }

        private int UnitToPx_X(double unit)
        {
            return (int)((unit - view_left) * px_per_unit_x) + plot_left;
        }

        private int UnitToPx_Y(double unit)
        {
            return (int)((unit - view_bot) * px_per_unit_y) + plot_top;
            // TODO: MAYBE INVERT
        }

        private double PxToUnit_X(int pixel)
        {
            throw new NotImplementedException();
        }
        private double PxToUnit_Y(int pixel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update pixel/unit conversions. This should get called right before the axis is plotted.
        /// These units will be around when it's time to plot the data.
        /// </summary>
        private void RecalcAxis()
        {
            px_per_unit_y = plot_height / view_height;
            px_per_unit_x = plot_width / view_width;
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
            view_right = right;
            view_bot = bottom;
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
        public void Pad(int left, int top, int right, int bottom)
        {
            pad_left = left;
            pad_top = top;
            pad_right = right;
            pad_bot = bottom;
            RecalcPlot();
        }

        /// <summary>
        /// Updates plot dimensions after resizing or pad value changes. ALl units are pixels.
        /// </summary>
        private void RecalcPlot()
        {
            plot_left = pad_left;
            plot_top = pad_top;
            plot_width = figure_width - pad_left - pad_right;
            plot_height = figure_height - pad_top - pad_bot;
            plot_bot = plot_top + plot_height;
            plot_right = plot_left + plot_width;

            RedrawAxis();
        }

        /// <summary>
        /// Resize the figure image
        /// </summary>
        /// <param name="figure_width">size of the image (pixels)</param>
        /// <param name="figure_height">size of the image (pixels)</param>
        public void Resize(int figure_width, int figure_height)
        {
            this.figure_width = figure_width;
            this.figure_height = figure_height;
            bmp_frame = new Bitmap(this.figure_width, this.figure_height);
            RecalcPlot();
        }

        /// <summary>
        /// given an arbitrary number, return the nearerest round number
        /// (i.e., 1000, 500, 100, 50, 10, 5, 1, .5, .1, .05, .01)
        /// </summary>
        private double RoundNumberNear(double target)
        {
            target = Math.Abs(target);
            int lastDivision = 2;
            double round = 1000000000000;
            while (round > 0.00000000001)
            {
                if (round <= target) return round;
                round /= lastDivision;
                if (lastDivision == 2) lastDivision = 5;
                else lastDivision = 2;
            }
            return 0;
        }

        /// <summary>
        /// return an array of good tick values for an axis given a range
        /// </summary>
        private double[] TickGen(double axisValueLower, double axisValueUpper, int graphWidthPx, int nTicks = 4)
        {
            List<double> values = new List<double>();
            List<int> pixels = new List<int>();
            List<string> labels = new List<string>();
            double dataSpan = axisValueUpper - axisValueLower;
            double unitsPerPx = dataSpan / graphWidthPx;
            double PxPerUnit = graphWidthPx / dataSpan;
            double tickSize = RoundNumberNear((dataSpan / nTicks) * 1.5);

            int lastTick = 123456789;
            for (int i = 0; i < graphWidthPx; i++)
            {
                double thisPosition = i * unitsPerPx + axisValueLower;
                int thisTick = (int)(thisPosition / tickSize);
                if (thisTick != lastTick)
                {
                    lastTick = thisTick;
                    double thisPositionRounded = (double)((int)(thisPosition / tickSize) * tickSize);
                    if (thisPositionRounded > axisValueLower && thisPositionRounded < axisValueUpper)
                    {
                        values.Add(thisPositionRounded);
                        pixels.Add(i);
                        labels.Add(string.Format("{0}", thisPosition));
                    }
                }
            }
            return values.ToArray();
        }

        /// <summary>
        /// Format a number for a tick label by limiting its precision.
        /// </summary>
        /// <param name="value">tick position</param>
        /// <param name="axisSpan">range of the axis in units (not pixels)</param>
        /// <returns>formatted tick label</returns>
        private string TickString(double value, double axisSpan)
        {
            if (axisSpan < .01) return string.Format("{0:0.0000}", value);
            if (axisSpan < .1) return string.Format("{0:0.000}", value);
            if (axisSpan < 1) return string.Format("{0:0.00}", value);
            if (axisSpan < 10) return string.Format("{0:0.0}", value);
            return string.Format("{0:0}", value);
        }













        /// <summary>
        /// Demo is an alternative to sending actual data to plot
        /// </summary>
        /// <returns>final bitmap figure</returns>
        public Bitmap PlotDemoSine()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Restart();

            Bitmap bmp_live = new Bitmap(bmp_frame); // start with the pre-drawn axis
            Graphics gfx = Graphics.FromImage(bmp_live);
            gfx.SmoothingMode = smoothing_mode_data;

            Random rand = new Random();
            Point[] points = new Point[plot_width];

            for (int i = 0; i < points.Length; i++)
            {
                double y = Math.Sin((double)i * Math.PI / 50.0) * plot_height * .5 + plot_height * .5 + plot_top;
                int x = plot_left + i;
                points[i] = new Point(x, (int)y);
            }

            Pen pen = new Pen(color_data);
            gfx.DrawLines(pen, points);
            gfx.Dispose();

            stopwatch.Stop();
            time_redraw_data_ms = (double)stopwatch.ElapsedTicks / System.Diagnostics.Stopwatch.Frequency * 1000.0;

            return bmp_live;
        }



    }
}
