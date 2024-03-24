using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Plotly.NET;

namespace LaserTestingApp
{
    /// <summary>
    /// Interaction logic for ImportWindow.xaml
    /// </summary>
    public partial class ImportWindow : Window
    {
        public ImportWindow()
        {
            InitializeComponent();
            _mainWindow = new MainWindow();
        }
        private MainWindow _mainWindow;

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|JSON Files (*.json)|*.json|All Files|*.*";

            if (openFileDialog.ShowDialog() == true)
            {  
                string fileName = Path.GetFileName(openFileDialog.FileName); // get file name               
                switch (Path.GetExtension(fileName).ToLower())
                {
                    case ".xlsx":
                        Console.WriteLine("Xlsx file");
                        _mainWindow.filePathTextBox.Text = fileName;
                        Debug.WriteLine(fileName);
                        _mainWindow.filePath = openFileDialog.FileName; // Assign path to string
                        Debug.WriteLine("_mainWindow.filePath" + _mainWindow.filePath);
                        _mainWindow.Show();
                        _mainWindow.LoadAllData();
                        ImportWindow importWindow = new ImportWindow();
                        importWindow.Visibility = Visibility.Hidden;
                        _mainWindow.LoadDataButton.IsEnabled = false;
                        _mainWindow.IsEnabled = true;
                        break;
                    case ".json":
                        Debug.WriteLine("Json file");
                        _mainWindow.filePath = openFileDialog.FileName; // Assign path to string
                        Debug.WriteLine("_mainWindow.filePath" + _mainWindow.filePath);
                        _mainWindow.Show();
                        _mainWindow.LoaddAllDataJson();
                        _mainWindow.LoadDataButton.IsEnabled = false;
                        _mainWindow.IsEnabled = true;
                        break;
                    default:
                        Console.WriteLine("Unknown file format");
                        System.Windows.MessageBox.Show("Unknown file format", "Wrong file format");
                        break;
                }

            }
        }
    }
}
