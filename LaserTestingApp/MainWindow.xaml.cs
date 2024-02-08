using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using OfficeOpenXml;

namespace LaserTestingApp
{

    public partial class MainWindow : Window
    {
        string filePath = "C:\\Users\\venqu\\OneDrive\\Dokumenty\\Honours\\DataLaserData.xlsx";

        // ---------------------------------> Needing sorted into reading from the excel sheet

        List<laserInfo> laserData = new List<laserInfo>{
                new laserInfo { Time =  1, AmbientTemp = 0.00, UnitTemp = 0.00, Divergence = 1.00, PowerOutput = 5.00},
            };


        public MainWindow()
        {
            InitializeComponent();
        }
        public class laserInfo
        {

            public int Time { get; set; }
            public double AmbientTemp { get; set; }
            public double UnitTemp { get; set; }
            public double Divergence { get; set; }
            public double PowerOutput { get; set; }

        }

        private void LoadDataButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Start of things");
            string filePath = "C:\\Users\\venqu\\OneDrive\\Dokumenty\\Honours\\Data\\LaserData.xlsx";
            LaserDataGrid.AutoGenerateColumns = false;

            try
            {

                using (ExcelPackage package = new ExcelPackage(filePath))
                {
                    LaserDataGrid.ItemsSource = LoadExcel();
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Licence
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    List<laserInfo> laserData = new List<laserInfo>();

                    int rowCount = worksheet.Dimension.Rows;
                    int columnCount = worksheet.Dimension.Columns;
                    Debug.WriteLine("Rows: {0}", rowCount);
                    for (int row = 1; row <= rowCount; row++)
                    {
                        for (int col = 1; col <= columnCount; col++)
                        {
                            var cellValue = worksheet.Cells[row, col].Text;
                            Debug.WriteLine(cellValue + "\t");
                            //Debug.WriteLine("");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private List<laserInfo> LoadExcel()
        {
            List<laserInfo> laserInfos = new List<laserInfo>();

            laserInfos.Add(new laserInfo()
            {
                Time =  1, AmbientTemp = 0.00, UnitTemp = 0.00, Divergence = 1.00, PowerOutput = 5.00
            });

            return laserInfos;
        }
   
    }
    
}
