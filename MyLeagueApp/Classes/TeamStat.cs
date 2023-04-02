using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeagueApp.Classes
{
    internal class TeamStat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Logo { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int PPG { get; set; }
        public int APPG { get; set; }
        public double GD { get; set; }

        public TeamStat(int Id, string Name, string City, string Logo, int Wins, int Losses, int PPG, int APPG, double GD)
        {
            this.Id = Id;
            this.Name = Name;
            this.City = City;
            this.Logo = Logo;
            this.Wins = Wins;
            this.Losses = Losses;
            this.PPG = PPG;
            this.APPG = APPG;
            this.GD = GD;
        }

        public override string ToString()
        {
            return City + " " + Name;
        }
    }
}
