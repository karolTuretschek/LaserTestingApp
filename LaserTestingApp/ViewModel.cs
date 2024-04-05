using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Plotly.NET;
using System.Windows.Controls;

namespace LaserTestingApp
{
    class ViewModel
    {
        MainWindow myWindow = new MainWindow();
        public void viewModelFast(List<double> x, List<double> y, List<double> y2, List<double> y3, List<double> y4, int ListLength)
        {
            var sub1 = new LineSeries { MarkerType = MarkerType.Circle, Color = OxyColors.Green };
            for (int i = 0; i < ListLength; i++)
            {
                sub1.Points.Add(new DataPoint(x[i], y[i]));
            }
            MyModel3a.Series.Add(sub1);

            var sub2 = new LineSeries { MarkerType = MarkerType.Circle, Color = OxyColors.Orange };
            for (int i = 0; i < ListLength; i++)
            {
                sub2.Points.Add(new DataPoint(x[i], y2[i]));
            }
            MyModel3b.Series.Add(sub2);

            var sub3 = new LineSeries { MarkerType = MarkerType.Circle, Color = OxyColors.Blue };
            for (int i = 0; i < ListLength; i++)
            {
                sub3.Points.Add(new DataPoint(x[i], y3[i]));
            }
            MyModel3c.Series.Add(sub3);

            var sub4 = new LineSeries { MarkerType = MarkerType.Circle, Color = OxyColors.Red };
            for (int i = 0; i < ListLength; i++)
            {
                sub4.Points.Add(new DataPoint(x[i], y4[i]));
            }
            MyModel3d.Series.Add(sub4);

        }
        public void viewModelFastReset(string yLabel, string xLabel, List<double> x, List<double> y, List<double> y2, int ListLength, double DotSize)
        {
            //Fast Line
            MyModel3a = new PlotModel { Title = "Fast Line" };
            MyModel3b = new PlotModel { Title = "Fast Line" };
            MyModel3c = new PlotModel { Title = "Fast Line" };
            MyModel3d = new PlotModel { Title = "Fast Line" };
            var lineSeriesa = new LineSeries { MarkerType = MarkerType.Circle };
            var lineSeriesb = new LineSeries { MarkerType = MarkerType.Circle };
            var lineSeriesc = new LineSeries { MarkerType = MarkerType.Circle };
            var lineSeriesd = new LineSeries { MarkerType = MarkerType.Circle };
            MyModel3a.Series.Add(lineSeriesa);
            MyModel3b.Series.Add(lineSeriesb); 
            MyModel3c.Series.Add(lineSeriesc); 
            MyModel3d.Series.Add(lineSeriesd); 
        }
        public void viewModelLineY4(List<double> x, List<double> y4, int ListLength)
        {
            var lineSeries4 = new LineSeries { MarkerType = MarkerType.Circle, Color = OxyColors.Green };
            // y4
            for (int i = 0; i <= ListLength; i++)
            {
                if (y4[i].Equals(double.NaN))
                {
                    lineSeries4.Points.Add(new DataPoint(x[i], double.NaN));
                }
                else
                {
                    lineSeries4.Points.Add(new DataPoint(x[i], y4[i]));
                }
            }
            MyModel.Series.Add(lineSeries4);
        }
        public void viewModelLineY4Gaps(List<double> x, List<double> y4, int ListLength, Dictionary<double, double> GapsY4)
        {
            // y4
            for (int i = 2; i < ListLength; i++)
            {
                if (y4[i].Equals(double.NaN))
                {
                    continue;
                }
                else
                {
                    foreach (var item in GapsY4)
                    {
                        if (x[i] == item.Key)
                        {
                            var gapSeries = new LineAnnotation { Text = "^", FontSize = 25 };
                            gapSeries.TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Center;
                            gapSeries.TextPosition = new DataPoint(item.Key, y4[i]);
                            MyModel.Annotations.Add(gapSeries);
                        }
                    }
                }
            }
        }
        public void viewModelLineY3(List<double> x, List<double> y3, int ListLength)
        {
            var lineSeries3 = new LineSeries { MarkerType = MarkerType.Circle, Color = OxyColors.Orange };
            // y3
            for (int i = 0; i <= ListLength; i++)
            {
                if (y3[i].Equals(double.NaN))
                {
                    lineSeries3.Points.Add(new DataPoint(x[i], double.NaN));
                }
                else
                {
                    lineSeries3.Points.Add(new DataPoint(x[i], y3[i]));
                }
            }
            MyModel.Series.Add(lineSeries3);
        }
        public void viewModelLineY3Gaps(List<double> x, List<double> y3, int ListLength, Dictionary<double, double> GapsY3)
        {
            // y3
            for (int i = 2; i < ListLength; i++)
            {
                if (y3[i].Equals(double.NaN))
                {
                    continue;
                }
                else
                {
                    foreach (var item in GapsY3)
                    {
                        if (x[i] == item.Key)
                        {
                            var gapSeries = new LineAnnotation { Text = "^", FontSize = 25 };
                            gapSeries.TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Center;
                            gapSeries.TextPosition = new DataPoint(item.Key, y3[i]);
                            MyModel.Annotations.Add(gapSeries);
                        }
                    }
                }
            }
        }
        public void viewModelLineY2(List<double> x, List<double> y2, int ListLength)
        {
            var lineSeries2 = new LineSeries { MarkerType = MarkerType.Circle, Color = OxyColors.Blue };
            // y2
            for (int i = 0; i <= ListLength; i++)
            {
                if (y2[i].Equals(double.NaN))
                {
                    lineSeries2.Points.Add(new DataPoint(x[i], double.NaN));
                }
                else
                {
                    lineSeries2.Points.Add(new DataPoint(x[i], y2[i]));
                }
            }
            MyModel.Series.Add(lineSeries2);
        }
        public void viewModelLineY2Gaps(List<double> x, List<double> y2, int ListLength, Dictionary<double, double> GapsY2)
        {
            // y2
            for (int i = 2; i < ListLength; i++)
            {
                if (y2[i].Equals(double.NaN))
                {
                    continue;
                }
                else
                {                   
                    foreach (var item in GapsY2)
                    {
                        if (x[i] == item.Key)
                        {
                            var gapSeries = new LineAnnotation { Text = "^", FontSize = 25 };
                            gapSeries.TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Center;
                            gapSeries.TextPosition = new DataPoint(item.Key, y2[i]);
                            MyModel.Annotations.Add(gapSeries);
                        }
                    }
                }
            }
        }
        public void viewModelLine(List<double> x, List<double> y, int ListLength)
        {
            //Scatter Line
            var lineSeries = new LineSeries { MarkerType = MarkerType.Circle, Color = OxyColors.Red};
            var lineSeriesAverage = new LineSeries { MarkerType = MarkerType.Triangle };
            var lineSeriesTrend = new LineSeries { MarkerType = MarkerType.Triangle };
            // y
            for (int i = 0; i <= ListLength; i++)
            {
                if (y[i].Equals(double.NaN))
                {
                    lineSeries.Points.Add(new DataPoint(x[i], double.NaN));
                }
                else
                {
                    lineSeries.Points.Add(new DataPoint(x[i], y[i]));
                }
            }
            MyModel.Series.Add(lineSeriesTrend);
            MyModel.Series.Add(lineSeries);
            MyModel.Series.Add(lineSeriesAverage);
            // Interpolation implementation
            var series = new LineSeries
            {
                InterpolationAlgorithm = InterpolationAlgorithms.CanonicalSpline
            };
            for (int i = 0; i < ListLength; i++)
            {
                series.Points.Add(new DataPoint(x[i], y[i]));
            }
            MyModel.Series.Add(series);
        }
        private double CalculateStandardDeviation(List<double> values, double mean)
        {
            double sumOfSquaredDifferences = values.Sum(value => Math.Pow(value - mean, 2));
            double variance = sumOfSquaredDifferences / values.Count;
            double standardDeviation = Math.Sqrt(variance);
            return standardDeviation;
        }
        public void viewModelLineGaps(List<double> x, List<double> y, int ListLength, Dictionary<double, double> GapsY)
        {
            // y
            for (int i = 2; i < ListLength; i++)
            {
                if (y[i].Equals(double.NaN))
                {
                    continue;
                }
                else
                {
                    foreach (var item in GapsY)
                    {
                        if (x[i] == item.Key)
                        {
                            var gapSeries = new LineAnnotation { Text = "^", FontSize = 25 };
                            gapSeries.TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Center;
                            gapSeries.TextPosition = new DataPoint(item.Key, y[i]);
                            MyModel.Annotations.Add(gapSeries);
                        }
                    }
                }
            }
        }
        public void createUpperLimit(int ListLength, double UpperLimit)
        {
            // Upper limit       
            var horizontalLineUp = new LineAnnotation
            {
                Type = LineAnnotationType.Horizontal,
                Y = UpperLimit,
                Text = "Max Temp",
                TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Right,
                TextVerticalAlignment = OxyPlot.VerticalAlignment.Top,
                Color = OxyColors.OrangeRed,
                StrokeThickness = 1.0,
                MinimumX = 0,
                MaximumX = ListLength
            };
            MyModel.Annotations.Add(horizontalLineUp);
        }
        public void createLowerLimit(int ListLength, double LowerLimit)
        {
            // Lower limit
            var horizontalLineDown = new LineAnnotation
            {
                Type = LineAnnotationType.Horizontal,
                Text = "Min Temp",
                TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Right,
                TextVerticalAlignment = OxyPlot.VerticalAlignment.Top,
                Y = LowerLimit,
                Color = OxyColors.Aqua,
                StrokeThickness = 1.0,
                MinimumX = 0,
                MaximumX = ListLength
            };
            MyModel.Annotations.Add(horizontalLineDown);
        }
        public void viewModelLineReset(string yLabel, string xLabel, List<double> x, List<double> y, List<double> y2, int ListLength, double DotSize)
        {
            //Scatter Line
            MyModel = new PlotModel { Title = "Line Plot" };
            var lineSeries = new LineSeries { MarkerType = MarkerType.Circle };
            MyModel.Series.Add(lineSeries);
        }
        public double CalculateDistanceBetweenPoints(List<double> x, List<double> y2, int ListLength, double distanceMax)
        {
            for (int i = 1; i < ListLength; i++)
            {
                //double squaredDifferenceX = Math.Pow(x[i] - x[i-1], 2);
                //double squaredDifferenceY = Math.Pow(y2[i] - y2[i-1], 2);
                //Debug.WriteLine($" {squaredDifferenceX} + { squaredDifferenceY}");
                double distance = Math.Sqrt(Math.Pow(x[i] - x[i - 1], 2) + Math.Pow(y2[i] - y2[i - 1], 2));
                //double distance = Math.Sqrt(squaredDifferenceX + squaredDifferenceY);
                if (distance > distanceMax)
                {
                    distanceMax = distance;
                }
            }

            return distanceMax;
        }
        public Dictionary<double, double> GapsDictionary { get; set; }
        public PlotModel MyModel { get; set; }
        public PlotModel MyModel3 { get; set; }
        public PlotModel MyModel3a { get; set; }
        public PlotModel MyModel3b { get; set; }
        public PlotModel MyModel3c { get; set; }
        public PlotModel MyModel3d { get; set; }
    }
}
