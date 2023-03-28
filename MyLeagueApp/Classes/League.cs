using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeagueApp.Classes
{
    internal class League
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedTime { get; set; }

        public League(int Id, string Name, string CreatedTime)
        {
            this.Id = Id;
            this.Name = Name;
            this.CreatedTime = CreatedTime;
        }
    }
}
