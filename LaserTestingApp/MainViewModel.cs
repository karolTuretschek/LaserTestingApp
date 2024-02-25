using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserTestingApp
{
    public class MainViewModel
    {
        public MainViewModel(List<double> laserTime, List<double>laserAmbientTemp,List<double>laserDivergence, int ListLength)
        {
            var r = new Random(314);

            this.MyModel2 = new PlotModel { Title = "Points Plot" };
            var scatterSeries2 = new ScatterSeries { MarkerType = MarkerType.Circle };
            for (int i = 0; i < ListLength; i++)
            {
                var size = r.Next(5, 15);
                scatterSeries2.Points.Add(new ScatterPoint(laserTime[i], laserAmbientTemp[i], size, laserDivergence[i]));
            }
            this.MyModel2.Series.Add(scatterSeries2);
            this.MyModel2.Series.Add(new ScatterSeries { MarkerType = MarkerType.Circle });
            this.MyModel2.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Jet(200) });
        }
        public PlotModel MyModel2 { get; set; }
    }
}
