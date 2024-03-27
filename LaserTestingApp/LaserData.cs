using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserTestingApp
{
    internal class LaserData
    {
        public double? Time { get; set; }
        public double? AmbientTemp { get; set; }
        public double? UnitTemp { get; set; }
        public double? Divergence { get; set; }
        public double? PowerOutput { get; set; }
    }
}
