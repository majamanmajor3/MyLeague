using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeagueApp.Classes.Samples
{
    internal class TeamSample
    {
        public int Id { get; set; }
        public int ApiId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Abbreviation { get; set; }
        public string Division { get; set; }
        public string Conference { get; set; }
        public string FullName { get; set; }

        public TeamSample(int Id, int ApiId, string Name, string City, string Abbreviation, string Division, string Conference, string FullName)
        {
            this.Id = Id;
            this.ApiId = ApiId;
            this.Name = Name;
            this.City = City;
            this.Abbreviation = Abbreviation;
            this.Division = Division;
            this.Conference = Conference;
            this.FullName = FullName;
        }

        public override string ToString()
        {
            return City + " " + Name + ", " + Abbreviation;
        }
    }
}
