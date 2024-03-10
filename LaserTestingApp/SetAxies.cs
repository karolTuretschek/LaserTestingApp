using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LaserTestingApp
{
    public class SetAxies:MainWindow
    {
        private void SetAxis(System.Windows.Controls.ComboBox comboBox, ref List<double> axie, 
                 ref List<double> laserTime, ref List<double> laserAmbientTemp, ref List<double> laserDivergence, 
                 ref List<double> laserPowerOutput, ref List<double> laserUnitTemp )
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
    }
}
