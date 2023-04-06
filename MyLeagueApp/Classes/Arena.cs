using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeagueApp.Classes
{
    internal class Arena
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int TeamId { get; set; }

        public Arena(int Id, string Name, string City, string State, double Latitude, double Longitude, int TeamId)
        {
            this.Id = Id;
            this.Name = Name;
            this.City = City;
            this.State = State;
            this.Latitude = Latitude;
            this.Longitude = Longitude;
            this.TeamId = TeamId;
        }

        public override string ToString()
        {
            return Name + " - " + City + ", " + State;
        }
    }
}
