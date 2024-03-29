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
        public void viewModelFast(string yLabel, string xLabel, List<double> x, List<double> y, List<double> y2, int ListLength, double DotSize)
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
        public void viewModelLine(string yLabel, string xLabel, List<double> x, List<double> y, List<double> y2, int ListLength, double DotSize, Dictionary<double, double> Gaps)
        {
            //Scatter Line
            MyModel = new PlotModel { Title = "Line Plot" };
            var lineSeries = new LineSeries { MarkerType = MarkerType.Circle };
            var lineSeriesAverage = new LineSeries { MarkerType = MarkerType.Triangle };
            List<double> interpolatedX, interpolatedY;
            var lineSeriesTrend = new LineSeries { MarkerType = MarkerType.Triangle };

            for (int i = 1; i < ListLength; i++)
            {
                if (y2[i].Equals(double.NaN))
                {
                    lineSeries.Points.Add(new DataPoint(x[i], double.NaN));
                }

                else
                {
                    foreach (var item in Gaps)
                    {
                        if (x[i] == item.Key)
                        {
                            var gapSeries = new LineAnnotation { Text = "^", FontSize = 25 };
                            gapSeries.TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Center;
                            gapSeries.TextPosition = new DataPoint(item.Key, y2[i]);
                            MyModel.Annotations.Add(gapSeries);
                        }

                    }
                    lineSeries.Points.Add(new DataPoint(x[i], y2[i]));
                }

            }
            Debug.WriteLine(Gaps.Count());// Confirm data is being passed on from other class
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
                series.Points.Add(new DataPoint(x[i], y2[i]));

            }
            // TODO:
            // Consider creating either an average of the values before and after the missing value
            // Or, create a small interpolation between these two values to fill the gap
            MyModel.Series.Add(series);
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
            var rectangleAnnotation = new RectangleAnnotation
            {
                MinimumX = 0.0,
                MaximumX = (double)ListLength,
                MinimumY = double.Parse(myWindow.UnitMinOperatingTemperatureTextBox.Text.ToString()),
                MaximumY = double.Parse(myWindow.UnitMaxOperatingTemperatureTextBox.Text.ToString()),
                Fill = OxyColor.FromRgb(215, 255, 255),
                Layer = AnnotationLayer.BelowAxes
            };
            MyModel.Annotations.Add(rectangleAnnotation);
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
    }
}
