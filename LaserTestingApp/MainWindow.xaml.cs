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
        List<double> axie = new List<double>();
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
            public double Axie {  get; set; }
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
            SetSelectedAxisValue(ComboBoxY, ref yAxie);
            SetSelectedAxisValue(ComboBoxY2, ref yAxie2);
            SetSelectedAxisValue(ComboBoxX, ref xAxie);
            //SetAxies setAxiesY = new SetAxies(ComboBoxY, yAxie, laserTime, laserAmbientTemp,laserUnitTemp, laserDivergence, laserPowerOutput);
            //SetAxies setAxiesY2 = new SetAxies(ComboBoxY2, yAxie2, laserTime, laserAmbientTemp, laserUnitTemp, laserDivergence, laserPowerOutput);
            //SetAxies setAxiesX = new SetAxies(ComboBoxX, xAxie, laserTime, laserAmbientTemp, laserUnitTemp, laserDivergence, laserPowerOutput);

            DotSize = DotSizeSlider.Value;

            MainViewModel mainViewModel = new MainViewModel(yLabel, xLabel , xAxie, yAxie, yAxie2, RowsData, DotSize);//Assign model 

            // Populate plots
            ScatterChart.DataContext = mainViewModel; 
            LineChart.DataContext = mainViewModel; 
            FastChart.DataContext = mainViewModel; 
            
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

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv|Excel Files (*.xlsx)|*.xlsx|JSON Files (*.json)|*.json|All Files|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = Path.GetFileName(openFileDialog.FileName); // Find file name
                filePathTextBox.Text = fileName;
            }
        }

        private void filePathTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBoxY_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            yLabel = ComboBoxY.SelectedValue?.ToString();          
        }
    }
    
}
