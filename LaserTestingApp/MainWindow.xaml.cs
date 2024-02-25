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




        public MainWindow()
        {
            //InitializeComponent();
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
                        laserTime.Add(double.Parse(worksheet.Cells[row, 1].Text));
                        laserAmbientTemp.Add(double.Parse(worksheet.Cells[row, 2].Text));
                        laserUnitTemp.Add(double.Parse(worksheet.Cells[row, 3].Text));
                        laserDivergence.Add(double.Parse(worksheet.Cells[row, 4].Text));
                        laserPowerOutput.Add(double.Parse(worksheet.Cells[row, 5].Text));


                    }
                    LaserDataGrid.ItemsSource = data;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred: {ex.Message}");
            }

            int RowsData = laserTime.Count(); // Find number of rows

            MainViewModel mainViewModel = new MainViewModel(laserTime, laserAmbientTemp, laserDivergence, RowsData);//Assign model 

            ScatterChart.DataContext = mainViewModel; // Populate plot
            
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
