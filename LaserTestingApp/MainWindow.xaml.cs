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

namespace LaserTestingApp
{

    public partial class MainWindow : Window
    {
        string filePath = "C:\\Users\\venqu\\OneDrive\\Dokumenty\\Honours\\DataLaserData.xlsx";

        List<double> laserTime = new List<double>();
        List<double> laserAmbientTemp = new List<double>();
        List<double> laserUnitTemp = new List<double>();
        List<double> laserDivergence = new List<double>();
        List<double> laserPowerOutput = new List<double>();
        string yLabel ="", xLabel = "";
        List<double> yAxie = new List<double>();
        List<double> yAxie2 = new List<double>();
        List<double> xAxie = new List<double>();
        double DotSize = 1; // Later to be provided be the user
        public static List<laserInfo> data = new List<laserInfo>();

        public MainWindow()
        {
            InitializeComponent();
        }
        public class laserInfo
        {

            public double Time { get; set; }
            public double AmbientTemp { get; set; }
            public double UnitTemp { get; set; }
            public double Divergence { get; set; }
            public double PowerOutput { get; set; }

        }

        private List<laserInfo> laserInfos = new List<laserInfo>();
        private void LoadDataButton_Click(object sender, RoutedEventArgs e) 
        {
            string filePath = "C:\\Users\\venqu\\OneDrive\\Dokumenty\\Honours\\Data\\LaserData.xlsx";
            LaserDataGrid.AutoGenerateColumns = false;
            
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
                                    double.Parse((worksheet.Cells[row, 2].Text)),
                                    double.Parse((worksheet.Cells[row, 3].Text)),
                                    double.Parse((worksheet.Cells[row, 4].Text)),
                                    double.Parse((worksheet.Cells[row, 5].Text))
                                    ); // Insert data from rows into DataGrid
                            laserTime.Add(double.Parse(worksheet.Cells[row, 1].Text));
                            laserAmbientTemp.Add(double.Parse(worksheet.Cells[row, 2].Text));
                            laserUnitTemp.Add(double.Parse(worksheet.Cells[row, 3].Text));
                            laserDivergence.Add(double.Parse(worksheet.Cells[row, 4].Text));
                            laserPowerOutput.Add(double.Parse(worksheet.Cells[row, 5].Text));


                        }
                    }
                    LaserDataGrid.ItemsSource = data;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while loading data: {ex.Message}");
            }

            int RowsData = laserTime.Count(); // Find number of rows
            if (ComboBoxY.SelectedValue?.ToString() != string.Empty)
            {
                if(ComboBoxY.SelectedValue.ToString().ToLower().Contains("time"))
                {
                    yAxie = laserTime;
                }else if(ComboBoxY.SelectedValue.ToString().ToLower().Contains("ambient"))
                {
                    yAxie = laserAmbientTemp;
                }
                else if (ComboBoxY.SelectedValue.ToString().ToLower().Contains("divergence"))
                {
                    yAxie = laserDivergence;
                }
                else if (ComboBoxY.SelectedValue.ToString().ToLower().Contains("power"))
                {
                    yAxie = laserPowerOutput;
                }
                else if (ComboBoxY.SelectedValue.ToString().ToLower().Contains("unit"))
                {
                    yAxie = laserUnitTemp;
                }
            }
            if (ComboBoxY2.SelectedValue?.ToString() != string.Empty)
            {
                if (ComboBoxY2.SelectedValue.ToString().ToLower().Contains("time"))
                {
                    yAxie2 = laserTime;
                }
                else if (ComboBoxY2.SelectedValue.ToString().ToLower().Contains("ambient"))
                {
                    yAxie2 = laserAmbientTemp;
                }
                else if (ComboBoxY2.SelectedValue.ToString().ToLower().Contains("divergence"))
                {
                    yAxie2 = laserDivergence;
                }
                else if (ComboBoxY2.SelectedValue.ToString().ToLower().Contains("power"))
                {
                    yAxie2 = laserPowerOutput;
                }
                else if (ComboBoxY2.SelectedValue.ToString().ToLower().Contains("unit"))
                {
                    yAxie2 = laserUnitTemp;
                }
            }
            if (ComboBoxX.SelectedValue?.ToString() != string.Empty)
            {
                if (ComboBoxX.SelectedValue.ToString().ToLower().Contains("time"))
                {
                    xAxie = laserTime;
                }
                else if (ComboBoxX.SelectedValue.ToString().ToLower().Contains("ambient"))
                {
                    xAxie = laserAmbientTemp;
                }
                else if (ComboBoxX.SelectedValue.ToString().ToLower().Contains("divergence"))
                {
                    xAxie = laserDivergence;
                }
                else if (ComboBoxX.SelectedValue.ToString().ToLower().Contains("power"))
                {
                    xAxie = laserPowerOutput;
                }
                else if (ComboBoxX.SelectedValue.ToString().ToLower().Contains("unit"))
                {
                    xAxie = laserUnitTemp;
                }
            }

            DotSize = DotSizeSlider.Value;

            MainViewModel mainViewModel = new MainViewModel(yLabel, xLabel , xAxie, yAxie, yAxie2, RowsData, DotSize);//Assign model 

            // Populate plots
            ScatterChart.DataContext = mainViewModel; 
            LineChart.DataContext = mainViewModel; 
            FastChart.DataContext = mainViewModel; 
            
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

        private void RadioTime_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBoxX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            xLabel = ComboBoxX.SelectedValue?.ToString();
        }

        private void DotSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ComboBoxX.SelectedValue?.ToString() != null || ComboBoxY.SelectedValue?.ToString() != null)
                LoadDataButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
        }

        private void ResetDataButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void ComboBoxY2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBoxY_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            yLabel = ComboBoxY.SelectedValue?.ToString();          
        }
    }
    
}
