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
        public double FreeThrowsMade { get; set; }
        public double FreeThrowsAttempted { get; set; }
        public double Turnovers { get; set; }
        public double PersonalFouls { get; set; }
        public double MinutesPlayed { get; set; }
        public int Season { get; set; }
        public int Team { get; set; }
        public double Efficiency { get; set; }
        public double TrueShooting { get; set; }
        public double FreeThrowRate { get; set; }

        public SeasonalStats(int StatId, int PlayerId, double Points, double Rebounds, double Assists, double Steals, double Blocks, double FGMade, double FGAttempted, double ThreesMade, double ThreesAttempted, double FreeThrowsMade, double FreeThrowsAttempted, double Turnovers, double PersonalFouls, double MinutesPlayed, int Season, int Team, double Efficiency, double TrueShooting, double FreeThrowRate)
        {
            this.SeasonId = SeasonId;
            this.PlayerId = PlayerId;
            this.Points = Points;
            this.Rebounds = Rebounds;
            this.Assists = Assists;
            this.Steals = Steals;
            this.Blocks = Blocks;
            this.FGMade = FGMade;
            this.FGAttempted = FGAttempted;
            this.ThreesMade = ThreesMade;
            this.ThreesAttempted = ThreesAttempted;
            this.FreeThrowsMade = FreeThrowsMade;
            this.FreeThrowsAttempted = FreeThrowsAttempted;
            this.Turnovers = Turnovers;
            this.PersonalFouls = PersonalFouls;
            this.MinutesPlayed = MinutesPlayed;
            this.Season = Season;
            this.Team = Team;
            this.Efficiency = Efficiency;
            this.TrueShooting = TrueShooting;
            this.FreeThrowRate = FreeThrowRate;
        }

    }
}
