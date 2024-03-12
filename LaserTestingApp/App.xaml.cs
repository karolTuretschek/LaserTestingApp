using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LaserTestingApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Create instances of both windows
            //MainWindow mainWindow = new MainWindow();
            ImportWindow importWindow = new ImportWindow();

            // Show both windows
            //mainWindow.Show();
            importWindow.Show();
        }

    }
}
