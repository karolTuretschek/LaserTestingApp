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
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Linq;
using Microsoft.Win32;
using System.IO;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json.Linq;
using static Plotly.NET.StyleParam.BackOff;
using Plotly.NET.TraceObjects;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection.Metadata;
using System.Windows.Input;
using static LaserTestingApp.MainWindow;
using System.Text.Json.Nodes;
using static Plotly.NET.StyleParam.LinearAxisId;
using System.Xml;
using static Giraffe.ViewEngine.HtmlElements.XmlAttribute;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Windows.Media.Media3D;
using System.Windows.Documents;
using OxyPlot.Wpf;
using System.Windows.Media;

namespace LaserTestingApp
{
    public partial class MainWindow : System.Windows.Window
    {
        bool Xlsx;
        List<double> laserTime = new List<double>();
        List<double> laserAmbientTemp = new List<double>();
        List<double> laserUnitTemp = new List<double>();
        List<double> laserDivergence = new List<double>();
        List<double> laserPowerOutput = new List<double>();
        string yLabel = "", xLabel = "";
        List<double> axie = new List<double>();
        List<double> yAxie = new List<double>();
        List<double> yAxie2 = new List<double>();
        List<double> yAxie3 = new List<double>();
        List<double> yAxie4 = new List<double>();
        List<double> xAxie = new List<double>();
        List<double> distances = new List<double>();
        double UpperLimit, LowerLimit;
        string RootPath = "test";
        double DotSize = 3;
        public static List<laserInfo> data = new List<laserInfo>();
        public ScatterSeries saveSeries = new ScatterSeries { MarkerType = MarkerType.Cross, MarkerSize = 100 };        
        public ScatterSeries saveSeries2 = new ScatterSeries { MarkerType = MarkerType.Cross, MarkerSize = 100 };
        public ScatterSeries saveSeries3 = new ScatterSeries { MarkerType = MarkerType.Cross, MarkerSize = 100 };
        public ScatterSeries saveSeries4 = new ScatterSeries { MarkerType = MarkerType.Cross, MarkerSize = 100 };
        public ScatterSeries saveSeriesJson = new ScatterSeries { MarkerType = MarkerType.Cross, MarkerSize = 100 };
        public ScatterSeries saveSeries2Json = new ScatterSeries { MarkerType = MarkerType.Cross, MarkerSize = 100 };
        public ScatterSeries saveSeries3Json = new ScatterSeries { MarkerType = MarkerType.Cross, MarkerSize = 100 };
        public ScatterSeries saveSeries4Json = new ScatterSeries { MarkerType = MarkerType.Cross, MarkerSize = 100 };
        List<double> outliersY = new List<double>();
        List<double> outliersY2 = new List<double>();
        List<double> outliersY3 = new List<double>();
        List<double> outliersY4 = new List<double>();
        List<double> InterpolationYValues = new List<double>();
        bool RemovedOutliers = false, NotRemovedOutliers = false;
        bool LineChartYX, ScatterChartYX, FastChartYX;
        public Dictionary<double, double> gapsDictionaryY { get; set; } = new Dictionary<double, double>();
        public Dictionary<double, double> gapsDictionaryY2 { get; set; } = new Dictionary<double, double>();
        public Dictionary<double, double> gapsDictionaryY3 { get; set; } = new Dictionary<double, double>();
        public Dictionary<double, double> gapsDictionaryY4 { get; set; } = new Dictionary<double, double>();
        public Dictionary<double, double> gapsDictionaryYJson { get; set; } = new Dictionary<double, double>();
        public Dictionary<double, double> gapsDictionaryY2Json { get; set; } = new Dictionary<double, double>();
        public Dictionary<double, double> gapsDictionaryY3Json { get; set; } = new Dictionary<double, double>();
        public Dictionary<double, double> gapsDictionaryY4Json { get; set; } = new Dictionary<double, double>();
        public Dictionary<string, ItemData> unit1 { get; set; } = new Dictionary<string, ItemData>();
        public Dictionary<string, ItemData> unit2 { get; set; } = new Dictionary<string, ItemData>();
        public class ItemData
        {
            public double PowerOutput { get; set; }
            public double Divergence { get; set; }
            public double MinTemp { get; set; }
            public double MaxTemp { get; set; }
        }
        public string filePath { get; set; } = "test";
        ComboBoxItem newItem = new ComboBoxItem();
        public MainWindow()
        {
            InitializeComponent();
        }
        public class laserInfo
        {
            public double Axie { get; set; }
            public double Time { get; set; }
            public double AmbientTemp { get; set; }
            public double UnitTemp { get; set; }
            public double Divergence { get; set; }
            public double PowerOutput { get; set; }

        }
        private List<laserInfo> laserInfos = new List<laserInfo>();
        private void GenerateDataButton_Click(object sender, RoutedEventArgs e)
        {
            DataGenerator myGenerator = new DataGenerator();
            myGenerator.Show();
        }
        public string FindDataFile()
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|JSON Files (*.json)|*.json|All Files|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string fullPath = openFileDialog.FileName; // get file name
                return fullPath;  
            }
            return null;          
        }
        private void LoadDataButtonFast_Click(object sender, RoutedEventArgs e)
        {
            SetSelectedAxisValue(ComboBoxYFast, ref yAxie);
            SetSelectedAxisValue(ComboBoxY2Fast, ref yAxie2);
            SetSelectedAxisValue(ComboBoxY3Fast, ref yAxie3);
            SetSelectedAxisValue(ComboBoxY4Fast, ref yAxie4);
            SetSelectedAxisValue(ComboBoxXFast, ref xAxie);

            LoadAllData();

            int RowsData = laserTime.Count() - 3; // Find number of rows

            ViewModel viewModel = new ViewModel();//Assign model 

            viewModel.MyModel3a = new PlotModel { };
            viewModel.MyModel3b = new PlotModel { };
            viewModel.MyModel3c = new PlotModel { };
            viewModel.MyModel3d = new PlotModel { };
            // Assign Bottom title for subplots
            if (ComboBoxYFast.SelectedIndex != -1 && ComboBoxY.SelectedValue != "")
            {
                viewModel.MyModel3a.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = ComboBoxXFast.SelectedValue.ToString(), TitleFontWeight = OxyPlot.FontWeights.Bold, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });
                viewModel.MyModel3a.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = ComboBoxYFast.SelectedValue.ToString(), TitleFontWeight = OxyPlot.FontWeights.Bold, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });
                //viewModel.viewModelLine(xAxie, yAxie, RowsData);
            }

            if (ComboBoxY2Fast.SelectedIndex != -1 && ComboBoxY2.SelectedValue != "")
            {
                viewModel.MyModel3b.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = ComboBoxXFast.SelectedValue.ToString(), TitleFontWeight = OxyPlot.FontWeights.Bold, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });
                viewModel.MyModel3b.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = ComboBoxY2Fast.SelectedValue.ToString(), TitleFontWeight = OxyPlot.FontWeights.Bold, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });
                //viewModel.viewModelLineY2(xAxie, yAxie2, RowsData);
            }

            if (ComboBoxY3Fast.SelectedIndex != -1 && ComboBoxY3.SelectedValue != "")
            {
                viewModel.MyModel3c.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = ComboBoxY3Fast.SelectedValue.ToString(), TitleFontWeight = OxyPlot.FontWeights.Bold, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });
                viewModel.MyModel3c.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = ComboBoxXFast.SelectedValue.ToString(), TitleFontWeight = OxyPlot.FontWeights.Bold, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });
                //viewModel.viewModelLineY3(xAxie, yAxie3, RowsData);
            }


            if (ComboBoxY4Fast.SelectedIndex != -1 && ComboBoxY4.SelectedValue != "")
            {
                viewModel.MyModel3d.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = ComboBoxY4Fast.SelectedValue.ToString(), TitleFontWeight = OxyPlot.FontWeights.Bold, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });
                viewModel.MyModel3d.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = ComboBoxXFast.SelectedValue.ToString(), TitleFontWeight = OxyPlot.FontWeights.Bold, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });
                //viewModel.viewModelLineY4(xAxie, yAxie4, RowsData);
            }
            // View Fast Model only if 4 sub plots are chosen
            if (ComboBoxYFast.SelectedIndex != -1 && ComboBoxYFast.SelectedValue != ""
                && ComboBoxY2Fast.SelectedIndex != -1 && ComboBoxY2Fast.SelectedValue != "" &&
                ComboBoxY3Fast.SelectedIndex != -1 && ComboBoxY3Fast.SelectedValue != "" &&
                ComboBoxY4Fast.SelectedIndex != -1 && ComboBoxY4Fast.SelectedValue != "")
                viewModel.viewModelFast(xAxie, yAxie, yAxie2, yAxie3, yAxie4, RowsData);

            FastChart.DataContext = viewModel;
            // Assign flags
            LineChartYX = true;
            ScatterChartYX = true;
            FastChartYX = true;
        }
        private void LoadDataButton_Click(object sender, RoutedEventArgs e)
        {
            // Show previously disabled buttons          
            ResetButton.IsEnabled = true;            
            ButtonsStack.IsEnabled = true;
            ButtonsStack.Opacity = 1.0;
 
            LoadAllData();

            int RowsData = laserTime.Count()-3; // Find number of rows
            SetSelectedAxisValue(ComboBoxY, ref yAxie);
            SetSelectedAxisValue(ComboBoxY2, ref yAxie2);
            SetSelectedAxisValue(ComboBoxY3, ref yAxie3);
            SetSelectedAxisValue(ComboBoxY4, ref yAxie4);
            SetSelectedAxisValue(ComboBoxX, ref xAxie);

            ViewModel viewModel = new ViewModel();//Assign model 

            if (DisplayOutliersCheckBox.IsChecked == true)            // Data with remved Outliers
            {
                if(!RemovedOutliers)
                {
                    outliersY = viewModel.FindOutliers(yAxie, outliersY);
                    outliersY2 = viewModel.FindOutliers(yAxie2, outliersY2);
                    outliersY3 = viewModel.FindOutliers(yAxie3, outliersY3);
                    outliersY4 = viewModel.FindOutliers(yAxie4, outliersY4);
                }

                if (Xlsx) //Display Xlsx data
                {
                    viewModel.MyModel = new PlotModel { Title = "Line Plot Without Outliers" , EdgeRenderingMode = OxyPlot.Hardware};
                    viewModel.MyModel.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Jet(200)});
                    viewModel.MyModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, TitleFontWeight = OxyPlot.FontWeights.Bold, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });
                    viewModel.MyModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = ComboBoxX.SelectedValue.ToString(), TitleFontWeight = OxyPlot.FontWeights.Bold, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });

                    if (ComboBoxY.SelectedIndex != -1 && ComboBoxY.SelectedValue != "")
                        viewModel.viewModelLineYOutliers(xAxie, yAxie, RowsData, outliersY, InterpolationYValues);
                    if (ComboBoxY2.SelectedIndex != -1 && ComboBoxY2.SelectedValue != "")
                        viewModel.viewModelLineY2Outliers(xAxie, yAxie2, RowsData, outliersY2);
                    if (ComboBoxY3.SelectedIndex != -1 && ComboBoxY3.SelectedValue != "")
                        viewModel.viewModelLineY3Outliers(xAxie, yAxie3, RowsData, outliersY3);
                    if (ComboBoxY4.SelectedIndex != -1 && ComboBoxY4.SelectedValue != "")
                        viewModel.viewModelLineY4Outliers(xAxie, yAxie4, RowsData, outliersY4);

                    if(DisplaySplineCheckBox.IsChecked == true)// Show spline if ticked
                        viewModel.createInterpolation(xAxie, InterpolationYValues, RowsData);

                    LineChart.DataContext = viewModel; // Plot it up   
                }
                else
                {
                    viewModel.MyModel = new PlotModel { Title = "Line Plot Without Outliers" };
                    viewModel.MyModel.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Jet(200) });
                    viewModel.MyModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, TitleFontWeight = OxyPlot.FontWeights.Bold, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });
                    viewModel.MyModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = ComboBoxX.SelectedValue.ToString(), TitleFontWeight = OxyPlot.FontWeights.Bold, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });

                    if (ComboBoxY.SelectedIndex != -1 && ComboBoxY.SelectedValue != "")
                        viewModel.viewModelLineYOutliers(xAxie, yAxie, RowsData, outliersY, InterpolationYValues);
                    if (ComboBoxY2.SelectedIndex != -1 && ComboBoxY2.SelectedValue != "")
                        viewModel.viewModelLineY2Outliers(xAxie, yAxie2, RowsData, outliersY2);
                    if (ComboBoxY3.SelectedIndex != -1 && ComboBoxY3.SelectedValue != "")
                        viewModel.viewModelLineY3Outliers(xAxie, yAxie3, RowsData, outliersY3);
                    if (ComboBoxY4.SelectedIndex != -1 && ComboBoxY4.SelectedValue != "")
                        viewModel.viewModelLineY4Outliers(xAxie, yAxie4, RowsData, outliersY4);
                    LineChart.DataContext = viewModel; // Plot it up 
                }

                RemovedOutliers = true;
            } else // Normal data
            {
                viewModel.MyModel = new PlotModel { Title = "Line Plot" };
                viewModel.MyModel.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Jet(200) });
                viewModel.MyModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, TitleFontWeight = OxyPlot.FontWeights.Bold, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });
                viewModel.MyModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = ComboBoxX.SelectedValue.ToString(), TitleFontWeight = OxyPlot.FontWeights.Bold, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot });
                
                // Assign Bottom title for subplots
                if (ComboBoxY.SelectedIndex != -1 && ComboBoxY.SelectedValue != "")
                    viewModel.viewModelLine(xAxie, yAxie, RowsData);
                if (ComboBoxY2.SelectedIndex != -1 && ComboBoxY2.SelectedValue != "")
                    viewModel.viewModelLineY2(xAxie, yAxie2, RowsData);
                if (ComboBoxY3.SelectedIndex != -1 && ComboBoxY3.SelectedValue != "")
                    viewModel.viewModelLineY3(xAxie, yAxie3, RowsData);
                if (ComboBoxY4.SelectedIndex != -1 && ComboBoxY4.SelectedValue != "")
                    viewModel.viewModelLineY4(xAxie, yAxie4, RowsData);
                LineChart.DataContext = viewModel; // Plot it up    
                NotRemovedOutliers = true;
            }

            UpperLimit = double.Parse(UnitMaxOperatingTemperatureTextBox.Text.ToString()); // Upper limit display
            if (DisplayUpperLimitCheckBox.IsChecked == true)
                viewModel.createUpperLimit(RowsData, UpperLimit);

            LowerLimit = double.Parse(UnitMinOperatingTemperatureTextBox.Text.ToString()); // Lower limit display
            if (DisplayLowerLimitCheckBox.IsChecked == true)
                viewModel.createLowerLimit(RowsData, LowerLimit);

            if (DisplayGapMarkerCheckBox.IsChecked == true) // Display where gaps in data was present
                // check if Xlsx or Json file is being used
                if (Xlsx) //Display Xlsx data
                {
                    viewModel.viewModelLineGaps(xAxie, yAxie, RowsData, gapsDictionaryY);
                    viewModel.viewModelLineY2Gaps(xAxie, yAxie2, RowsData, gapsDictionaryY2);
                    viewModel.viewModelLineY3Gaps(xAxie, yAxie3, RowsData, gapsDictionaryY3);
                    viewModel.viewModelLineY4Gaps(xAxie, yAxie4, RowsData, gapsDictionaryY4);
                }
                else //Display Json data
                {
                    viewModel.viewModelLineGaps(xAxie, yAxie, laserTime.Count(), gapsDictionaryYJson);
                    viewModel.viewModelLineY2Gaps(xAxie, yAxie2, laserTime.Count(), gapsDictionaryY2Json);
                    viewModel.viewModelLineY3Gaps(xAxie, yAxie3, laserTime.Count(), gapsDictionaryY3Json);
                    viewModel.viewModelLineY4Gaps(xAxie, yAxie4, laserTime.Count(), gapsDictionaryY4Json);
                }  
            // Assign flags
            LineChartYX = true;
            //LineChart.DataContext = viewModel;
        }
        private void MainTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FastChart.IsSelected)
            {
                FastStackPanel.Visibility = Visibility.Visible;
                LineStackPanel.Visibility = Visibility.Hidden;
            }
            else if (LineChart.IsSelected)
            {
                FastStackPanel.Visibility = Visibility.Hidden;
                LineStackPanel.Visibility = Visibility.Visible;
            }
        }
        public void LoadAllData()
        {
            try
            {
                using (ExcelPackage package = new ExcelPackage(filePath))
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Licence
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    double tempValue = 0.0;
                    int rowCount = worksheet.Dimension.Rows;
                    Debug.WriteLine($"Rows: { worksheet.Dimension.Rows }");
                    if (data.Count == 0) // Only isert data if empty
                    {
                        for (int row = 2; row <= rowCount; row++)
                        {
                            laserTime.Add(double.Parse(worksheet.Cells[row, 1].Text));
                            // If data missing calculate it based on one before and after
                            // Otherwise just take it from file
                            string cellText = worksheet.Cells[row, 2].Text;
                            ProcessMissingAmbientTempValue(cellText, tempValue, laserAmbientTemp, worksheet, row, saveSeries);
                            cellText = worksheet.Cells[row, 3].Text; 
                            ProcessMissingUnitTempValue(cellText, tempValue, laserUnitTemp, worksheet, row, saveSeries2);
                            cellText = worksheet.Cells[row, 4].Text;
                            ProcessMissingDivergenceValue(cellText, tempValue, laserDivergence, worksheet, row, saveSeries3);
                            cellText = worksheet.Cells[row, 5].Text;
                            ProcessMissingPowerOutputValue(cellText, tempValue, laserPowerOutput, worksheet, row, saveSeries4);

                            data = LoadExcel(laserTime.Last(),
                            laserAmbientTemp.Last(),
                            laserUnitTemp.Last(),
                            laserDivergence.Last(),
                            laserPowerOutput.Last()
                                ); // Insert data from rows into DataGrid
                        }
                    }
                    DataTab.ItemsSource = data;
                }
                Xlsx = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while loading Xlsx data: {ex.Message}");
            }
        }
        public void ProcessMissingAmbientTempValue(string cellText, double tempValue, List<double> myValue, ExcelWorksheet worksheet, int row, ScatterSeries mySeries)
        {
            ViewModel mainView = new ViewModel();
            double cellValue;

            if (double.TryParse(cellText, out cellValue))
            {
                if (cellValue.Equals(0.0) || cellValue.Equals(double.NaN) || cellValue.ToString().Length == 0) // Finding if value is 0.0/empty
                {
                    tempValue = ((double.Parse(worksheet.Cells[row - 1, 2].Text) + double.Parse(worksheet.Cells[row + 1, 2].Text)) / 2);
                    double roundedTempValue = Math.Round(tempValue, 1);
                    myValue.Add(roundedTempValue);
                    mySeries.Points.Add(new ScatterPoint(row, 2, 100, 50)); // Add saved value's position
                    gapsDictionaryY.Add(row, 2);
                }
                else
                {
                    myValue.Add(cellValue);
                }
            }
            else // Not a number
            {
                tempValue = ((double.Parse(worksheet.Cells[row - 1, 2].Text) + double.Parse(worksheet.Cells[row + 1, 2].Text)) / 2);
                double roundedTempValue = Math.Round(tempValue, 1);
                myValue.Add(roundedTempValue);
                mySeries.Points.Add(new ScatterPoint(row, 2, 100, 50)); // Add saved value's position
                gapsDictionaryY.Add(row, 2);
            }
        }
        public void ProcessMissingUnitTempValue(string cellText, double tempValue, List<double> myValue, ExcelWorksheet worksheet, int row, ScatterSeries mySeries2)
        {
            double cellValue;

            if (double.TryParse(cellText, out cellValue))
            {
                if (cellValue.Equals(0.0) || cellValue.Equals(double.NaN) || cellValue.ToString().Length == 0) // Finding if value is 0.0/empty
                {
                    tempValue = ((double.Parse(worksheet.Cells[row - 1, 3].Text) + double.Parse(worksheet.Cells[row + 1, 3].Text)) / 2);
                    double roundedTempValue = Math.Round(tempValue, 1);
                    myValue.Add(roundedTempValue);
                    mySeries2.Points.Add(new ScatterPoint(row, 3, 100, 50)); // Add saved value's position
                    gapsDictionaryY2.Add(row, 3);
                }
                else
                {
                    myValue.Add(cellValue);
                }
            }
            else // Not a number
            {
                tempValue = ((double.Parse(worksheet.Cells[row - 1, 3].Text) + double.Parse(worksheet.Cells[row + 1, 3].Text)) / 2);
                double roundedTempValue = Math.Round(tempValue, 1);
                myValue.Add(roundedTempValue);
                mySeries2.Points.Add(new ScatterPoint(row, 3, 100, 50)); // Add saved value's position
                gapsDictionaryY2.Add(row, 3);
            }

        }
        public void ProcessMissingDivergenceValue(string cellText, double tempValue, List<double> myValue, ExcelWorksheet worksheet, int row, ScatterSeries mySeries3)
        {
            double cellValue;

            if (double.TryParse(cellText, out cellValue))
            {
                if (cellValue.Equals(0.0) || cellValue.Equals(double.NaN) || cellValue.ToString().Length == 0) // Finding if value is 0.0/empty
                {
                    tempValue = ((double.Parse(worksheet.Cells[row - 1, 4].Text) + double.Parse(worksheet.Cells[row + 1, 4].Text)) / 2);
                    double roundedTempValue = Math.Round(tempValue, 1);
                    myValue.Add(roundedTempValue);
                    mySeries3.Points.Add(new ScatterPoint(row, 4, 100, 50)); // Add saved value's position
                    gapsDictionaryY3.Add(row, 4);
                }
                else
                {
                    myValue.Add(cellValue);
                }
            }
            else // Not a number
            {
                tempValue = ((double.Parse(worksheet.Cells[row - 1, 4].Text) + double.Parse(worksheet.Cells[row + 1, 4].Text)) / 2);
                double roundedTempValue = Math.Round(tempValue, 1);
                myValue.Add(roundedTempValue);
                mySeries3.Points.Add(new ScatterPoint(row, 4, 100, 50)); // Add saved value's position
                gapsDictionaryY3.Add(row, 4);
            }
        }
        public void ProcessMissingPowerOutputValue(string cellText, double tempValue, List<double> myValue, ExcelWorksheet worksheet, int row, ScatterSeries mySeries4)
        {
            double cellValue;

            if (double.TryParse(cellText, out cellValue))
            {
                if (cellValue.Equals(0.0) || cellValue.Equals(double.NaN) || cellValue.ToString().Length == 0) // Finding if value is 0.0/empty
                {
                    tempValue = ((double.Parse(worksheet.Cells[row - 1, 5].Text) + double.Parse(worksheet.Cells[row + 1, 5].Text)) / 2);
                    double roundedTempValue = Math.Round(tempValue, 1);
                    myValue.Add(roundedTempValue);
                    mySeries4.Points.Add(new ScatterPoint(row, 5, 100, 50)); // Add saved value's position
                    gapsDictionaryY4.Add(row, 5);
                }
                else
                {
                    myValue.Add(cellValue);
                }
            }
            else // Not a number
            {
                tempValue = ((double.Parse(worksheet.Cells[row - 1, 5].Text) + double.Parse(worksheet.Cells[row + 1, 5].Text)) / 2);
                double roundedTempValue = Math.Round(tempValue, 1);
                myValue.Add(roundedTempValue);
                mySeries4.Points.Add(new ScatterPoint(row, 5, 100, 50)); // Add saved value's position
                gapsDictionaryY4.Add(row, 5);
            }
        }
        public void ProcessMissingAmbientTempValueJson(string cellText, double tempValue, List<double> myValue, List<laserInfo> jsonData, int currentIndex, ScatterSeries mySeries, int counter)
        {
            double cellValue;

            if (double.TryParse(cellText, out cellValue))
            {
                if (cellValue.Equals(0.0) || double.IsNaN(cellValue) || cellText.Length == 0) // Check if value is 0.0, NaN, or empty
                {                    
                    tempValue = (jsonData[counter - 2].AmbientTemp + jsonData[counter].AmbientTemp) / 2.0 ;
                    double roundedTempValue = Math.Round(tempValue, 1);
                    myValue.Add(roundedTempValue);
                    mySeries.Points.Add(new ScatterPoint(counter, roundedTempValue, 100, 50)); // Add saved value's position
                    Debug.WriteLine("Missing value found! at counter: " + counter);
                    gapsDictionaryYJson.Add(counter, roundedTempValue);
                }
                else
                {
                    myValue.Add(cellValue);
                }
            }
            else // Not a number
            {
                tempValue = (jsonData[counter - 2].AmbientTemp + jsonData[counter].AmbientTemp) / 2.0;
                double roundedTempValue = Math.Round(tempValue, 1);
                myValue.Add(roundedTempValue);
                mySeries.Points.Add(new ScatterPoint(counter, roundedTempValue, 100, 50));
                gapsDictionaryYJson.Add(counter, roundedTempValue);
            }
        }
        public void ProcessMissingUnitTempValueJson(string cellText, double tempValue, List<double> myValue, List<laserInfo> jsonData, int currentIndex, ScatterSeries mySeries, int counter)
        {
            double cellValue;

            if (double.TryParse(cellText, out cellValue))
            {
                if (cellValue.Equals(0.0) || double.IsNaN(cellValue) || cellText.Length == 0) // Check if value is 0.0, NaN, or empty
                {
                    tempValue = (jsonData[counter - 2].UnitTemp + jsonData[counter].UnitTemp) / 2.0;
                    double roundedTempValue = Math.Round(tempValue, 1);
                    myValue.Add(roundedTempValue);
                    mySeries.Points.Add(new ScatterPoint(counter, roundedTempValue, 100, 50)); // Add saved value's position
                    Debug.WriteLine("Missing value found! at counter: " + counter);
                    gapsDictionaryY2Json.Add(counter, roundedTempValue);
                }
                else
                {
                    myValue.Add(cellValue);
                }
            }
            else // Not a number
            {
                tempValue = (jsonData[counter - 2].UnitTemp + jsonData[counter].UnitTemp) / 2.0;
                double roundedTempValue = Math.Round(tempValue, 1);
                myValue.Add(roundedTempValue);
                mySeries.Points.Add(new ScatterPoint(counter, roundedTempValue, 100, 50));
                gapsDictionaryY2Json.Add(counter, roundedTempValue);
            }
        }      
        public void ProcessMissingDivergenceValueJson(string cellText, double tempValue, List<double> myValue, List<laserInfo> jsonData, int currentIndex, ScatterSeries mySeries, int counter)
        {
            double cellValue;

            if (double.TryParse(cellText, out cellValue))
            {
                if (cellValue.Equals(0.0) || double.IsNaN(cellValue) || cellText.Length == 0) // Check if value is 0.0, NaN, or empty
                {
                    tempValue = (jsonData[counter - 2].Divergence + jsonData[counter].Divergence) / 2.0;
                    double roundedTempValue = Math.Round(tempValue, 1);
                    myValue.Add(roundedTempValue);
                    mySeries.Points.Add(new ScatterPoint(counter, roundedTempValue, 100, 50)); // Add saved value's position
                    Debug.WriteLine("Missing value found! at counter: " + counter);
                    gapsDictionaryY3Json.Add(counter, roundedTempValue);
                }
                else
                {
                    myValue.Add(cellValue);
                }
            }
            else // Not a number
            {
                tempValue = (jsonData[counter - 2].Divergence + jsonData[counter].Divergence) / 2.0;
                double roundedTempValue = Math.Round(tempValue, 1);
                myValue.Add(roundedTempValue);
                mySeries.Points.Add(new ScatterPoint(counter, roundedTempValue, 100, 50));
                gapsDictionaryY3Json.Add(counter, roundedTempValue);
            }
        }
        public void ProcessMissingPowerOutputValueJson(string cellText, double tempValue, List<double> myValue, List<laserInfo> jsonData, int currentIndex, ScatterSeries mySeries, int counter)
        {
            double cellValue;

            if (double.TryParse(cellText, out cellValue))
            {
                if (cellValue.Equals(0.0) || double.IsNaN(cellValue) || cellText.Length == 0) // Check if value is 0.0, NaN, or empty
                {
                    tempValue = (jsonData[counter - 2].PowerOutput + jsonData[counter].PowerOutput) / 2.0;
                    double roundedTempValue = Math.Round(tempValue, 1);
                    myValue.Add(roundedTempValue);
                    mySeries.Points.Add(new ScatterPoint(counter, roundedTempValue, 100, 50)); // Add saved value's position
                    Debug.WriteLine("Missing value found! at counter: " + counter);
                    gapsDictionaryY4Json.Add(counter, roundedTempValue);
                }
                else
                {
                    myValue.Add(cellValue);
                }
            }
            else // Not a number
            {
                tempValue = (jsonData[counter - 2].PowerOutput + jsonData[counter].PowerOutput) / 2.0;
                double roundedTempValue = Math.Round(tempValue, 1);
                myValue.Add(roundedTempValue);
                mySeries.Points.Add(new ScatterPoint(counter, roundedTempValue, 100, 50));
                gapsDictionaryY4Json.Add(counter, roundedTempValue);
            }
        }
        public void LoaddAllDataJson()
        {
            string jsonText = System.IO.File.ReadAllText(filePath);
            List<laserInfo> dataJson = JsonConvert.DeserializeObject<List<laserInfo>>(jsonText);
            int counter = 0;

            foreach (var item in dataJson)
            {
                counter++;
                laserTime.Add(item.Time);
                // Process missing values (if applicable)
                double tempValue = 0.0;
                string cellText = item.AmbientTemp.ToString();
                ProcessMissingAmbientTempValueJson(cellText, tempValue, laserAmbientTemp, dataJson, dataJson.Count(), saveSeriesJson, counter);
                cellText = item.UnitTemp.ToString();
                ProcessMissingUnitTempValueJson(cellText, tempValue, laserUnitTemp, dataJson, dataJson.Count(), saveSeries2Json, counter);
                cellText = item.Divergence.ToString();
                ProcessMissingDivergenceValueJson(cellText, tempValue, laserDivergence, dataJson, dataJson.Count(), saveSeries3Json, counter);
                cellText = item.PowerOutput.ToString();
                ProcessMissingPowerOutputValueJson(cellText, tempValue, laserPowerOutput, dataJson, dataJson.Count(), saveSeries4Json, counter);

                data = LoadExcel(laserTime.Last(),
                    laserAmbientTemp.Last(),
                    laserUnitTemp.Last(),
                    laserDivergence.Last(),
                    laserPowerOutput.Last());
            }
            DataTab.ItemsSource = data;
            Xlsx = false;
        }
        private void SetSelectedAxisValue(System.Windows.Controls.ComboBox comboBox, ref List<double> axie)
        {
            if (comboBox.SelectedValue?.ToString() != string.Empty && comboBox.SelectedIndex != -1)
            {
                string selectedValue = comboBox.SelectedValue.ToString().ToLower();

                if (selectedValue.Contains("time"))
                {
                    axie = laserTime;
                }
                else if (selectedValue.Contains("ambient"))
                {
                    axie = laserAmbientTemp;
                }
                else if (selectedValue.Contains("divergence"))
                {
                    axie = laserDivergence;
                }
                else if (selectedValue.Contains("power"))
                {
                    axie = laserPowerOutput;
                }
                else if (selectedValue.Contains("unit"))
                {
                    axie = laserUnitTemp;
                }
            }
        }
        private List<laserInfo> LoadExcel(double Time, double AmbientTemp, double UnitTemp, double Divergence, double PowerOutput)
        {


            laserInfos.Add(new laserInfo()
            {
                Time = Time,
                AmbientTemp = AmbientTemp,
                UnitTemp = UnitTemp,
                Divergence = Divergence,
                PowerOutput = PowerOutput
            });

            return laserInfos;
        }
        private void LaserDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {

        }
        private void Frame_Navigated_1(object sender, NavigationEventArgs e)
        {

        }
        private void RadioTime_Checked(object setnder, RoutedEventArgs e)
        {

        }
        private void ResetDataButton_Click(object sender, RoutedEventArgs e)
        {
            int RowsData = laserTime.Count(); // Find number of rows

            ViewModel viewModel = new ViewModel();//Assign model
            int currentTabIndex = MainTab.SelectedIndex; // Find current tab open
            switch (currentTabIndex) // Based on open tab reverse axis
            {
                case 0:
                    // Tab index is 0
                    Debug.WriteLine("Tab index is 0, Cannot reverse axis");
                    break;
                case 1:
                    viewModel.viewModelLineReset(yLabel, xLabel, xAxie, yAxie, yAxie2, RowsData, DotSize);
                    LineChart.DataContext = viewModel;
                    break;
                case 2:
                    viewModel.viewModelFastReset(yLabel, xLabel, xAxie, yAxie, yAxie2, RowsData, DotSize);
                    FastChart.DataContext = viewModel;
                    break;
                default:
                    // Handle case when index is out of range
                    Debug.WriteLine($"Tab index is out of range. Current tab {currentTabIndex}");
                    break;
            }


        }       
        private void ImportDataButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|JSON Files (*.json)|*.json|All Files|*.*";
            
            if (openFileDialog.ShowDialog() == true)
            {
                MainWindow myWindow = new MainWindow();
                string fileName = Path.GetFileName(openFileDialog.FileName); // get file name               
                switch (Path.GetExtension(fileName).ToLower())
                {
                    
                    case ".xlsx":
                        Console.WriteLine("Xlsx file");
                        myWindow.filePathTextBox.Text = fileName;
                        Debug.WriteLine(fileName);
                        myWindow.filePath = openFileDialog.FileName; // Assign path to string
                        Debug.WriteLine("myWindow.filePath" + myWindow.filePath);                        
                        myWindow.Show();
                        this.Close();
                        myWindow.LoadAllData();
                        myWindow.LoadDataButton.IsEnabled = false;
                        myWindow.IsEnabled = true;
                        
                        break;
                    case ".json":
                        Debug.WriteLine("Json file");
                        myWindow.filePath = openFileDialog.FileName; // Assign path to string
                        Debug.WriteLine("myWindow.filePath" + myWindow.filePath);
                        myWindow.Show();
                        this.Close();
                        myWindow.LoaddAllDataJson();
                        myWindow.LoadDataButton.IsEnabled = false;
                        myWindow.IsEnabled = true;
                        break;
                    default:
                        Console.WriteLine("Unknown file format");
                        System.Windows.MessageBox.Show("Unknown file format", "Wrong file format");
                        break;
                }

            }
        }
        private void filePathTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        int nonDefaultComboBoxes = 0;
        public void changeTextBlock()
        {
            if (nonDefaultComboBoxes >= 3)
            {
                LoadDataButton.IsEnabled = true;
            }

        }
        bool yFlag, y2Flag, y3Flag, y4Flag, xFlag = false;
        private void ComboBoxY_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            yLabel = ComboBoxY.SelectedValue?.ToString();

            if (!yFlag)
                nonDefaultComboBoxes++;
            yFlag = true;

            changeTextBlock();
        }
        private void ComboBoxY2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (!y2Flag)
                nonDefaultComboBoxes++;
            y2Flag = true;

            changeTextBlock();
        }
        private void ComboBoxY3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (!y3Flag)
                nonDefaultComboBoxes++;
            y3Flag = true;

            changeTextBlock();
        }
        private void ComboBoxY4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (!y4Flag)
                nonDefaultComboBoxes++;
            y4Flag = true;

            changeTextBlock();
        }
        private void ComboBoxX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!xFlag)
                nonDefaultComboBoxes++;
            xFlag = true;

            changeTextBlock();
        }
        private void LaserComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (LaserCombBox.SelectedValue.ToString().Contains("1"))
            {
                UnitNameTextBox.Text = "Starlight R2";
                UnitPowerOutputTextBox.Text = "1mW";
                UnitMinOperatingTemperatureTextBox.Text = "-10";
                UnitMaxOperatingTemperatureTextBox.Text = "40";
                UnitDivergenceTextBox.Text = "700";

            }
            if (LaserCombBox.SelectedValue.ToString().Contains("2"))
            {
                UnitNameTextBox.Text = "163 LTD";
                UnitPowerOutputTextBox.Text = "70mJ";
                UnitMinOperatingTemperatureTextBox.Text = "-30";
                UnitMaxOperatingTemperatureTextBox.Text = "50";
                UnitDivergenceTextBox.Text = "300";
            }
        }
        private void UnitDivergenceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
