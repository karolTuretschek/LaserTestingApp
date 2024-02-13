﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using OfficeOpenXml;
//using Plotly.NET;
using System.IO;
using Plotly.NET.CSharp;
using System.Linq;

namespace LaserTestingApp
{

    public partial class MainWindow : Window
    {
        

        string filePath = "C:\\Users\\venqu\\OneDrive\\Dokumenty\\Honours\\DataLaserData.xlsx";

        public MainWindow()
        {
            InitializeComponent();
            showScatterChart();
        }
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

            Chart.Point<double, double, string>(
             x: new double[] { 1, 2 },
             y: new double[] { 5, 10 }
            )
            .WithTraceInfo("Hello from C#", ShowLegend: true)
            .WithXAxisStyle<double, double, string>(Title: Plotly.NET.Title.init("xAxis"))
            .WithYAxisStyle<double, double, string>(Title: Plotly.NET.Title.init("yAxis"))
             .Show();


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

        TabControl myTab = new TabControl();

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
