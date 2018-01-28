using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScottPlot
{
    class FigureAxis
    {
        public Axis xAxis { get; private set; }
        public Axis yAxis { get; private set; }

        /// <summary>
        /// A FigureAxis holds an X-axis and Y-axis and proves easy ways to zoom, pan, and convert between 
        /// axis positions and pixel positions.
        /// </summary>
        /// <param name="xMin">left side of the graph (units)</param>
        /// <param name="xMax">right side of the graph (units)</param>
        /// <param name="yMin">lower side of the graph (units)</param>
        /// <param name="yMax">upper side of the graph (units)</param>
        /// <param name="widthPx">width of the graph image (px)</param>
        /// <param name="heightPx">height of the graph image (px)</param>
        public FigureAxis(double xMin, double xMax, double yMin, double yMax, int widthPx, int heightPx)
        {
            // expand axis to make sure we don't get too crazy small
            double xCenter = (xMin + xMax) / 2;
            double yCenter = (yMin + yMax) / 2;
            double xHalfWidth = xCenter - xMin;
            double yHalfWidth = yCenter - yMin;
            double limitHalfWidth = .001;
            if (xHalfWidth < limitHalfWidth)
            {
                xHalfWidth = limitHalfWidth;
                xMin = xCenter - limitHalfWidth;
                xMax = xCenter + limitHalfWidth;
            }
            if (yHalfWidth < limitHalfWidth)
            {
                yHalfWidth = limitHalfWidth;
                yMin = yCenter - limitHalfWidth;
                yMax = yCenter + limitHalfWidth;
            }

            // build each axis with our valid limits
            xAxis = new Axis(xMin, xMax, widthPx);
            yAxis = new Axis(yMin, yMax, heightPx, true);
        }

        /// <summary>
        /// Re-derive unit/pixel scales. Call then when manyally adjusting axis min and max.
        /// </summary>
        private void UpdateConversionFactors()
        {
            xAxis.RecalculateScale();
            yAxis.RecalculateScale();
        }

        /// <summary>
        /// Adjust the edges of the axis (units).
        /// </summary>
        /// <param name="xMin">left side of the graph (units)</param>
        /// <param name="xMax">right side of the graph (units)</param>
        /// <param name="yMin">lower side of the graph (units)</param>
        /// <param name="yMax">upper side of the graph (units)</param>
        public void AxisView(double? xMin, double? xMax, double? yMin, double? yMax)
        {
            if (xMin != null) xAxis.min = (double)xMin;
            if (xMax != null) xAxis.max = (double)xMax;
            if (yMin != null) yAxis.min = (double)yMin;
            if (yMax != null) yAxis.max = (double)yMax;
            UpdateConversionFactors();
        }

        /// <summary>
        /// Adjust the size of the axis on the screen (pixels)
        /// </summary>
        /// <param name="widthPx">width of the axis (pixels)</param>
        /// <param name="heightPx">height of the axis (pixels)</param>
        public void Resize(int widthPx, int heightPx)
        {
            xAxis.ResizePx(widthPx);
            yAxis.ResizePx(heightPx);
            UpdateConversionFactors();
        }

        /// <summary>
        /// Zoom by a fraction on the center of an axis.
        /// Values below 1 zoom in, those above 1 zoom out.
        /// </summary>
        /// <param name="xFrac">horizontal zoom fraction</param>
        /// <param name="yFrac">vertical zoom fraction</param>
        public void Zoom(double? xFrac, double? yFrac)
        {
            if (xFrac != null) xAxis.Zoom((double)xFrac);
            if (yFrac != null) yAxis.Zoom((double)yFrac);
        }

        /// <summary>
        /// Shift the axis horizontally and/or vertically with no change in zoom.
        /// </summary>
        /// <param name="xShift"></param>
        /// <param name="yShift"></param>
        public void Pan(double? xShift, double? yShift)
        {
            if (xShift != null) xAxis.Pan((double)xShift);
            if (yShift != null) yAxis.Pan((double)yShift);
        }

        /// <summary>
        /// Formatted summary of figure/axis information
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Axis2d: X:{xAxis.ToString()} Y:{yAxis.ToString()}";
        }
    }
}
