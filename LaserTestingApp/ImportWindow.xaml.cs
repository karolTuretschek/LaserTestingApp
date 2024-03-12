using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.IO;
using System.Diagnostics;

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
            //openFileDialog.Filter = "CSV Files (*.csv)|*.csv|Excel Files (*.xlsx)|*.xlsx|JSON Files (*.json)|*.json|All Files|*.*";
            openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|JSON Files (*.json)|*.json|All Files|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = Path.GetFileName(openFileDialog.FileName); // get file name
                _mainWindow.filePathTextBox.Text = fileName;
                Debug.WriteLine(fileName);               
                _mainWindow.filePath = openFileDialog.FileName; // Assign path to string
                Debug.WriteLine("_mainWindow.filePath" + _mainWindow.filePath);
                _mainWindow.LoadAllData();
                _mainWindow.Show();
                ImportWindow importWindow = new ImportWindow();
                importWindow.Visibility = Visibility.Hidden;
                _mainWindow.LoadDataButton.IsEnabled = true;
            }
        }
    }
}
