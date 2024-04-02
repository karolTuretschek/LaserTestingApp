using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Microsoft.FSharp.Core.ByRefKinds;
using System.Data;
using System.Windows.Markup;
using Plotly.NET.TraceObjects;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection.Metadata;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO.Packaging;

namespace LaserTestingApp
{
    /// <summary>
    /// Interaction logic for DataGenerator.xaml
    /// </summary>
    public partial class DataGenerator : System.Windows.Window
    {
        public DataGenerator()
        {
            InitializeComponent();
        }

        double MinTemp, MaxTemp, Divergence, UnitTemp, PowerOutput;
        int Duration;
        double FirstNumber;
        double tempRandom;
        MainWindow myWindow = new MainWindow();
        string myRootPath = "test2";

        private void GenerateDataButton_Click(object sender, RoutedEventArgs e)
        {
            myRootPath = myWindow.FindDataFile();
            MinTemp = double.Parse(AmbientTempMinGeneratorTextBox.Text.ToString());
            MaxTemp = double.Parse(AmbientTempMaxGeneratorTextBox.Text.ToString());
            Duration = int.Parse(TimeGeneratorTextBox.Text.ToString());
            PowerOutput = double.Parse(PowerOutputGeneratorTextBox.Text.ToString());
            Divergence = double.Parse(DivergenceGeneratorTextBox.Text.ToString());
            UnitTemp = 15;
            Random random = new Random();
            double Temp = 15;
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Licence
                ExcelPackage package;
                FileInfo file = new FileInfo(myRootPath);
                if (file.Exists)
                {
                    // If the file exists, load it
                    package = new ExcelPackage(file);
                }
                else
                {
                    // If the file doesn't exist, create a new package
                    package = new ExcelPackage();
                }
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                {
                    worksheet = package.Workbook.Worksheets[0];
                }
                else
                {
                    // Clear previous data if the worksheet already exists
                    worksheet.Cells.Clear();
                }

                package.Save();
                // HEADERS
                worksheet.Cells[1, 1].Value = "Time";
                worksheet.Cells[1, 2].Value = "Ambient Temp";
                worksheet.Cells[1, 3].Value = "Unit Temp";
                worksheet.Cells[1, 4].Value = "Divergence";
                worksheet.Cells[1, 5].Value = "Power Output";
                for (int i = 1; i < Duration; i++)
                {
                    int TempRandom = random.Next(10);
                    if (TempRandom >= 5)
                    {
                        if (Temp >= MaxTemp * 1.1)
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                Temp = Temp - (TempRandom * 0.1);
                                worksheet.Cells[i + 1, 1].Value = i;
                                worksheet.Cells[i + 1, 2].Value = Temp;
                                worksheet.Cells[i + 1, 3].Value = Temp * 1.2;
                                worksheet.Cells[i + 1, 4].Value = Divergence;
                                worksheet.Cells[i + 1, 5].Value = PowerOutput;
                            }
                        }
                        else
                        {
                            Temp = Temp + (TempRandom * 0.1);
                            worksheet.Cells[i + 1, 1].Value = i;
                            worksheet.Cells[i + 1, 2].Value = Temp;
                            worksheet.Cells[i + 1, 3].Value = Temp * 0.9;
                            worksheet.Cells[i + 1, 4].Value = Divergence;
                            worksheet.Cells[i + 1, 5].Value = PowerOutput;
                        }
                    }
                    else
                    {
                        Temp = Temp - (TempRandom * 0.1);
                        worksheet.Cells[i + 1, 1].Value = i;
                        worksheet.Cells[i + 1, 2].Value = Temp;
                        worksheet.Cells[i + 1, 3].Value = Temp * 0.9;
                        worksheet.Cells[i + 1, 4].Value = Divergence;
                        worksheet.Cells[i + 1, 5].Value = PowerOutput;
                    }
                }
                package.Save();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while loading Xlsx data: {ex.Message}");
            }
        }
    }

}
