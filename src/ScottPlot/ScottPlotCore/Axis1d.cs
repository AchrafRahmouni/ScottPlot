using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScottPlot
{

    class Axis
    {
        // user provides these
        public double min { get; set; }
        public double max { get; set; }
        public int pxSize { get; private set; }
        public bool pxInverted { get; set; }

        // these are calculated
        public double unitsPerPx { get; private set; }
        public double pxPerUnit { get; private set; }

        // these relate to ticks
        public double[] TickValues { get; private set; }
        public string[] TickLabels { get; private set; }
        
        /// <summary>
        /// Single-dimensional axis (i.e., x-axis)
        /// </summary>
        /// <param name="Min">lower bound (units)</param>
        /// <param name="Max">upper bound (units)</param>
        /// <param name="pxSize">size of this axis (pixels)</param>
        /// <param name="pxInverted">inverted axis vs. pixel position (common for Y-axis)</param>
        public Axis(double Min, double Max, int pxSize = 500, bool pxInverted = false)
        {
            this.min = Min;
            this.max = Max;
            this.pxInverted = pxInverted;
            ResizePx(pxSize);
        }

        /// <summary>
        /// Tell the Axis how large it will be on the screen
        /// </summary>
        /// <param name="sizePx">size of this axis (pixels)</param>
        public void ResizePx(int sizePx)
        {
            this.pxSize = sizePx;
            RecalculateScale();
        }

        /// <summary>
        /// Update units/pixels conversion scales.
        /// </summary>
        public void RecalculateScale()
        {
            this.pxPerUnit = (double)pxSize / (max - min);
            this.unitsPerPx = (max - min) / (double)pxSize;

        }

        /// <summary>
        /// Shift the Axis by a specified amount
        /// </summary>
        /// <param name="Shift">distance (units)</param>
        public void Pan(double Shift)
        {
            min += Shift;
            max += Shift;
        }

        /// <summary>
        /// Zoom in on the center of Axis by a fraction. &lt;1 zooms out, &gt; zooms in.
        /// </summary>
        /// <param name="fracX">Fractional amount to zoom</param>
        public void Zoom(double fracX)
        {
            double center = (min + max) / 2;
            double halfPad = center - min;
            min = center - halfPad * fracX;
            max = center + halfPad * fracX;
            RecalculateScale();
        }

        /// <summary>
        /// Given a position on the axis (in units), return its position on the screen (in pixels).
        /// Returned values may be negative, or greater than the pixel width.
        /// </summary>
        /// <param name="unit">position (units)</param>
        /// <returns></returns>
        public int UnitToPx(double unit)
        {
            int px = (int)((unit - min) * pxPerUnit);
            if (pxInverted) px = pxSize - px;
            return px;
        }

        /// <summary>
        /// Given a position on the screen (in pixels), return its location on the axis (in units).
        /// </summary>
        /// <param name="px">position (pixels)</param>
        /// <returns></returns>
        public double PxToUnit(int px)
        {
            if (pxInverted) px = pxSize - px;
            return min + (double)px * unitsPerPx;
        }

        /// <summary>
        /// Formatted summary of axis information
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"({min},{max})";
        }

        /// <summary>
        /// Given an arbitrary number, return the nearerest round number
        /// (i.e., 1000, 500, 100, 50, 10, 5, 1, .5, .1, .05, .01)
        /// </summary>
        /// <param name="target">the number to approximate</param>
        /// <returns></returns>
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
        /// Re-calculate ideal tick values given the axis and size.
        /// </summary>
        public Tick[] Ticks(int targetTickCount)
        {
            List<Tick> ticks = new List<Tick>();

            if (targetTickCount > 0)
            {
                double tickSize = RoundNumberNear(((max - min) / targetTickCount) * 1.5);
                int lastTick = 123456789;
                for (int i = 0; i < pxSize; i++)
                {
                    double thisPosition = i * unitsPerPx + min;
                    int thisTick = (int)(thisPosition / tickSize);
                    if (thisTick != lastTick)
                    {
                        lastTick = thisTick;
                        double thisPositionRounded = (double)((int)(thisPosition / tickSize) * tickSize);
                        if (thisPositionRounded > min && thisPositionRounded < max)
                        {
                            ticks.Add(new Tick(thisPositionRounded, UnitToPx(thisPositionRounded), max-min));
                        }
                    }
                }
            }
            return ticks.ToArray();
        }
    }
}
