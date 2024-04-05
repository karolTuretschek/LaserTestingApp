﻿using OxyPlot.Annotations;
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
        public void viewModelScatter(string yLabel, string xLabel, List<double> x, List<double> y, List<double> y2, int ListLength, double DotSize)
        {
            //Scatter graph
            MyModel2 = new PlotModel { Title = "Scatter Plot" };
            var scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerSize = 55 };
            for (int i = 0; i < ListLength; i++)
            {
                scatterSeries.Points.Add(new ScatterPoint(x[i], y[i], DotSize, y2[i]));
            }
            MyModel2.Series.Add(scatterSeries);
            MyModel2.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Jet(200) });
            MyModel2.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = yLabel, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, MinimumRange = 0, MaximumRange = 100, AbsoluteMaximum = 50, AbsoluteMinimum = 0 });
            MyModel2.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = xLabel, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, MinimumRange = 0, MaximumRange = 100, AbsoluteMaximum = 50, AbsoluteMinimum = 0 });
        }
        public void viewModelScatterReverse(string yLabel, string xLabel, List<double> x, List<double> y, List<double> y2, int ListLength, double DotSize)
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
        public void viewModelScatterReset(string yLabel, string xLabel, List<double> x, List<double> y, List<double> y2, int ListLength, double DotSize)
        {
            //Scatter graph
            MyModel2 = new PlotModel { Title = "Scatter Plot" };
            var scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerSize = 55 };
            MyModel2.Series.Add(scatterSeries);
        }
        public void viewModelFast(List<double> x, List<double> y, List<double> y2, List<double> y3, List<double> y4, int ListLength)
        {
            MyModel3a = new PlotModel { Title = "y" };
            var sub1 = new LineSeries { MarkerType = MarkerType.Circle, Color = OxyColors.Green };
            for (int i = 0; i < ListLength; i++)
            {
                sub1.Points.Add(new DataPoint(x[i], y[i]));
            }
            MyModel3a.Series.Add(sub1);

            MyModel3b = new PlotModel { Title = "y2" };
            var sub2 = new LineSeries { MarkerType = MarkerType.Circle, Color = OxyColors.Orange };
            for (int i = 0; i < ListLength; i++)
            {
                sub2.Points.Add(new DataPoint(x[i], y2[i]));
            }
            MyModel3b.Series.Add(sub2);

            MyModel3c = new PlotModel { Title = "y3" };
            var sub3 = new LineSeries { MarkerType = MarkerType.Circle, Color = OxyColors.Blue };
            for (int i = 0; i < ListLength; i++)
            {
                sub3.Points.Add(new DataPoint(x[i], y3[i]));
            }
            MyModel3c.Series.Add(sub3);

            MyModel3d = new PlotModel { Title = "y4" };
            var sub4 = new LineSeries { MarkerType = MarkerType.Circle, Color = OxyColors.Red };
            for (int i = 0; i < ListLength; i++)
            {
                sub4.Points.Add(new DataPoint(x[i], y4[i]));
            }
            MyModel3d.Series.Add(sub4);
        }
        public void viewModelFastReverse(string yLabel, string xLabel, List<double> x, List<double> y, List<double> y2, int ListLength, double DotSize)
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
        public void viewModelFastReset(string yLabel, string xLabel, List<double> x, List<double> y, List<double> y2, int ListLength, double DotSize)
        {
            //Fast Line
            MyModel3 = new PlotModel { Title = "Fast Line" };
            var lineSeries3 = new LineSeries();
            var lineSeries4 = new LineSeries();
            MyModel3.Series.Add(lineSeries3); // First line of data
            MyModel3.Series.Add(lineSeries4); // Second
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
        public void viewModelLineReverse(string yLabel, string xLabel, List<double> x, List<double> y, List<double> y2, int ListLength, double DotSize)
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
        public void viewModelLineReset(string yLabel, string xLabel, List<double> x, List<double> y, List<double> y2, int ListLength, double DotSize)
        {
            //Scatter Line
            MyModel = new PlotModel { Title = "Line Plot" };
            var lineSeries = new LineSeries { MarkerType = MarkerType.Circle };
            MyModel.Series.Add(lineSeries);
        }
        static void LinearInterpolation(List<double> x, List<double> y, out List<double> interpolatedX, out List<double> interpolatedY)
        {
            interpolatedX = new List<double>();
            interpolatedY = new List<double>();

            for (int i = 0; i < x.Count - 1; i++)
            {
                double startX = x[i];
                double endX = x[i + 1];
                double startY = y[i];
                double endY = y[i + 1];

                // Calculate the slope
                double slope = (endY - startY) / (endX - startX);

                // Interpolate points between startX and endX
                for (double j = startX; j < endX; j += 0.1) // Change the step size for smoother interpolation
                {
                    interpolatedX.Add(j);
                    interpolatedY.Add(startY + slope * (j - startX));
                }
            }

            // Add the last point
            interpolatedX.Add(x[x.Count - 1]);
            interpolatedY.Add(y[y.Count - 1]);
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
        public PlotModel MyModel2 { get; set; }
        public PlotModel MyModel3 { get; set; }
        public PlotModel MyModel3a { get; set; }
        public PlotModel MyModel3b { get; set; }
        public PlotModel MyModel3c { get; set; }
        public PlotModel MyModel3d { get; set; }
    }
}
