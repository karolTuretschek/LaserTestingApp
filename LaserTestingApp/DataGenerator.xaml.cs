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

        double MinTemp, MaxTemp, Divergence, UnitTemp;
        int Duration;
        double FirstNumber;
        double tempRandom;
        private void GenerateDataButton_Click(object sender, RoutedEventArgs e)
        {

            MinTemp = double.Parse(AmbientTempMinGeneratorTextBox.Text.ToString());
            MaxTemp = double.Parse(AmbientTempMaxGeneratorTextBox.Text.ToString());
            Duration = int.Parse(TimeGeneratorTextBox.Text.ToString());
            UnitTemp = 15;  
            Random random = new Random();
            double Temp = 15;

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Licence
                using (ExcelPackage package = new ExcelPackage())
                {                   
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("DataSheet");
                    worksheet.Cells.Clear(); // Delete previous data
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
                                    //Debug.WriteLine($"{Math.Round(Temp, 1)} DOWN");
                                    worksheet.Cells[i + 1, 1].Value = i;
                                    worksheet.Cells[i + 1, 2].Value = Temp;
                                    worksheet.Cells[i + 1, 3].Value = Temp * 1.2;
                                }
                            }
                            else
                            {
                                Temp = Temp + (TempRandom * 0.1);
                                worksheet.Cells[i + 1, 1].Value = i;
                                worksheet.Cells[i + 1, 2].Value = Temp;
                                worksheet.Cells[i + 1, 3].Value = Temp * 0.9;
                            }
                        }
                        else
                        {
                            Temp = Temp - (TempRandom * 0.1);
                            worksheet.Cells[i + 1, 1].Value = i;
                            worksheet.Cells[i + 1, 2].Value = Temp;
                            worksheet.Cells[i + 1, 3].Value = Temp * 0.9;
                        }
                    }
                    string filePath = @"C:/Users/venqu/OneDrive/Dokumenty/Honours/Data/GeneratedData.xlsx";
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        package.SaveAs(fileStream);
                    }

                    Debug.WriteLine("Excel file created successfully.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while loading Xlsx data: {ex.Message}");
            }
        }
    }
}
