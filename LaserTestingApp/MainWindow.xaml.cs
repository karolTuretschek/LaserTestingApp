using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using OfficeOpenXml;
using Plotly.NET.CSharp;
using OxyPlot;
using OxyPlot.Series;
using Plotly.NET;
using OxyPlot.Axes;

namespace LaserTestingApp
{

    public partial class MainWindow : Window
    {
        string filePath = "C:\\Users\\venqu\\OneDrive\\Dokumenty\\Honours\\DataLaserData.xlsx";

        public MainWindow()
        {
            this.MyModel = new PlotModel{ Title = "Example" };
            this.MyModel.Series.Add(new FunctionSeries(Math.Cos, 0, 30, 0.1, "sin(x)"));

            var model = new PlotModel { Title = "ScatterSeries" };
            var scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle };
            var r = new Random(314);
            for (int i = 0; i < 100; i++)
            {
                var x = r.NextDouble();
                var y = r.NextDouble();
                var size = r.Next(5, 15);
                var colorValue = r.Next(100, 1000);
                scatterSeries.Points.Add(new ScatterPoint(x, y, size, colorValue));
            }
            model.Series.Add(scatterSeries);
            model.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Jet(200) });

            this.MyModel2 = new PlotModel { Title = "Example2" };
            var scatterSeries2 = new ScatterSeries { MarkerType = MarkerType.Circle };
            for (int i = 0; i < 100; i++)
            {
                var x = r.NextDouble();
                var y = r.NextDouble();
                var size = r.Next(5, 15);
                var colorValue = r.Next(100, 1000);
                scatterSeries2.Points.Add(new ScatterPoint(x, y, size, colorValue));
            }
            MyModel2.Series.Add(scatterSeries2);
            this.MyModel2.Series.Add(new ScatterSeries { MarkerType = MarkerType.Circle });


        }

        public PlotModel MyModel { get; private set; }
        public PlotModel MyModel2 { get; private set; }

        public class laserInfo
        {

            public double Time { get; set; }
            public double AmbientTemp { get; set; }
            public double UnitTemp { get; set; }
            public double Divergence { get; set; }
            public double PowerOutput { get; set; }

        }
        private void showScatterChart()
        {

/*            var scatterChart = Chart.Point<double, double, string>(
             x: new double[] { 1, 3, 4, 2 },
             y: new double[] { 5, 10, 12, 19 }
            )
            .WithTraceInfo("Test Run", ShowLegend: true)
            .WithXAxisStyle<double, double, string>(Title: Plotly.NET.Title.init("xAxis"))
            .WithYAxisStyle<double, double, string>(Title: Plotly.NET.Title.init("yAxis"));

            ScatterChart.Content = scatterChart;

            //Display in html
            scatterChart.Show();*/
            

            

        }
        private List<laserInfo> laserInfos = new List<laserInfo>();

        private void LoadDataButton_Click(object sender, RoutedEventArgs e) 
        {
            showScatterChart();

            string filePath = "C:\\Users\\venqu\\OneDrive\\Dokumenty\\Honours\\Data\\LaserData.xlsx";
            LaserDataGrid.AutoGenerateColumns = false;

            try
            {
                
                using (ExcelPackage package = new ExcelPackage(filePath))
                {
                    //LaserDataGrid.ItemsSource = LoadExcel(myData);
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Licence
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;
                    Debug.WriteLine("Rows: {0}", rowCount);
                    var data = new List<laserInfo>();
                    for (int row = 2; row <= 21; row++)
                    {                       
                        
                        data = LoadExcel(double.Parse(worksheet.Cells[row , 1].Text),
                                double.Parse((worksheet.Cells[row, 2].Text)),
                                double.Parse((worksheet.Cells[row, 3].Text)),
                                double.Parse((worksheet.Cells[row, 4].Text)),
                                double.Parse((worksheet.Cells[row, 5].Text))
                                ); // Insert data from rows into DataGrid
                    }
                    LaserDataGrid.ItemsSource = data;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        private List<laserInfo> LoadExcel(double Time, double AmbientTemp, double UnitTemp, double Divergence, double PowerOutput)
        {
            

            laserInfos.Add(new laserInfo()
            {
                Time =  Time, AmbientTemp = AmbientTemp, UnitTemp = UnitTemp, Divergence = Divergence, PowerOutput = PowerOutput
            });

            return laserInfos;
        }

        //TabControl myTab = new TabControl();

        private void LaserDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
           
        }

        private void Frame_Navigated_1(object sender, NavigationEventArgs e)
        {

        }
    }
    
}
