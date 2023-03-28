using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeagueApp.Classes
{
    internal class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Logo { get; set; }

        public Team(int Id, string Name, string City, string Logo)
        {
            this.Id = Id;
            this.Name = Name;
            this.City = City;
            this.Logo = Logo;
        }

        public override string ToString()
        {
            return City + " " + Name;
        }
    }
}
