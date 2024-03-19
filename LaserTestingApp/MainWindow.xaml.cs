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
        string yLabel = "", xLabel = "";
        List<double> axie = new List<double>();
        List<double> yAxie = new List<double>();
        List<double> yAxie2 = new List<double>();
        List<double> xAxie = new List<double>();
        double DotSize = 1; // Later to be provided be the user
        public static List<laserInfo> data = new List<laserInfo>();
        bool LineChartYX, ScatterChartYX, FastChartYX;
        public string filePath { get; set; }
        public MainWindow()
        {
            InitializeComponent();
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
        
        private void LoadDataButton_Click(object sender, RoutedEventArgs e)
        {
            LoadingTextBlock.Visibility = Visibility.Hidden;
            LoadAllData();

            int RowsData = laserTime.Count(); // Find number of rows
            SetSelectedAxisValue(ComboBoxY, ref yAxie);
            SetSelectedAxisValue(ComboBoxY2, ref yAxie2);
            SetSelectedAxisValue(ComboBoxX, ref xAxie);

            DotSize = DotSizeSlider.Value;

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

        }
        public void LoadAllData()
        {
            try
            {
                using (ExcelPackage package = new ExcelPackage(filePath))
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Licence
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;
                    if (data.Count == 0) // Only isert data if empty
                    {
                        for (int row = 2; row <= rowCount; row++)
                        {

                            data = LoadExcel(double.Parse(worksheet.Cells[row, 1].Text),
                                    double.Parse(worksheet.Cells[row, 2].Text),
                                    double.Parse(worksheet.Cells[row, 3].Text),
                                    double.Parse(worksheet.Cells[row, 4].Text),
                                    double.Parse(worksheet.Cells[row, 5].Text)
                                    ); // Insert data from rows into DataGrid
                            laserTime.Add(double.Parse(worksheet.Cells[row, 1].Text));
                            laserAmbientTemp.Add(double.Parse(worksheet.Cells[row, 2].Text));
                            laserUnitTemp.Add(double.Parse(worksheet.Cells[row, 3].Text));
                            laserDivergence.Add(double.Parse(worksheet.Cells[row, 4].Text));
                            laserPowerOutput.Add(double.Parse(worksheet.Cells[row, 5].Text));
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

        private void DotSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ComboBoxX.SelectedValue?.ToString() != null || ComboBoxY.SelectedValue?.ToString() != null)
                LoadDataButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
        }

        private void ResetDataButton_Click(object sender, RoutedEventArgs e)
        {
            int RowsData = laserTime.Count(); // Find number of rows
            DotSize = DotSizeSlider.Value;

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
                LoadingTextBlock.Text = "Press Create Graph";
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
            DotSize = DotSizeSlider.Value;

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
    }
}
