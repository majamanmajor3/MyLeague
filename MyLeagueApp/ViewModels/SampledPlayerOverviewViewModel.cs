using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Google.Protobuf.WellKnownTypes;
using MyLeagueApp.Classes.Samples;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyLeagueApp.ViewModels
{
    partial class SampledPlayerOverviewViewModel : ObservableObject
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
        ObservableCollection<SeasonalStats> stats;

        [ObservableProperty]
        ObservableCollection<PlayerStatSample> matches;

        [ObservableProperty]
        private PlayerSample player;

        [ObservableProperty]
        private string playerFirstName;

        [ObservableProperty]
        private string playerLastName;

        [ObservableProperty]
        private int playerTeam;

        [ObservableProperty]
        private string playerPosition;

        [ObservableProperty]
        private string playerHeightFeet;

        [ObservableProperty]
        private string playerHeightInches;

        [ObservableProperty]
        private string playerWeightPounds;

        [ObservableProperty]
        private int playerAPIId; //ADD HOZZA ITT IS!!

        int current_player_id;

        public SampledPlayerOverviewViewModel(int player_id)
        {
            Stats = new ObservableCollection<SeasonalStats>();
            current_player_id = player_id;
            getPlayerInfo(player_id);
            loadStats(player_id);

            List<SeasonalStats> list = Stats.ToList();
            list.Sort(CompareStats);
            Stats = new ObservableCollection<SeasonalStats>(list);
        }

        private static int CompareStats(SeasonalStats b, SeasonalStats a)
        {
            if (a.Season > b.Season) return 1;
            else return -1;
        }

        private void getPlayerInfo(int player_id)
        {
            try
            {

                //int team_count;
                //string team_name;
                //string team_city;
                //string team_logo;

                sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                            ";password=" + password +
                                            ";database=" + database +
                                            ";convert zero datetime=True";

                sqlConn.Open();

                String sql2 = "SELECT first_name FROM `sampled_players` WHERE `player_id`=" + player_id + ";";

                sqlCmd = new MySqlCommand(sql2, sqlConn);
                sqlRd2 = sqlCmd.ExecuteReader();

                while (sqlRd2.Read())
                {

                }

                PlayerFirstName = sqlRd2[0].ToString();
                sqlRd2.Close();

                String df_sql = "SELECT last_name FROM `sampled_players` WHERE `player_id`=" + player_id + ";";

                sqlCmd = new MySqlCommand(df_sql, sqlConn);
                sqlRdDF = sqlCmd.ExecuteReader();

                while (sqlRdDF.Read())
                {

                }

                PlayerLastName = sqlRdDF[0].ToString();

                sqlRdDF.Close();

                String sqlAbbr = "SELECT team FROM `sampled_players` WHERE `player_id`=" + player_id + ";";

                sqlCmd = new MySqlCommand(sqlAbbr, sqlConn);
                sqlRd3 = sqlCmd.ExecuteReader();

                while (sqlRd3.Read())
                {

                }

                PlayerTeam = Int32.Parse(sqlRd3[0].ToString());

                sqlRd3.Close();

                String sqlDiv = "SELECT position FROM `sampled_players` WHERE `player_id`=" + player_id + ";";

                sqlCmd = new MySqlCommand(sqlDiv, sqlConn);
                sqlRd3 = sqlCmd.ExecuteReader();

                while (sqlRd3.Read())
                {

                }

                PlayerPosition = sqlRd3[0].ToString();

                sqlRd3.Close();

                String sqlConf = "SELECT height_feet FROM `sampled_players` WHERE `player_id`=" + player_id + ";";

                sqlCmd = new MySqlCommand(sqlConf, sqlConn);
                sqlRd3 = sqlCmd.ExecuteReader();

                while (sqlRd3.Read())
                {

                }

                PlayerHeightFeet = sqlRd3[0].ToString();

                sqlRd3.Close();

                String sqlHInches = "SELECT height_inches FROM `sampled_players` WHERE `player_id`=" + player_id + ";";

                sqlCmd = new MySqlCommand(sqlConf, sqlConn);
                sqlRd3 = sqlCmd.ExecuteReader();

                while (sqlRd3.Read())
                {

                }

                PlayerHeightInches = sqlRd3[0].ToString();

                sqlRd3.Close();

                String sqlWeigth = "SELECT weight_pounds FROM `sampled_players` WHERE `player_id`=" + player_id + ";";

                sqlCmd = new MySqlCommand(sqlConf, sqlConn);
                sqlRd3 = sqlCmd.ExecuteReader();

                while (sqlRd3.Read())
                {

                }

                PlayerWeightPounds = sqlRd3[0].ToString();

                String sql_api = "SELECT api_player_id FROM `sampled_players` WHERE `player_id`=" + player_id + ";";

                sqlCmd = new MySqlCommand(sqlConf, sqlConn);
                sqlRd3 = sqlCmd.ExecuteReader();

                while (sqlRd3.Read())
                {

                }

                PlayerAPIId = Int32.Parse(sqlRd3[0].ToString());

                sqlRd3.Close();

                sqlConn.Close();

                Player = new PlayerSample(player_id, PlayerFirstName, PlayerLastName, PlayerTeam, PlayerPosition, PlayerPosition, PlayerHeightFeet, PlayerHeightInches, PlayerWeightPounds, PlayerFirstName + " " + PlayerLastName);

            }
            catch (Exception ex)
            {
                //DisplayAlert("", ex.Message, "OK");
                sqlConn.Close();
            }

        }

        private void loadStats(int player_id)
        {
            try
            {
                string seasons_count = "0";

                int season_id;
                double points;
                double rebounds;
                double assists;
                double steals;
                double blocks;
                double fg_made;
                double fg_att;
                double threes_made;
                double threes_att;
                int season;

                sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                            ";password=" + password +
                                            ";database=" + database +
                                            ";convert zero datetime=True";

                sqlConn.Open();

                String sql = "SELECT COUNT(*) FROM `seasonal_stats` WHERE `player_id`=" + player_id + "; ";

                sqlCmd = new MySqlCommand(sql, sqlConn);
                sqlRd3 = sqlCmd.ExecuteReader();

                while (sqlRd3.Read())
                {

                }

                seasons_count = sqlRd3[0].ToString();
                sqlRd3.Close();
                sqlConn.Close();

                int cr = Int32.Parse(seasons_count);

                if (cr == 0)
                {
                    //label.IsVisible = true;
                }

                for (int i = 0; i < cr; i++)
                {
                    sqlConn.Open();

                    String sql_m_id = "SELECT season_id FROM seasonal_stats WHERE `player_id`=" + player_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_m_id, sqlConn);
                    sqlRd = sqlCmd.ExecuteReader();

                    while (sqlRd.Read())
                    {

                    }

                    season_id = Int32.Parse(sqlRd[0].ToString());
                    sqlRd.Close();

                    String sql_h_id = "SELECT points FROM seasonal_stats WHERE `player_id`=" + player_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_h_id, sqlConn);
                    sqlRd2 = sqlCmd.ExecuteReader();

                    while (sqlRd2.Read())
                    {

                    }

                    points = Double.Parse(sqlRd2[0].ToString());
                    sqlRd2.Close();

                    String sql_a_id = "SELECT rebounds FROM seasonal_stats WHERE `player_id`=" + player_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_a_id, sqlConn);
                    sqlRd4 = sqlCmd.ExecuteReader();

                    while (sqlRd4.Read())
                    {

                    }

                    rebounds = Double.Parse(sqlRd4[0].ToString());
                    sqlRd4.Close();

                    String sql_pos = "SELECT assists FROM seasonal_stats WHERE `player_id`=" + player_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_pos, sqlConn);
                    sqlRd6 = sqlCmd.ExecuteReader();

                    while (sqlRd6.Read())
                    {

                    }

                    assists = Double.Parse(sqlRd6[0].ToString());
                    sqlRd6.Close();

                    String sql_st = "SELECT steals FROM seasonal_stats WHERE `player_id`=" + player_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_st, sqlConn);
                    sqlRd6 = sqlCmd.ExecuteReader();

                    while (sqlRd6.Read())
                    {

                    }

                    steals = Double.Parse(sqlRd6[0].ToString());
                    sqlRd6.Close();

                    String sql_bl = "SELECT blocks FROM seasonal_stats WHERE `player_id`=" + player_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_bl, sqlConn);
                    sqlRd6 = sqlCmd.ExecuteReader();

                    while (sqlRd6.Read())
                    {

                    }

                    blocks = Double.Parse(sqlRd6[0].ToString());
                    sqlRd6.Close();

                    String sql_fgm = "SELECT fg_made FROM seasonal_stats WHERE `player_id`=" + player_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_fgm, sqlConn);
                    sqlRd6 = sqlCmd.ExecuteReader();

                    while (sqlRd6.Read())
                    {

                    }

                    fg_made = Double.Parse(sqlRd6[0].ToString());
                    sqlRd6.Close();

                    String sql_fga = "SELECT fg_attempted FROM seasonal_stats WHERE `player_id`=" + player_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_fga, sqlConn);
                    sqlRd6 = sqlCmd.ExecuteReader();

                    while (sqlRd6.Read())
                    {

                    }

                    fg_att = Double.Parse(sqlRd6[0].ToString());
                    sqlRd6.Close();

                    String sql_3m = "SELECT threes_made FROM seasonal_stats WHERE `player_id`=" + player_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_3m, sqlConn);
                    sqlRd6 = sqlCmd.ExecuteReader();

                    while (sqlRd6.Read())
                    {

                    }

                    threes_made = Double.Parse(sqlRd6[0].ToString());
                    sqlRd6.Close();

                    String sql_3a = "SELECT threes_attempted FROM seasonal_stats WHERE `player_id`=" + player_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_3a, sqlConn);
                    sqlRd6 = sqlCmd.ExecuteReader();

                    while (sqlRd6.Read())
                    {

                    }

                    threes_att = Double.Parse(sqlRd6[0].ToString());
                    sqlRd6.Close();

                    String sql_season = "SELECT season FROM seasonal_stats WHERE `player_id`=" + player_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_season, sqlConn);
                    sqlRd6 = sqlCmd.ExecuteReader();

                    while (sqlRd6.Read())
                    {

                    }

                    season = Int32.Parse(sqlRd6[0].ToString());
                    sqlRd6.Close();

                    sqlConn.Close();

                    Stats.Add(new SeasonalStats(season_id, player_id, points, rebounds, assists, steals, blocks, fg_made, fg_att, threes_made, threes_att, season));
                }
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                sqlConn.Close();
            }
        }

        [RelayCommand]
        async void AddSeason()
        {
            string result = await Shell.Current.DisplayPromptAsync("Sample Player Season", "Please provide a year from which you would like to import this player's stats");

            if (Int32.Parse(result) > 1980 && Int32.Parse(result) < 2024)
            {

                WebClient client = new WebClient();
                //String stats = client.DownloadString("https://free-nba.p.rapidapi.com/stats?seasons[]=2021&player_ids[]=237&rapidapi-key=ffe8de403amshdbfef1479d9fdafp10e8a0jsna7708bdc0688");
                String search_string = "https://free-nba.p.rapidapi.com/stats?seasons[]=" + Int32.Parse(result) + "&player_ids[]=" + PlayerAPIId + "&per_page=100&rapidapi-key=ffe8de403amshdbfef1479d9fdafp10e8a0jsna7708bdc0688";
                String stats = client.DownloadString(search_string);
                dynamic data = JObject.Parse(stats);
                int cr = 1;

                foreach (var member in data["data"])
                {
                    int player_id = Player.Id;
                    int game_id = (int)member["id"];
                    int points = (int)member["pts"];
                    int rebounds = (int)member["reb"];
                    int assists = (int)member["ast"];
                    int steals = (int)member["stl"];
                    int blocks = (int)member["blk"];
                    int fg_made = (int)member["fgm"];
                    int fg_att = (int)member["fga"];
                    int threes_made = (int)member["fg3m"];
                    int threes_att = (int)member["fg3a"];

                    Matches.Add(new PlayerStatSample(cr, player_id, game_id, points, rebounds, assists, steals, blocks, fg_made, fg_att, threes_made, threes_att, Int32.Parse(result)));
                    cr++;
                }

                double points_avg = 0;
                double rebounds_avg = 0;
                double assists_avg = 0;
                double steals_avg = 0;
                double blocks_avg = 0;
                double fg_made_avg = 0;
                double fg_att_avg = 0;
                double threes_made_avg = 0;
                double threes_att_avg = 0;

                List<PlayerStatSample> list = Matches.ToList();

                foreach (PlayerStatSample match in list)
                {
                    points_avg += match.Points;
                    rebounds_avg += match.Rebounds;
                    assists_avg += match.Assists;
                    steals_avg += match.Steals;
                    blocks_avg += match.Blocks;
                    fg_made_avg += match.FGMade;
                    fg_att_avg += match.FGAttempted;
                    threes_made_avg += match.ThreesMade;
                    threes_att_avg += match.ThreesAttempted;
                }

                points_avg /= list.Count;
                rebounds_avg /= list.Count;
                assists_avg /= list.Count;
                steals_avg /= list.Count;
                blocks_avg /= list.Count;
                fg_made_avg /= list.Count;
                fg_att_avg /= list.Count;
                threes_made_avg /= list.Count;
                threes_att_avg /= list.Count;

                bool answer = await Shell.Current.DisplayAlert("Are you sure you want to import this season?",
                                                          "Season: " + Int32.Parse(result) + "\n" +
                                                          "Points/Game: " + points_avg + "\n" +
                                                          "Rebounds/Game: " + rebounds_avg + "\n" +
                                                          "Assists/Game: " + assists_avg + "\n" +
                                                          "Steals/Game: " + steals_avg + "\n" +
                                                          "Blocks/Game: " + blocks_avg + "\n" +
                                                          "Field Goals Made/Game: " + fg_made_avg + "\n" +
                                                          "Field Goals Attempted/Game: " + fg_att_avg + "\n" +
                                                          "Three Pointers Made/Game: " + threes_made_avg + "\n" +
                                                          "Three Pointers Attempted/Game: " + threes_att_avg + "\n" +
                                                          "Games Played: " + list.Count
                                                          , "Yes", "No");

                if (answer)
                {
                    try
                    {

                        int match_count;

                        sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                                    ";password=" + password +
                                                    ";database=" + database +
                                                    ";convert zero datetime=True";

                        sqlConn.Open();

                        foreach (PlayerStatSample match in list)
                        {

                            String sql_id = "SELECT COUNT(*)+1 FROM `sampled_player_stats` ORDER BY stat_id DESC LIMIT 0,1; ";

                            sqlCmd = new MySqlCommand(sql_id, sqlConn);
                            sqlRd = sqlCmd.ExecuteReader();

                            while (sqlRd.Read())
                            {

                            }

                            int stat_id = Int32.Parse(sqlRd[0].ToString());
                            sqlRd.Close();

                            String sql_ins = "INSERT INTO `sampled_player_stats` (`stat_id`, `player_id`, `game_id`, `points`, `rebounds`, `assists`, `steals`, `blocks`, `fg_made`, `fg_attempted`, `threes_made`, `threes_attempted`, `season`) " +
                            "VALUES (" + stat_id + ", '" + Player.Id + "', '" + match.MatchId + "', '" + match.Points + "', '" + match.Rebounds + "', '" + match.Assists + "', '" + match.Steals + "', '" + match.Blocks + "', '" + match.FGMade + "', '" + match.FGAttempted + "', '" + match.ThreesMade + "', '" + match.ThreesAttempted + "', '" + Int32.Parse(result) + "');";

                            sqlCmd = new MySqlCommand(sql_ins, sqlConn);
                            sqlRd = sqlCmd.ExecuteReader();

                            while (sqlRd.Read())
                            {

                            }

                            sqlRd.Close();

                        }

                        String sql = "SELECT COUNT(*)+1 FROM `seasonal_stats` ORDER BY season_id DESC LIMIT 0,1; ";

                        sqlCmd = new MySqlCommand(sql, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        int season_id = Int32.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql2 = "INSERT INTO `seasonal_stats` (`season_id`, `player_id`, `points`, `rebounds`, `assists`, `steals`, `blocks`, `fg_made`, `fg_attempted`, `threes_made`, `threes_attempted`, `season`) " +
                            "VALUES (" + season_id + ", '" + Player.Id + "', '" + points_avg + "', '" + rebounds_avg + "', '" + assists_avg + "', '" + steals_avg + "', '" + blocks_avg + "', '" + fg_made_avg + "', '" + fg_att_avg + "', '" + threes_made_avg + "', '" + threes_att_avg + "', '" + Int32.Parse(result) + "');";

                        sqlCmd = new MySqlCommand(sql2, sqlConn);

                        sqlRd2 = sqlCmd.ExecuteReader();

                        while (sqlRd2.Read())
                        {

                        }

                        sqlRd2.Close();

                        sqlConn.Close();

                        Application.Current.MainPage.DisplayAlert("", "Your player's season has been imported!", "OK");

                        //Shell.Current.GoToAsync(nameof(MainPage));

                        Task.CompletedTask.Dispose();

                    }
                    catch (Exception ex)
                    {
                        Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                        sqlConn.Close();
                    }
                }

            }
            else
            {
                Application.Current.MainPage.DisplayAlert("", "Please provide a proper year!", "OK");
                Task.CompletedTask.Dispose();
            }
        }

        [RelayCommand]
        async Task DeletePlayer()
        {
            try
            {
                bool answer = await Shell.Current.DisplayAlert("Attention", "Are you sure you want to delete this player?", "Yes", "No");

                if (answer)
                {
                    sqlConn.Open();

                    String sql_delete = "DELETE FROM `sampled_players` WHERE `player_id`=" + current_player_id + "; ";

                    sqlCmd = new MySqlCommand(sql_delete, sqlConn);

                    sqlRd2 = sqlCmd.ExecuteReader();

                    while (sqlRd2.Read())
                    {

                    }

                    sqlRd2.Close();

                    sqlConn.Close();

                    Application.Current.MainPage.DisplayAlert("", "The player has been succesfully deleted!", "OK");

                    //STATISZTIKAKAT IS TOROLD!!!

                    await Shell.Current.GoToAsync(nameof(TeamsPage));
                }
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                sqlConn.Close();
            }

        }

    }
}
