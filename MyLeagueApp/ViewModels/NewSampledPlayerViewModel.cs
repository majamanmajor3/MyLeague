using CommunityToolkit.Mvvm.ComponentModel;
using MyLeagueApp.Classes;
using MyLeagueApp.Classes.Samples;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyLeagueApp.ViewModels
{
    partial class NewSampledPlayerViewModel : ObservableObject
    {
        MySqlConnection sqlConn = new MySqlConnection();
        MySqlCommand sqlCmd = new MySqlCommand();
        String sqlQuery;
        MySqlDataAdapter DtA = new MySqlDataAdapter();
        MySqlDataReader sqlRd;
        MySqlDataReader sqlRdC;
        MySqlDataReader sqlRd2;
        MySqlDataReader sqlRd3;
        MySqlDataReader sqlRd4;
        MySqlDataReader sqlRd5;
        MySqlDataReader sqlRd6;
        MySqlDataReader sqlRd7;
        MySqlDataReader sqlRd8;
        MySqlDataReader sqlRdDF;
        MySqlDataReader sqlRdA1;
        MySqlDataReader sqlRdA2;
        MySqlDataReader sqlRdA3;
        MySqlDataReader sqlRdA4;
        MySqlDataReader sqlRdA5;
        MySqlDataReader sqlRdA6;
        MySqlDataReader sqlRdDate;

        String server = "localhost";
        String username = "root";
        String password = "";
        String database = "myleague";

        [ObservableProperty]
        ObservableCollection<PlayerSample> players;

        string current_searched_player;

        public NewSampledPlayerViewModel(string player_name)
        {
            Players = new ObservableCollection<PlayerSample>();
            current_searched_player = player_name;
            loadPlayers(current_searched_player);
        }

        private void loadPlayers(string player_name)
        {
            int player_id;
            string player_fname;
            string player_lname;
            int player_team; //egyelore a kivalasztott csapathoz megy
            string player_posletter;
            string player_posname;
            string player_heightfeet;
            string player_heightinches;
            string player_weight;
            string player_fullname;

            WebClient client = new WebClient();
            //String stats = client.DownloadString("https://free-nba.p.rapidapi.com/stats?seasons[]=2021&player_ids[]=237&rapidapi-key=ffe8de403amshdbfef1479d9fdafp10e8a0jsna7708bdc0688");
            String search_string = "https://free-nba.p.rapidapi.com/players?search=" + player_name + "&rapidapi-key=ffe8de403amshdbfef1479d9fdafp10e8a0jsna7708bdc0688";
            String teams = client.DownloadString(search_string);
            dynamic data = JObject.Parse(teams);

            foreach (var member in data["data"])
            {
                player_id = (int)member["id"];
                player_fname = (string)member["first_name"];
                player_lname = (string)member["last_name"];

                //JsonArray team = (JsonArray)member["team"];
                //JsonObject teamObject = (JsonObject)team[0];
                //string player_team_string = teamObject["id"].ToString();
                //player_team = Int32.Parse(player_team_string);

                //JSONArray team = member.getJSONArray("team");
                //JSONObject teamObject = team.GetJSONObject(0);
                //JSONObject teamData = teamObject.GetJSONObject("team");
                //String teamNumber = teamData.GetString("id");
                //player_team = Int32.Parse(teamNumber);

                //JArray team = (JArray)member["team"];
                //var jArray = JArray.Parse(team);
                //var result = jArray.FirstOrDefault()?["value"]?.Value<DateTime>();
                //player_team = Int32.Parse(teamNumber);

                player_team = (int)member["team"]["id"];

                player_posletter = (string)member["position"];
                player_heightfeet = (string)member["height_feet"];
                player_heightinches = (string)member["height_inches"];
                player_weight = (string)member["weight"];

                player_fullname = player_fname + " " + player_lname;

                player_posname = "";

                if (player_posletter == "G") player_posname = "Guard";
                if (player_posletter == "F") player_posname = "Forward";
                if (player_posletter == "C") player_posname = "Center";
                if (player_posletter == "") player_posname = "Not Specified";

                Players.Add(new PlayerSample(player_id, player_fname, player_lname, player_team, player_posletter, player_posname, player_heightfeet, player_heightinches, player_weight, player_fullname));
            }


        }
    }
}
