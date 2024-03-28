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

namespace LaserTestingApp
{

    public partial class MainWindow : System.Windows.Window
    {
        //public string filePath = "";

        List<double> laserTime = new List<double>();
        List<double> laserAmbientTemp = new List<double>();
        List<double> laserUnitTemp = new List<double>();
        List<double> laserDivergence = new List<double>();
        List<double> laserPowerOutput = new List<double>();
        List<double> laserAmbientTempGap = new List<double>();
        List<double> laserUnitTempGap = new List<double>();
        List<double> laserDivergenceGap = new List<double>();
        List<double> laserPowerOutputGap = new List<double>();
        string yLabel = "", xLabel = "";
        List<double> axie = new List<double>();
        List<double> yAxie = new List<double>();
        List<double> yAxie2 = new List<double>();
        List<double> xAxie = new List<double>();
        List<double> distances = new List<double>();
        double DotSize = 3;
        public static List<laserInfo> data = new List<laserInfo>();
        public ScatterSeries saveSeries;
        bool LineChartYX, ScatterChartYX, FastChartYX;
        public string filePath { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            saveSeries = new ScatterSeries { MarkerType = MarkerType.Cross, MarkerSize = 100 };
            filePath = "test";
            IsEnabled = false;
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
        private void ParametersAssignButton_Click(object sender, RoutedEventArgs e)
        {
            LoadAllData();

            int RowsData = laserTime.Count(); // Find number of rows
            SetSelectedAxisValue(ComboBoxY, ref yAxie);
            SetSelectedAxisValue(ComboBoxY2, ref yAxie2);
            SetSelectedAxisValue(ComboBoxX, ref xAxie);

            MainViewModel mainViewModel = new MainViewModel();//Assign model 
            mainViewModel.ViewModelLine(yLabel, xLabel, xAxie, yAxie, yAxie2, RowsData, DotSize);
            mainViewModel.ViewModelScatter(yLabel, xLabel, xAxie, yAxie, yAxie2, RowsData, DotSize);
            mainViewModel.ViewModelFast(yLabel, xLabel, xAxie, yAxie, yAxie2, RowsData, DotSize);
            // Populate plots          
            LineChart.DataContext = mainViewModel;
            ScatterChart.DataContext = mainViewModel;
            FastChart.DataContext = mainViewModel;


        }
        private void LoadDataButton_Click(object sender, RoutedEventArgs e)
        {
            // Show previously disabled buttons
            ReverseAxis.IsEnabled = true;           
            ResetButton.IsEnabled = true;            
            DotSizeStack.IsEnabled = true;
            DotSizeStack.Opacity = 1.0;
            ButtonsStack.IsEnabled = true;
            ButtonsStack.Opacity = 1.0;
 

            LoadAllData();

            int RowsData = laserTime.Count()-1; // Find number of rows
            SetSelectedAxisValue(ComboBoxY, ref yAxie);
            SetSelectedAxisValue(ComboBoxY2, ref yAxie2);
            SetSelectedAxisValue(ComboBoxX, ref xAxie);

            MainViewModel mainViewModel = new MainViewModel();//Assign model 
            mainViewModel.ViewModelLine(yLabel, xLabel, xAxie, yAxie, yAxie2, RowsData, DotSize);
            mainViewModel.ViewModelScatter(yLabel, xLabel, xAxie, yAxie, yAxie2, RowsData, DotSize);
            mainViewModel.ViewModelFast(yLabel, xLabel, xAxie, yAxie, yAxie2, RowsData, DotSize);         
            // Populate plots          
            LineChart.DataContext = mainViewModel;
            ScatterChart.DataContext = mainViewModel;
            FastChart.DataContext = mainViewModel;
            // Assign flags
            LineChartYX = true;
            ScatterChartYX = true;
            FastChartYX = true;
            double distanceMaxTemp = 0;
            double distanceMax = mainViewModel.CalculateDistanceBetweenPoints(xAxie, yAxie2, RowsData, distanceMaxTemp);
            Debug.WriteLine($" Max distance found - > {distanceMax}");
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
                    if (data.Count == 0) // Only isert data if empty
                    {
                        for (int row = 2; row < rowCount; row++)
                        {
                            laserTime.Add(double.Parse(worksheet.Cells[row, 1].Text));
                            string cellText = worksheet.Cells[row, 2].Text;
                            double cellValue;
                            // If data missing calculate it based on one before and after
                            // Otherwise just take it from file
                            ProcessMissingAmbientTempValue(cellText, tempValue, laserAmbientTemp, worksheet, row, saveSeries);
                            //cellText = worksheet.Cells[row, 3].Text;                            
                            ProcessMissingUnitTempValue(cellText, tempValue, laserUnitTemp, worksheet, row);
                            cellText = worksheet.Cells[row, 4].Text;
                            //saveSeries.Points.Add(new DataPoint(row, 4)); // Add saved value's position
                            ProcessMissingDivergenceValue(cellText, tempValue, laserDivergence, worksheet, row);
                            cellText = worksheet.Cells[row, 5].Text;
                            //saveSeries.Points.Add(new DataPoint(row, 5)); // Add saved value's position
                            ProcessMissingPowerOutputValue(cellText, tempValue, laserPowerOutput, worksheet, row);
                            //laserUnitTemp.Add(double.Parse(worksheet.Cells[row, 3].Text));
                            //laserDivergence.Add(double.Parse(worksheet.Cells[row, 4].Text));
                            //laserPowerOutput.Add(double.Parse(worksheet.Cells[row, 5].Text));

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
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while loading Xlsx data: {ex.Message}");
            }
        }
        public void ProcessMissingAmbientTempValue(string cellText, double tempValue, List<double> myValue, ExcelWorksheet worksheet, int row, ScatterSeries mySeries)
        {
            double cellValue;

            if (double.TryParse(cellText, out cellValue))
            {
                if (cellValue.Equals(0.0)) // Finding if value is 0.0/empty
                {
                    tempValue = ((double.Parse(worksheet.Cells[row - 1, 2].Text) + double.Parse(worksheet.Cells[row + 1, 2].Text)) / 2);
                    double roundedTempValue = Math.Round(tempValue, 1);
                    myValue.Add(roundedTempValue);
                    mySeries.Points.Add(new ScatterPoint(row, 2, 100, 50)); // Add saved value's position
                    //scatterSeries.Points.Add(new ScatterPoint(x[i], y[i], DotSize, y2[i]));
                }
                else
                {
                    myValue.Add(cellValue);
                }
            }
        }
        public void ProcessMissingUnitTempValue(string cellText, double tempValue, List<double> myValue, ExcelWorksheet worksheet, int row)
        {
            double cellValue;

            if (double.TryParse(cellText, out cellValue))
            {
                if (cellValue.Equals(0.0)) // Finding if value is 0.0/empty
                {
                    tempValue = ((double.Parse(worksheet.Cells[row - 1, 3].Text) + double.Parse(worksheet.Cells[row + 1, 3].Text)) / 2);
                    double roundedTempValue = Math.Round(tempValue, 1);
                    myValue.Add(roundedTempValue);
                }
                else
                {
                    myValue.Add(cellValue);
                }
            }
        }
        public void ProcessMissingDivergenceValue(string cellText, double tempValue, List<double> myValue, ExcelWorksheet worksheet, int row)
        {
            double cellValue;

            if (double.TryParse(cellText, out cellValue))
            {
                if (cellValue.Equals(0.0)) // Finding if value is 0.0/empty
                {
                    tempValue = ((double.Parse(worksheet.Cells[row - 1, 4].Text) + double.Parse(worksheet.Cells[row + 1, 4].Text)) / 2);
                    double roundedTempValue = Math.Round(tempValue, 2);
                    myValue.Add(roundedTempValue);
                }
                else
                {
                    myValue.Add(cellValue);
                }
            }
        }
        public void ProcessMissingPowerOutputValue(string cellText, double tempValue, List<double> myValue, ExcelWorksheet worksheet, int row)
        {
            double cellValue;

            if (double.TryParse(cellText, out cellValue))
            {
                if (cellValue.Equals(0.0)) // Finding if value is 0.0/empty
                {
                    tempValue = ((double.Parse(worksheet.Cells[row - 1, 5].Text) + double.Parse(worksheet.Cells[row + 1, 5].Text)) / 2);
                    double roundedTempValue = Math.Round(tempValue, 2);
                    myValue.Add(roundedTempValue);
                }
                else
                {
                    myValue.Add(cellValue);
                }
            }
        }
        public void LoaddAllDataJson()
        {
            string jsonText = System.IO.File.ReadAllText(filePath);
            List<laserInfo> dataJson = JsonConvert.DeserializeObject<List<laserInfo>>(jsonText);
            foreach (var item in dataJson)
            {
                Debug.WriteLine(item.AmbientTemp);
                //DataTab.Items.Add(item.Time);
                data = LoadExcel((item.Time),(item.AmbientTemp),(item.UnitTemp),
                    (item.Divergence),(item.PowerOutput)); // Insert data from rows into DataGrid
                laserTime.Add(item.Time);
                laserAmbientTemp.Add(item.AmbientTemp);
                laserUnitTemp.Add(item.UnitTemp);
                laserDivergence.Add(item.Divergence);
                laserPowerOutput.Add(item.PowerOutput);
            }
            DataTab.ItemsSource = data;
        }
        private void SetSelectedAxisValue(System.Windows.Controls.ComboBox comboBox, ref List<double> axie)
        {
            if (comboBox.SelectedValue?.ToString() != string.Empty)
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

            MainViewModel mainViewModel = new MainViewModel();//Assign model
            int currentTabIndex = MainTab.SelectedIndex; // Find current tab open
            switch (currentTabIndex) // Based on open tab reverse axis
            {
                case 0:
                    // Tab index is 0
                    Debug.WriteLine("Tab index is 0, Cannot reverse axis");
                    break;
                case 1:
                    mainViewModel.ViewModelLineReset(yLabel, xLabel, xAxie, yAxie, yAxie2, RowsData, DotSize);
                    LineChart.DataContext = mainViewModel;
                    break;
                case 2:
                    mainViewModel.ViewModelScatterReset(yLabel, xLabel, xAxie, yAxie, yAxie2, RowsData, DotSize);
                    ScatterChart.DataContext = mainViewModel;
                    break;
                case 3:
                    mainViewModel.ViewModelFastReset(yLabel, xLabel, xAxie, yAxie, yAxie2, RowsData, DotSize);
                    FastChart.DataContext = mainViewModel;
                    break;
                default:
                    // Handle case when index is out of range
                    Debug.WriteLine($"Tab index is out of range. Current tab {currentTabIndex}");
                    break;
            }


        }

        private void ImportDataButton_Click(object sender, RoutedEventArgs e)
        {
            ImportWindow importWindow = new ImportWindow();
            importWindow.Show();
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
        bool yFlag, y2Flag, xFlag = false;
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
        private void ComboBoxX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!xFlag)
                nonDefaultComboBoxes++;
            xFlag = true;

            changeTextBlock();
        }
        private void ReverseAxisButton_Click(object sender, RoutedEventArgs e) {

            int RowsData = laserTime.Count(); // Find number of rows

            MainViewModel mainViewModel = new MainViewModel();//Assign model
            int currentTabIndex = MainTab.SelectedIndex; // Find current tab open
            switch (currentTabIndex) // Based on open tab reverse axis
            {
                case 0:
                    // Tab index is 0
                    Debug.WriteLine("Tab index is 0, Cannot reverse axis");
                    break;
                case 1:
                    if (!LineChartYX)
                    {
                        mainViewModel.ViewModelLine(yLabel, xLabel, xAxie, yAxie, yAxie2, RowsData, DotSize);
                        LineChart.DataContext = mainViewModel;
                        LineChartYX = true;
                    }
                    else
                    {
                        mainViewModel.ViewModelLineReverse(yLabel, xLabel, xAxie, yAxie, yAxie2, RowsData, DotSize);
                        LineChart.DataContext = mainViewModel;
                        LineChartYX = false;
                    }
                    break;
                case 2:
                    if (!ScatterChartYX)
                    {
                        mainViewModel.ViewModelScatter(yLabel, xLabel, xAxie, yAxie, yAxie2, RowsData, DotSize);
                        ScatterChart.DataContext = mainViewModel;
                        ScatterChartYX = true;
                    }
                    else
                    {
                        mainViewModel.ViewModelScatterReverse(yLabel, xLabel, xAxie, yAxie, yAxie2, RowsData, DotSize);
                        ScatterChart.DataContext = mainViewModel;
                        ScatterChartYX = false;
                    }
                    break;
                case 3:
                    if (!FastChartYX)
                    {
                        mainViewModel.ViewModelFast(yLabel, xLabel, xAxie, yAxie, yAxie2, RowsData, DotSize);
                        FastChart.DataContext = mainViewModel;
                        FastChartYX = true;
                    }
                    else
                    {
                        mainViewModel.ViewModelFastReverse(yLabel, xLabel, xAxie, yAxie, yAxie2, RowsData, DotSize);
                        FastChart.DataContext = mainViewModel;
                        FastChartYX = false;
                    }
                    break;
                default:
                    // Handle case when index is out of range
                    Debug.WriteLine($"Tab index is out of range. Current tab {currentTabIndex}");
                    break;
            }
        }
        private void DefaultDotButton_Click(object sender, RoutedEventArgs e)
        {
            DotSize = 3;
            LoadDataButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
        }
        private void SmallDotButton_Click(object sender, RoutedEventArgs e)
        {
            DotSize = 7;
            LoadDataButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
        }
        private void MediumDotButton_Click(object sender, RoutedEventArgs e)
        {
            DotSize = 12;
            LoadDataButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
        }
        private void LargeDotButton_Click(object sender, RoutedEventArgs e)
        {
            DotSize = 15;
            LoadDataButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
        }
        private void VeryLargeDotButton_Click(object sender, RoutedEventArgs e)
        {
            DotSize = 18;
            LoadDataButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
        }
    }
}
