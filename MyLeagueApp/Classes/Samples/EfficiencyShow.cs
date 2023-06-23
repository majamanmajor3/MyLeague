using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeagueApp.Classes.Samples
{
    class EfficiencyShow
    {
        public string Name { get; set; }
        public double Efficiency { get; set; }

        public EfficiencyShow(string Name, double Efficiency)
        {
            this.Name = Name;
            this.Efficiency = Efficiency;
        }
    }
}
