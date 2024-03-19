using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace LaserTestingApp
{
    public class MainViewModel
    {
        MainWindow myWindow = new MainWindow();
        public void ViewModelScatter(string yLabel, string xLabel , List<double> x, List<double>y , List<double>y2, int ListLength, double DotSize)
        {
            //Scatter graph
            MyModel2 = new PlotModel { Title = "Scatter Plot" };
            var scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerSize = 55};
            for (int i = 0; i < ListLength; i++)
            {
                scatterSeries.Points.Add(new ScatterPoint(x[i], y[i], DotSize, y2[i]));
            }
            MyModel2.Series.Add(scatterSeries);
            MyModel2.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Jet(200)});
            MyModel2.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = yLabel, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, MinimumRange = 0, MaximumRange = 100, AbsoluteMaximum = 50, AbsoluteMinimum = 0});
            MyModel2.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = xLabel, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, MinimumRange = 0, MaximumRange = 100, AbsoluteMaximum = 50, AbsoluteMinimum = 0});
        }
        public void ViewModelScatterReverse(string yLabel, string xLabel, List<double> x, List<double> y, List<double> y2, int ListLength, double DotSize)
        {
            //Scatter graph
            MyModel2 = new PlotModel { Title = "Scatter Plot" };
            var scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerSize = 55 };
            for (int i = 0; i < ListLength; i++)
            {
                scatterSeries.Points.Add(new ScatterPoint(y[i], x[i], DotSize, y2[i]));
            }
            MyModel2.Series.Add(scatterSeries);
            MyModel2.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Jet(200) });
            MyModel2.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = yLabel, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, MinimumRange = 0, MaximumRange = 100, AbsoluteMaximum = 50, AbsoluteMinimum = 0 });
            MyModel2.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = xLabel, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, MinimumRange = 0, MaximumRange = 100, AbsoluteMaximum = 50, AbsoluteMinimum = 0 });
        }
        public void ViewModelScatterReset(string yLabel, string xLabel, List<double> x, List<double> y, List<double> y2, int ListLength, double DotSize)
        {
            //Scatter graph
            MyModel2 = new PlotModel { Title = "Scatter Plot" };
            var scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerSize = 55 };
            MyModel2.Series.Add(scatterSeries);
        }
        public void ViewModelFast(string yLabel, string xLabel, List<double> x, List<double> y, List<double> y2, int ListLength, double DotSize)
        {           
            //Fast Line
            MyModel3 = new PlotModel { Title = "Fast Line" };
            var lineSeries3 = new LineSeries();
            var lineSeries4 = new LineSeries();
            for (int i = 0; i < ListLength; i++)
            {
                lineSeries3.Points.Add(new DataPoint(x[i], y[i]));
                lineSeries4.Points.Add(new DataPoint(x[i], y2[i]));
            }

            MyModel3.Series.Add(lineSeries3); // First line of data
            MyModel3.Series.Add(lineSeries4); // Second
            MyModel3.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Jet(200) });
            MyModel3.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });
            MyModel3.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = xLabel, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });
        }
        public void ViewModelFastReverse(string yLabel, string xLabel, List<double> x, List<double> y, List<double> y2, int ListLength, double DotSize)
        {
            //Fast Line
            MyModel3 = new PlotModel { Title = "Fast Line" };
            var lineSeries3 = new LineSeries();
            var lineSeries4 = new LineSeries();
            for (int i = 0; i < ListLength; i++)
            {
                lineSeries3.Points.Add(new DataPoint(y[i], x[i]));
                lineSeries4.Points.Add(new DataPoint(y2[i], x[i]));
            }

            MyModel3.Series.Add(lineSeries3); // First line of data
            MyModel3.Series.Add(lineSeries4); // Second
            MyModel3.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Jet(200) });
            MyModel3.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });
            MyModel3.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = xLabel, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });
        }
        public void ViewModelFastReset(string yLabel, string xLabel, List<double> x, List<double> y, List<double> y2, int ListLength, double DotSize)
        {
            //Fast Line
            MyModel3 = new PlotModel { Title = "Fast Line" };
            var lineSeries3 = new LineSeries();
            var lineSeries4 = new LineSeries();
            MyModel3.Series.Add(lineSeries3); // First line of data
            MyModel3.Series.Add(lineSeries4); // Second
        }
        public void ViewModelLine(string yLabel, string xLabel, List<double> x, List<double> y, List<double> y2, int ListLength, double DotSize)
        {
            //Scatter Line
            MyModel = new PlotModel { Title = "Line Plot" };
            var lineSeries = new LineSeries { MarkerType = MarkerType.Circle };
            for (int i = 0; i < ListLength; i++)
            {
                lineSeries.Points.Add(new DataPoint(x[i], y2[i]));
            }

            MyModel.Series.Add(lineSeries);
            MyModel.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Jet(200) });
            MyModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = yLabel, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });
            MyModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = xLabel, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });

            //Create line
            double horizontalLineYValue = 1.0; // At 1.0 of the divergence
            var horizontalLine = new LineAnnotation
            {
                Type = LineAnnotationType.Horizontal,
                Y = horizontalLineYValue,
                Color = OxyColors.LightSalmon,
                StrokeThickness = 1.5
            };
            double horizontalLineYValueLimitUp = 1.02;   // Upper limit       
            var horizontalLineUp = new LineAnnotation
            {
                Type = LineAnnotationType.Horizontal,
                Y = horizontalLineYValueLimitUp,
                Color = OxyColors.Red,
                StrokeThickness = 1.5
            };
            double horizontalLineYValueLimitDown = 0.98; // Lower limit
            var horizontalLineDown = new LineAnnotation
            {
                Type = LineAnnotationType.Horizontal,
                Y = horizontalLineYValueLimitDown,
                Color = OxyColors.Red,
                StrokeThickness = 1.5
            };
            MyModel.Annotations.Add(horizontalLine);
            MyModel.Annotations.Add(horizontalLineUp);
            MyModel.Annotations.Add(horizontalLineDown);
        }
        public void ViewModelLineReverse(string yLabel, string xLabel, List<double> x, List<double> y, List<double> y2, int ListLength, double DotSize)
        {
            //Scatter Line
            MyModel = new PlotModel { Title = "Line Plot" };
            var lineSeries = new LineSeries { MarkerType = MarkerType.Circle };
            for (int i = 0; i < ListLength; i++)
            {
                lineSeries.Points.Add(new DataPoint(y2[i], x[i]));
            }

            MyModel.Series.Add(lineSeries);
            MyModel.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Jet(200) });
            MyModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = yLabel, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });
            MyModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = xLabel, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });

            //Create line
            double horizontalLineYValue = 1.0; // At 1.0 of the divergence
            var horizontalLine = new LineAnnotation
            {
                Type = LineAnnotationType.Horizontal,
                Y = horizontalLineYValue,
                Color = OxyColors.LightSalmon,
                StrokeThickness = 1.5
            };
            double horizontalLineYValueLimitUp = 1.02;   // Upper limit       
            var horizontalLineUp = new LineAnnotation
            {
                Type = LineAnnotationType.Horizontal,
                Y = horizontalLineYValueLimitUp,
                Color = OxyColors.Red,
                StrokeThickness = 1.5
            };
            double horizontalLineYValueLimitDown = 0.98; // Lower limit
            var horizontalLineDown = new LineAnnotation
            {
                Type = LineAnnotationType.Horizontal,
                Y = horizontalLineYValueLimitDown,
                Color = OxyColors.Red,
                StrokeThickness = 1.5
            };
            MyModel.Annotations.Add(horizontalLine);
            MyModel.Annotations.Add(horizontalLineUp);
            MyModel.Annotations.Add(horizontalLineDown);
        }
        public void ViewModelLineReset(string yLabel, string xLabel, List<double> x, List<double> y, List<double> y2, int ListLength, double DotSize)
        {
            //Scatter Line
            MyModel = new PlotModel { Title = "Line Plot" };
            var lineSeries = new LineSeries { MarkerType = MarkerType.Circle };
            MyModel.Series.Add(lineSeries);
        }
        public PlotModel MyModel { get; set; }
        public PlotModel MyModel2 { get; set; }
        public PlotModel MyModel3 { get; set; }
        public ComboBox myCombo { get; set; }
        public Slider mySlider { get; set; }
    }
}
