using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeagueApp.Classes.Samples
{
    internal class SeasonalStats
    {

        public int SeasonId { get; set; }
        public int PlayerId { get; set; }
        public double Points { get; set; }
        public double Rebounds { get; set; }
        public double Assists { get; set; }
        public double Steals { get; set; }
        public double Blocks { get; set; }
        public double FGMade { get; set; }
        public double FGAttempted { get; set; }
        public double ThreesMade { get; set; }
        public double ThreesAttempted { get; set; }
        public int Season { get; set; }

        public SeasonalStats(int StatId, int PlayerId, double Points, double Rebounds, double Assists, double Steals, double Blocks, double FGMade, double FGAttempted, double ThreesMade, double ThreesAttempted, int Season)
        {
            this.SeasonId = SeasonId;
            this.PlayerId = PlayerId;
            this.Points = Points;
            this.Rebounds = Rebounds;
            this.Assists = Assists;
            this.Steals = Steals;
            this.Blocks = Blocks;
            this.FGMade = ThreesMade;
            this.FGAttempted = ThreesAttempted;
            this.ThreesMade = ThreesMade;
            this.ThreesAttempted = ThreesAttempted;
            this.Season = Season;
        }

    }
}
