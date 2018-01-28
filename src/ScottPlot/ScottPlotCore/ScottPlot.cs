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
        private FigureAxis axis1;

        // bitmap and graphics
        private Bitmap bmpFrame;
        private Bitmap bmpData;
        private Graphics gfxFrame;
        private Graphics gfxData;

        // settings stored internally
        private int padL, padR, padT, padB;
        private bool _antiAlias = false;

        // frame settings which can be adjusted
        public Color color_figure_background = Color.LightGray;
        public Color color_axis_labels = Color.Black;
        public Color color_plot_background = Color.White;
        public Color color_grid = Color.LightGray;

        // marker settings
        public enum markerShape { circle, x, plus, ring };
        public markerShape plotMarkerShape = markerShape.circle;
        public int plotMarkerSize = 3;

        // plot settings
        public Color PlotColor = Color.Red;
        public Color randomColor { get { return Color.FromArgb(150, rand.Next(200), rand.Next(200), rand.Next(200)); } }
        public int plotLineWidth = 1;
        public bool show_debug_message = true;

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
            RedrawFrame();
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
                    _antiAlias = true;
                } else
                {
                    gfxFrame.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                    gfxData.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                    _antiAlias = false;
                }
            }
            get
            {
                return _antiAlias;
            }
        }

        /// <summary>
        /// Resize the axis (view area)
        /// </summary>
        /// <param name="x1">left (units)</param>
        /// <param name="x2">right (units)</param>
        /// <param name="y1">bottom (units)</param>
        /// <param name="y2">top (units)</param>
        public FigureAxis Axis(double? x1=null, double? x2 = null, double? y1 = null, double? y2 = null)
        {
            if (x1 == null) x1 = axis1.xAxis.min;
            if (x2 == null) x2 = axis1.xAxis.max;
            if (y1 == null) y1 = axis1.yAxis.min;
            if (y2 == null) y2 = axis1.yAxis.max;
            this.axis1 = new FigureAxis((double)x1, (double)x2, (double)y1, (double)y2, bmpData.Width, bmpData.Height);
            return this.axis1;
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

            if (show_debug_message)
            {
                Font font = new Font("courier new", 11, FontStyle.Regular);
                var shp = gfx.MeasureString(debug_message, font);
                gfx.FillRectangle(new SolidBrush(Color.FromArgb(128,0,0,0)),new Rectangle(padL, padT, (int)shp.Width, (int)shp.Height));
                gfx.DrawString(debug_message, font, new SolidBrush(Color.Black), new Point(padL+1, padT+1));
                gfx.DrawString(debug_message, font, new SolidBrush(Color.White), new Point(padL, padT));
            }

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
            RedrawFrame();
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

            // since we reset the graphics, we should re-set the aliasing mode
            antiAlias=_antiAlias;

            RedrawFrame();
        }

        /// <summary>
        /// Clear the data area without modifying the frame
        /// </summary>
        public void Clear()
        {
            gfxData.DrawImage(bmpFrame, new Point(-padL, -padT));
        }


        public void RedrawFrame()
        {
            if (gfxFrame == null || gfxData == null || axis1 == null) return;

            Clear();
            gfxFrame.Clear(color_figure_background);

            // prepare drawing objects
            Pen pen_axis = new Pen(color_axis_labels);
            Pen pen_grid = new Pen(color_grid) { DashPattern = new float[] { 4, 4 } };

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
            double tick_density_y = bmpData.Height / 175;
            int tick_size_major = 5;
            int tick_size_minor = 2;
            int dataT = padT;
            int dataB = padT + bmpData.Height;
            int dataL = padL;
            int dataR = padL + bmpData.Width;

            // minor ticks
            foreach (Tick tick in axis1.xAxis.Ticks((int)(tick_density_x * 5)))
            {
                gfxFrame.DrawLine(pen_axis, new Point(dataL + tick.pixel, dataB), new Point(dataL + tick.pixel, dataB + tick_size_minor));
            }
            foreach (Tick tick in axis1.yAxis.Ticks((int)(tick_density_y * 5)))
            {
                gfxFrame.DrawLine(pen_axis, new Point(dataL - 1, dataT + tick.pixel), new Point(dataL - 1 - tick_size_minor, dataT + tick.pixel));
            }

            // major ticks
            foreach (Tick tick in axis1.xAxis.Ticks((int)(tick_density_x)))
            {
                gfxFrame.DrawLine(pen_grid, new Point(dataL + tick.pixel, dataT), new Point(dataL + tick.pixel, dataB - 1));
                gfxFrame.DrawLine(pen_axis, new Point(dataL + tick.pixel, dataB), new Point(dataL + tick.pixel, dataB + tick_size_major));
                gfxFrame.DrawString(tick.label, font_axis_labels, new SolidBrush(color_axis_labels), new Point(dataL + tick.pixel, dataB + 8), sf_center);
            }
            foreach (Tick tick in axis1.yAxis.Ticks((int)(tick_density_y * 5)))
            {
                gfxFrame.DrawLine(pen_grid, new Point(dataL, dataT + tick.pixel), new Point(dataR - tick_size_major, dataT + tick.pixel));
                gfxFrame.DrawLine(pen_axis, new Point(dataL - 1, dataT + tick.pixel), new Point(dataL - 1 - tick_size_major, dataT + tick.pixel));
                gfxFrame.DrawString(tick.label, font_axis_labels, new SolidBrush(color_axis_labels), new Point(dataL - 7, dataT + tick.pixel - 8), sf_right);
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
        /// Zoom by a fraction on the center of an axis.
        /// Values below 1 zoom in, those above 1 zoom out.
        /// </summary>
        /// <param name="xFrac">horizontal zoom fraction</param>
        /// <param name="yFrac">vertical zoom fraction</param>
        public void Zoom(double? xFrac, double? yFrac)
        {
            if (xFrac != null) axis1.xAxis.Zoom((double)xFrac);
            if (yFrac != null) axis1.yAxis.Zoom((double)yFrac);
            if (xFrac != null || yFrac != null) RedrawFrame();
        }

        /// <summary>
        /// Shift the axis horizontally and/or vertically with no change in zoom.
        /// </summary>
        /// <param name="xShift"></param>
        /// <param name="yShift"></param>
        public void Pan(double? xShift, double? yShift)
        {
            if (xShift != null) axis1.xAxis.Pan((double)xShift);
            if (yShift != null) axis1.yAxis.Pan((double)yShift);
            if (xShift != null || yShift != null) RedrawFrame();
        }
        

        /// <summary>
        /// Plot a series of arbitrarily-spaced (X,Y) data points.
        /// </summary>
        /// <param name="Xs"></param>
        /// <param name="Ys"></param>
        public void PlotLineXY(double[] Xs, double[] Ys)
        {
            int pointCount = Math.Min(Xs.Length, Ys.Length);
            Point[] points = new Point[pointCount];

            for (int i = 0; i < pointCount; i++)
            {
                int xPx = axis1.xAxis.UnitToPx(Xs[i]);
                int yPx = axis1.yAxis.UnitToPx(Ys[i]);
                points[i] = new Point(xPx, yPx);
            }

            Pen pen = new Pen(new SolidBrush(PlotColor), plotLineWidth);
            gfxData.DrawLines(pen, points);
        }

        /// <summary>
        /// Highspeed plotting of evenly spaced data.
        /// </summary>
        /// <param name="Ys">input data (y-axis units)</param>
        /// <param name="pointSpacing">distance between each data point (x-axis units)</param>
        /// <param name="firstPointX">the X point where this Y data starts (x-axis units)</param>
        /// <param name="offsetY">offset the data by this value (y-axis units)</param>
        public void PlotSignal(List<double> Ys, double pointSpacing = 1.0 / 1000, double firstPointX = 0, double offsetY = 0)
        {

            //TODO: make this run entirely on arrays, not lists.

            List<Point> points = new List<Point>();
            double dataPointsPerPixel = axis1.xAxis.unitsPerPx / pointSpacing;
            double pixelsPerDataPoint = pointSpacing / axis1.xAxis.unitsPerPx;

            // horizontal data density is greater than pixel density, so bin to the pixel size and plot min/max of each bin

            double lastPointX = firstPointX + Ys.Count() * pointSpacing;
            double binUnitsPerPx = axis1.xAxis.unitsPerPx / pointSpacing;
            int dataMinPx = (int)((firstPointX - axis1.xAxis.min) / axis1.xAxis.unitsPerPx);
            int dataMaxPx = (int)((lastPointX - axis1.xAxis.min) / axis1.xAxis.unitsPerPx);


            if (dataPointsPerPixel < 1)
            {
                // data density < pixel density, so plot X/Y pairs
                int iLeftSide = (int)(((axis1.xAxis.min - firstPointX) / axis1.xAxis.unitsPerPx) * dataPointsPerPixel);
                int iRightSide = iLeftSide + (int)(dataPointsPerPixel * bmpData.Width);
                for (int i = Math.Max(0, iLeftSide - 2); i < Math.Min(iRightSide + 3, Ys.Count - 1); i++)
                {
                    int xPx = axis1.xAxis.UnitToPx((double)i * pointSpacing + firstPointX);
                    int yPx = axis1.yAxis.UnitToPx(Ys[i]);
                    points.Add(new Point(xPx, yPx));
                }
            }
            else
            {
                // data density > pixel density, so bin data to pixel size and plot bin min/max
                for (int xPixel = Math.Max(0, dataMinPx); xPixel < Math.Min(bmpData.Width, dataMaxPx); xPixel++)
                {
                    int iLeft = (int)(binUnitsPerPx * (xPixel - dataMinPx));
                    int iRight = (int)(iLeft + binUnitsPerPx);
                    iLeft = Math.Max(iLeft, 0);
                    iRight = Math.Min(Ys.Count - 1, iRight);
                    iRight = Math.Max(iRight, 0);
                    if (iLeft == iRight) continue;
                    double yPxMin = Ys.GetRange(iLeft, iRight - iLeft).Min() + offsetY;
                    double yPxMax = Ys.GetRange(iLeft, iRight - iLeft).Max() + offsetY;
                    points.Add(new Point(xPixel, axis1.yAxis.UnitToPx(yPxMin)));
                    points.Add(new Point(xPixel, axis1.yAxis.UnitToPx(yPxMax)));
                }
            }

            // perform the plot
            Point[] points2 = points.ToArray();
            if (points.Count > 1) gfxData.DrawLines(new Pen(PlotColor, plotLineWidth), points2);
            if (dataPointsPerPixel < .2)
            {
                // density is less than one point per pixel, so show point markers
                int pointSize = 1;
                foreach (Point point in points)
                {
                    Rectangle rect = new Rectangle(point.X - pointSize / 2, point.Y - pointSize / 2, pointSize, pointSize);
                    gfxData.DrawEllipse(new Pen(PlotColor, 3), rect);
                }
            }

        }







        /*
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         *          DEMOS
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         */










        /// <summary>
        /// demo: random size and color lines
        /// </summary>
        public void PlotDemoConfetti(int pieces = 100, int length = 20)
        {
            while (pieces-- > 0)
            {
                Point p1 = new Point(rand.Next(bmpData.Width), rand.Next(bmpData.Height));
                Point p2 = new Point(p1.X + rand.Next(length), p1.Y + rand.Next(length));
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
            double freq = 2 + rand.Next(20);
            double amplitude = 20 + rand.Next(bmpData.Height / 2);
            double offset = rand.Next(bmpData.Height);
            Point[] points = new Point[bmpData.Width];

            for (int i = 0; i < bmpData.Width; i++)
            {
                double y = Math.Sin(shiftX + (double)i * freq / bmpData.Width) * amplitude + offset;
                points[i] = new Point(i, (int)y);
            }

            Pen pen = new Pen(new SolidBrush(Color.FromArgb(150, rand.Next(200), rand.Next(200), rand.Next(200))), 5);
            gfxData.DrawLines(pen, points);
        }

        public void PlotDemoXY()
        {
            List<double> Xs = new List<double>();
            List<double> Ys = new List<double>();

            Xs.Add(-11);
            Ys.Add(rand.NextDouble() * 10 - 5);

            while (Xs[Xs.Count() - 1] < 10)
            {
                Xs.Add(Xs[Xs.Count() - 1] + rand.NextDouble() + .1);
                Ys.Add(Ys[Ys.Count() - 1] + rand.NextDouble() - .5);
            }

            PlotColor = randomColor;
            plotLineWidth = rand.Next(10) + 1;
            PlotLineXY(Xs.ToArray(), Ys.ToArray());
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



        
        public string debug_message
        {
            get
            {
                string msg = "## DEBUG INFORMATION ##";
                msg += string.Format($"\nFigure size: ({bmpFrame.Width}, {bmpFrame.Height})");
                msg += string.Format($"\nData size: ({bmpData.Width}, {bmpData.Height})");
                msg += string.Format($"\nAxis: [%.02f, %.02f, %.02f, %.02f]", axis1.xAxis.min, axis1.xAxis.max, axis1.yAxis.min, axis1.yAxis.max);
                msg += string.Format($"\nMouse Buttons: {(mouse_left_down_axis!=null)}  {(mouse_right_down_axis != null)}");
                return msg;
            }
        }

        public FigureAxis mouse_left_down_axis = null;
        public FigureAxis mouse_right_down_axis = null;
        public Point mouse_left_down_position;
        public Point mouse_right_down_position;

        public void Mouse_left_down(Point mouse_position)
        {
            mouse_left_down_position = mouse_position;
            mouse_left_down_axis = new FigureAxis(axis1.xAxis.min, axis1.xAxis.max, axis1.yAxis.min, axis1.yAxis.max, axis1.xAxis.pxSize, axis1.yAxis.pxSize);
        }

        public void Mouse_right_down(Point mouse_position)
        {
            mouse_right_down_position = mouse_position;
            mouse_right_down_axis = new FigureAxis(axis1.xAxis.min, axis1.xAxis.max, axis1.yAxis.min, axis1.yAxis.max, axis1.xAxis.pxSize, axis1.yAxis.pxSize);
        }

        public void Mouse_left_up(Point mouse_position)
        {
            mouse_left_down_axis = null;
        }

        public void Mouse_right_up(Point mouse_position)
        {
            mouse_right_down_axis = null;
        }

        public void Mouse_move(Point mouse_position)
        {
            if (mouse_left_down_axis != null)
            {
                // left-click-drag panning: shift the axis by the pixel distance dragged times unitsPerPixel.
                double dX = (mouse_left_down_position.X - mouse_position.X) * mouse_left_down_axis.xAxis.unitsPerPx;
                double dY = (mouse_position.Y - mouse_left_down_position.Y) * mouse_left_down_axis.yAxis.unitsPerPx;
                axis1 = new FigureAxis(mouse_left_down_axis.xAxis.min + dX, mouse_left_down_axis.xAxis.max + dX,
                                       mouse_left_down_axis.yAxis.min + dY, mouse_left_down_axis.yAxis.max + dY,
                                       mouse_left_down_axis.xAxis.pxSize, mouse_left_down_axis.yAxis.pxSize);
                Clear();
            }
            else if (mouse_right_down_axis != null)
            {
                // right-click-drag zooming: expand the edges by the same distance of the drag when zooming out, or sqrt(distance) when zooming in.
                double dX = (mouse_right_down_position.X - mouse_position.X) * mouse_right_down_axis.xAxis.unitsPerPx;
                double dY = (mouse_position.Y - mouse_right_down_position.Y) * mouse_right_down_axis.yAxis.unitsPerPx;
                if (dX < 0) dX = -Math.Sqrt(Math.Abs(dX));
                if (dY < 0) dY = -Math.Sqrt(Math.Abs(dY));
                axis1 = new FigureAxis(mouse_right_down_axis.xAxis.min - dX, mouse_right_down_axis.xAxis.max + dX,
                                       mouse_right_down_axis.yAxis.min - dY, mouse_right_down_axis.yAxis.max + dY,
                                       mouse_right_down_axis.xAxis.pxSize, mouse_right_down_axis.yAxis.pxSize);
                Clear();
            }
        }












    }
}
