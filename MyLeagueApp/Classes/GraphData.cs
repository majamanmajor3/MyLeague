using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeagueApp.Classes
{
    internal class GraphData
    {
        public int Id { get; set; }
        public double Data { get; set; }

        public GraphData(int Id, double Data)
        {
            this.Id = Id;
            this.Data = Data;
        }

    }
}
