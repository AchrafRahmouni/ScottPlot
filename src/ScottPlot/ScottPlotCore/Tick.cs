using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScottPlot
{
    class Tick
    {
        public double value { get; set; }
        public int pixel { get; set; }
        public double axisSpan { get; set; }

        public Tick(double value, int pixel, double axisSpan)
        {
            this.value = value;
            this.pixel = pixel;
            this.axisSpan = axisSpan;
        }

        public string label
        {
            get
            {
                if (axisSpan < .01) return string.Format("{0:0.0000}", value);
                if (axisSpan < .1) return string.Format("{0:0.000}", value);
                if (axisSpan < 1) return string.Format("{0:0.00}", value);
                if (axisSpan < 10) return string.Format("{0:0.0}", value);
                return string.Format("{0:0}", value);
            }
        }
    }
}
