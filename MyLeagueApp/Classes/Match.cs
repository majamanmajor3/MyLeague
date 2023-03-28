using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeagueApp.Classes
{
    internal class Match
    {
        public int Id { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string HomeLogo { get; set; }
        public string AwayLogo { get; set; }
        public string HomeCity { get; set; }
        public string AwayCity { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public string Date { get; set; }

        public Match(int Id, string HomeName, string AwayName, string HomeCity, string AwayCity, int HomeScore, int AwayScore, string HomeLogo, string AwayLogo, string Date)
        {
            this.Id = Id;
            this.HomeTeam = HomeName;
            this.AwayTeam = AwayName;
            this.HomeCity = HomeCity;
            this.AwayCity = AwayCity;
            this.HomeScore = HomeScore;
            this.AwayScore = AwayScore;
            this.HomeLogo = HomeLogo;
            this.AwayLogo = AwayLogo;
            this.Date = Date;
        }
    }
}
