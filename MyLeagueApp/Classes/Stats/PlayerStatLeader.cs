using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeagueApp.Classes.Stats
{
    internal class PlayerStatLeader
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Points { get; set; }
        public double Rebounds { get; set; }
        public double Assists { get; set; }
        public double Steals { get; set; }
        public double Blocks { get; set; }
        public double ThreesMade { get; set; }
        public double ThreesAttempted { get; set; }
        public double ThreesPercentage { get; set; }
        public string Photo { get; set; }
        public string Team { get; set; }

        public PlayerStatLeader(int Id, int MatchId, string FirstName, string LastName, double Points, double Rebounds, double Assists, double Steals, double Blocks, double ThreesMade, double ThreesAttempted, double ThreesPercentage, string Photo, string Team)
        {
            this.Id = Id;
            this.MatchId = MatchId;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Points = Points;
            this.Rebounds = Rebounds;
            this.Assists = Assists;
            this.Steals = Steals;
            this.Blocks = Blocks;
            this.ThreesMade = ThreesMade;
            this.ThreesAttempted = ThreesAttempted;
            this.ThreesPercentage = ThreesPercentage;
            this.Photo = Photo;
            this.Team = Team;
        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
