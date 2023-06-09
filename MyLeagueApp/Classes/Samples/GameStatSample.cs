using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeagueApp.Classes.Samples
{
    internal class GameStatSample
    {
        public int Id { get; set; }
        public int HomeId { get; set; }
        public int AwayId { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public int Season { get; set; }
        public string Date { get; set; }
        public bool Postseason { get; set; }

        public GameStatSample(int Id, int HomeId, int AwayId, int HomeScore, int AwayScore, int Season, string Date, bool Postseason)
        {
            this.Id = Id;
            this.HomeId = HomeId;
            this.AwayId = AwayId;
            this.HomeScore = HomeScore;
            this.AwayScore = AwayScore;
            this.Season = Season;
            this.Date = Date;
            this.Postseason = Postseason;
        }

    }
}
