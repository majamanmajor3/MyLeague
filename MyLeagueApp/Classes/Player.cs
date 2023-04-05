using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeagueApp.Classes
{
    internal class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Team { get; set; }
        public string Photo { get; set; }
        public int Position { get; set; }
        public string PositionName { get; set; }
        public string FullName { get; set; }

        public Player(int Id, string FirstName, string LastName, int Team, string Photo, int Position, string PositionName, string FullName)
        {
            this.Id = Id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Team = Team;
            this.Photo = Photo;
            this.Position = Position;
            this.PositionName = PositionName;
            this.FullName = FullName;
        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
