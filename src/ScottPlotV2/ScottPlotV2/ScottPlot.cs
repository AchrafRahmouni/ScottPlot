using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScottPlotV2
{
    class ScottPlot
    {

        private int figure_width, figure_height;

        /// <summary>
        /// Create a new ScottPlot
        /// </summary>
        /// <param name="figure_width">size of the image (pixels)</param>
        /// <param name="figure_height">size of the image (pixels)</param>
        public ScottPlot(int figure_width=640, int figure_height=480)
        {
            this.figure_width = figure_width;
            this.figure_height = figure_height;
        }
    }
}
