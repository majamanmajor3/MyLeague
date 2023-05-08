using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeagueApp.Classes.Samples
{
    internal class PlayerSample
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Team { get; set; }
        public string PositionLetter { get; set; }
        public string PositionName { get; set; }
        public int HeightFeet { get; set; }
        public int HeightInches { get; set; }
        public int Weight { get; set; }
        public string FullName { get; set; }

        public PlayerSample(int Id, string FirstName, string LastName, int Team, string PositionLetter, string PositionName, int HeightFeet, int HeightInches, int Weight, string FullName)
        {
            this.Id = Id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Team = Team;
            this.PositionLetter = PositionLetter;
            this.PositionName = PositionName;
            this.HeightFeet = HeightFeet;
            this.HeightInches = HeightInches;
            this.Weight = Weight;
            this.FullName = FullName;
        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
