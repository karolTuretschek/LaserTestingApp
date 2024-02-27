using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LaserTestingApp
{
    public class MainViewModel
    {
        MainWindow myWindow = new MainWindow();
        public MainViewModel(string yLabel, string xLabel,List<double> laserTime, List<double>laserAmbientTemp,List<double>laserDivergence, int ListLength)
        {
            
            var r = new Random(314);

            this.MyModel2 = new PlotModel { Title = "Points Plot" };
            var scatterSeries2 = new ScatterSeries { MarkerType = MarkerType.Circle };
            for (int i = 0; i < ListLength; i++)
            {
                var size = r.Next(5, 15);
                scatterSeries2.Points.Add(new ScatterPoint(laserTime[i], laserAmbientTemp[i], laserDivergence[i]*10, laserDivergence[i]));
            }
            this.MyModel2.Series.Add(scatterSeries2);
            this.MyModel2.Series.Add(new ScatterSeries { MarkerType = MarkerType.Circle });
            this.MyModel2.Series.Add(new ScatterSeries { MarkerSize = 55});
            this.MyModel2.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Jet(200) });
            this.MyModel2.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = yLabel, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });
            this.MyModel2.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = xLabel, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });
        }

        public PlotModel MyModel2 { get; set; }
        public ComboBox myCombo { get; set; }
    }
}
