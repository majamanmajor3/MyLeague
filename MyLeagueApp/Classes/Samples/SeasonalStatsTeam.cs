using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeagueApp.Classes.Samples
{
    internal class SeasonalStatsTeam
    {
        public int SeasonId { get; set; }
        public int TeamId { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public double PPG { get; set; }
        public double APPG { get; set; }
        public int Season { get; set; }

        public SeasonalStatsTeam(int SeasonId, int TeamId, int Wins, int Losses, double PPG, double APPG, int Season)
        {
            this.SeasonId = SeasonId;
            this.TeamId = TeamId;
            this.Wins = Wins;
            this.Losses = Losses;
            this.PPG = PPG;
            this.APPG = APPG;
            this.Season = Season;
        }
    }
}
