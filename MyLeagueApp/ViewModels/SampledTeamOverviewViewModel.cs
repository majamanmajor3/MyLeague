using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyLeagueApp.Classes;
using MyLeagueApp.Classes.Samples;
using MyLeagueApp.Pages;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyLeagueApp.ViewModels
{
    [QueryProperty(nameof(TeamSample), "TeamSample")]
    partial class SampledTeamOverviewViewModel : ObservableObject
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

        [ObservableProperty]
        ObservableCollection<SeasonalStatsTeam> stats;

        [ObservableProperty]
        private TeamSample team;

        [ObservableProperty]
        private int teamId;

        [ObservableProperty]
        private string teamName;

        [ObservableProperty]
        private string teamCity;

        [ObservableProperty]
        private string teamAbbreviation;

        [ObservableProperty]
        private string teamDivision;

        [ObservableProperty]
        private string teamConference;

        [ObservableProperty]
        private string teamFullName;

        [ObservableProperty]
        private Arena teamArena;

        [ObservableProperty]
        private string teamArenaLocation;

        [ObservableProperty]
        private double awayStarAssists;

        Microsoft.Maui.ApplicationModel.IMap map;

        int current_team_id;
        int current_api_id;

        public SampledTeamOverviewViewModel(int team_id)
        {
            Players = new ObservableCollection<PlayerSample>();
            Stats = new ObservableCollection<SeasonalStatsTeam>();
            current_team_id = team_id;
            getTeamInfo(team_id);
            TeamFullName = TeamCity + " " + TeamName;
            loadPlayers(team_id);
            loadStats();

            List<PlayerSample> list = Players.ToList();
            list.Sort(CompareTeams);
            Players = new ObservableCollection<PlayerSample>(list);

            Microsoft.Maui.ApplicationModel.IMap map;
            //this.map = map;
        }

        private static int CompareTeams(PlayerSample b, PlayerSample a)
        {
            if (a.LastName.CompareTo(b.LastName) < 0) return 1;
            else return -1;
        }

        private void getTeamInfo(int team_id)
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

                String sql1 = "SELECT api_id FROM `sampled_teams` WHERE `team_id`=" + team_id + ";";

                sqlCmd = new MySqlCommand(sql1, sqlConn);
                sqlRd2 = sqlCmd.ExecuteReader();

                while (sqlRd2.Read())
                {

                }

                current_api_id = Int32.Parse(sqlRd2[0].ToString());
                sqlRd2.Close();

                String sql2 = "SELECT name FROM `sampled_teams` WHERE `team_id`=" + team_id + ";";

                sqlCmd = new MySqlCommand(sql2, sqlConn);
                sqlRd2 = sqlCmd.ExecuteReader();

                while (sqlRd2.Read())
                {

                }

                TeamName = sqlRd2[0].ToString();
                sqlRd2.Close();

                String df_sql = "SELECT city FROM `sampled_teams` WHERE `team_id`=" + team_id + ";";

                sqlCmd = new MySqlCommand(df_sql, sqlConn);
                sqlRdDF = sqlCmd.ExecuteReader();

                while (sqlRdDF.Read())
                {

                }

                TeamCity = sqlRdDF[0].ToString();

                sqlRdDF.Close();

                String sqlAbbr = "SELECT abbreviation FROM `sampled_teams` WHERE `team_id`=" + team_id + ";";

                sqlCmd = new MySqlCommand(sqlAbbr, sqlConn);
                sqlRd3 = sqlCmd.ExecuteReader();

                while (sqlRd3.Read())
                {

                }

                TeamAbbreviation = sqlRd3[0].ToString();

                sqlRd3.Close();

                String sqlDiv = "SELECT division FROM `sampled_teams` WHERE `team_id`=" + team_id + ";";

                sqlCmd = new MySqlCommand(sqlDiv, sqlConn);
                sqlRd3 = sqlCmd.ExecuteReader();

                while (sqlRd3.Read())
                {

                }

                TeamDivision = sqlRd3[0].ToString();

                sqlRd3.Close();

                String sqlConf = "SELECT conference FROM `sampled_teams` WHERE `team_id`=" + team_id + ";";

                sqlCmd = new MySqlCommand(sqlConf, sqlConn);
                sqlRd3 = sqlCmd.ExecuteReader();

                while (sqlRd3.Read())
                {

                }

                TeamConference = sqlRd3[0].ToString();

                sqlRd3.Close();

                //int arena_id;
                //string arena_name;
                //string arena_city;
                //string arena_state;
                //double arena_latitude;
                //double arena_longitude;

                //String sqlArena1 = "SELECT arena_id FROM `arenas` WHERE `team_id`=" + team_id + ";";

                //sqlCmd = new MySqlCommand(sqlArena1, sqlConn);
                //sqlRdA1 = sqlCmd.ExecuteReader();

                //while (sqlRdA1.Read())
                //{

                //}

                //arena_id = Int32.Parse(sqlRdA1[0].ToString());

                //sqlRdA1.Close();

                //String sqlArena2 = "SELECT name FROM `arenas` WHERE `team_id`=" + team_id + ";";

                //sqlCmd = new MySqlCommand(sqlArena2, sqlConn);
                //sqlRdA2 = sqlCmd.ExecuteReader();

                //while (sqlRdA2.Read())
                //{

                //}

                //arena_name = sqlRdA2[0].ToString();

                //sqlRdA2.Close();

                //String sqlArena3 = "SELECT city FROM `arenas` WHERE `team_id`=" + team_id + ";";

                //sqlCmd = new MySqlCommand(sqlArena3, sqlConn);
                //sqlRdA3 = sqlCmd.ExecuteReader();

                //while (sqlRdA3.Read())
                //{

                //}

                //arena_city = sqlRdA3[0].ToString();

                //sqlRdA3.Close();

                //String sqlArena4 = "SELECT state FROM `arenas` WHERE `team_id`=" + team_id + ";";

                //sqlCmd = new MySqlCommand(sqlArena4, sqlConn);
                //sqlRdA4 = sqlCmd.ExecuteReader();

                //while (sqlRdA4.Read())
                //{

                //}

                //arena_state = sqlRdA4[0].ToString();

                //sqlRdA4.Close();

                //String sqlArena5 = "SELECT latitude FROM `arenas` WHERE `team_id`=" + team_id + ";";

                //sqlCmd = new MySqlCommand(sqlArena5, sqlConn);
                //sqlRdA5 = sqlCmd.ExecuteReader();

                //while (sqlRdA5.Read())
                //{

                //}

                //arena_latitude = Double.Parse(sqlRdA5[0].ToString());

                //sqlRdA5.Close();

                //String sqlArena6 = "SELECT longitude FROM `arenas` WHERE `team_id`=" + team_id + ";";

                //sqlCmd = new MySqlCommand(sqlArena6, sqlConn);
                //sqlRdA6 = sqlCmd.ExecuteReader();

                //while (sqlRdA6.Read())
                //{

                //}

                //arena_longitude = Double.Parse(sqlRdA6[0].ToString());

                //sqlRdA6.Close();

                sqlConn.Close();

                //TeamArenaLocation = arena_city + ", " + arena_state;

                //TeamArena = new Arena(arena_id, arena_name, arena_city, arena_state, arena_latitude, arena_longitude, team_id);

            }
            catch (Exception ex)
            {
                //DisplayAlert("", ex.Message, "OK");
                sqlConn.Close();
            }

        }

        private void loadPlayers(int team_id)
        {
            try
            {
                string player_count = "0";

                int player_id;
                string first_name;
                string last_name;
                string position;
                string height_feet;
                string height_inches;
                string weight;

                sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                            ";password=" + password +
                                            ";database=" + database +
                                            ";convert zero datetime=True";

                sqlConn.Open();

                String sql = "SELECT COUNT(*) FROM `sampled_players` WHERE `team`=" + current_api_id + "; ";

                sqlCmd = new MySqlCommand(sql, sqlConn);
                sqlRd3 = sqlCmd.ExecuteReader();

                while (sqlRd3.Read())
                {

                }

                player_count = sqlRd3[0].ToString();
                sqlRd3.Close();
                sqlConn.Close();

                int cr = Int32.Parse(player_count);

                if (cr == 0)
                {
                    //label.IsVisible = true;
                }

                for (int i = 0; i < cr; i++)
                {
                    sqlConn.Open();

                    String sql_m_id = "SELECT player_id FROM sampled_players WHERE `team`=" + current_api_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_m_id, sqlConn);
                    sqlRd = sqlCmd.ExecuteReader();

                    while (sqlRd.Read())
                    {

                    }

                    player_id = Int32.Parse(sqlRd[0].ToString());
                    sqlRd.Close();

                    String sql_h_id = "SELECT first_name FROM sampled_players WHERE `team`=" + current_api_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_h_id, sqlConn);
                    sqlRd2 = sqlCmd.ExecuteReader();

                    while (sqlRd2.Read())
                    {

                    }

                    first_name = sqlRd2[0].ToString();
                    sqlRd2.Close();

                    String sql_a_id = "SELECT last_name FROM sampled_players WHERE `team`=" + current_api_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_a_id, sqlConn);
                    sqlRd4 = sqlCmd.ExecuteReader();

                    while (sqlRd4.Read())
                    {

                    }

                    last_name = sqlRd4[0].ToString();
                    sqlRd4.Close();

                    String sql_pos = "SELECT position FROM sampled_players WHERE `team`=" + current_api_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_pos, sqlConn);
                    sqlRd6 = sqlCmd.ExecuteReader();

                    while (sqlRd6.Read())
                    {

                    }

                    position = sqlRd6[0].ToString();
                    sqlRd6.Close();

                    String sql_hef = "SELECT height_feet FROM sampled_players WHERE `team`=" + current_api_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_hef, sqlConn);
                    sqlRd6 = sqlCmd.ExecuteReader();

                    while (sqlRd6.Read())
                    {

                    }

                    height_feet = sqlRd6[0].ToString();
                    sqlRd6.Close();

                    String sql_hei = "SELECT height_inches FROM sampled_players WHERE `team`=" + current_api_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_hei, sqlConn);
                    sqlRd6 = sqlCmd.ExecuteReader();

                    while (sqlRd6.Read())
                    {

                    }

                    height_inches = sqlRd6[0].ToString();
                    sqlRd6.Close();

                    String sql_we = "SELECT weight_pounds FROM sampled_players WHERE `team`=" + current_api_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_we, sqlConn);
                    sqlRd6 = sqlCmd.ExecuteReader();

                    while (sqlRd6.Read())
                    {

                    }

                    weight = sqlRd6[0].ToString();
                    sqlRd6.Close();

                    sqlConn.Close();

                    string full_name = first_name + " " + last_name;

                    string pos_name = "NA";

                    if (position == "G") pos_name = "Guard";
                    if (position == "PG") pos_name = "Point Guard";
                    if (position == "SG") pos_name = "Shooting Guard";
                    if (position == "F") pos_name = "Forward";
                    if (position == "SF") pos_name = "Small Forward";
                    if (position == "PF") pos_name = "Power Forward";
                    if (position == "C") pos_name = "Center";

                    Players.Add(new PlayerSample(player_id, first_name, last_name, current_api_id, position, pos_name, height_feet, height_inches, weight, full_name));
                }
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                sqlConn.Close();
            }
        }

        private void loadStats()
        {
            try
            {
                string seasons_count = "0";

                int season_id;
                int wins;
                int losses;
                double ppg;
                double appg;
                int season;

                sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                            ";password=" + password +
                                            ";database=" + database +
                                            ";convert zero datetime=True";

                sqlConn.Open();

                String sql = "SELECT COUNT(*) FROM `seasonal_stats_teams` WHERE `team_id`=" + current_api_id + "; ";

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

                    String sql_m_id = "SELECT stat_id FROM seasonal_stats_teams WHERE `team_id`=" + current_api_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_m_id, sqlConn);
                    sqlRd = sqlCmd.ExecuteReader();

                    while (sqlRd.Read())
                    {

                    }

                    season_id = Int32.Parse(sqlRd[0].ToString());
                    sqlRd.Close();

                    String sql_h_id = "SELECT wins FROM seasonal_stats_teams WHERE `team_id`=" + current_api_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_h_id, sqlConn);
                    sqlRd2 = sqlCmd.ExecuteReader();

                    while (sqlRd2.Read())
                    {

                    }

                    wins = Int32.Parse(sqlRd2[0].ToString());
                    sqlRd2.Close();

                    String sql_a_id = "SELECT losses FROM seasonal_stats_teams WHERE `team_id`=" + current_api_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_a_id, sqlConn);
                    sqlRd4 = sqlCmd.ExecuteReader();

                    while (sqlRd4.Read())
                    {

                    }

                    losses = Int32.Parse(sqlRd4[0].ToString());
                    sqlRd4.Close();

                    String sql_pos = "SELECT ppg FROM seasonal_stats_teams WHERE `team_id`=" + current_api_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_pos, sqlConn);
                    sqlRd6 = sqlCmd.ExecuteReader();

                    while (sqlRd6.Read())
                    {

                    }

                    ppg = Double.Parse(sqlRd6[0].ToString());
                    sqlRd6.Close();

                    String sql_st = "SELECT appg FROM seasonal_stats_teams WHERE `team_id`=" + current_api_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_st, sqlConn);
                    sqlRd6 = sqlCmd.ExecuteReader();

                    while (sqlRd6.Read())
                    {

                    }

                    appg = Double.Parse(sqlRd6[0].ToString());
                    sqlRd6.Close();

                    String sql_season = "SELECT season FROM seasonal_stats_teams WHERE `team_id`=" + current_api_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_season, sqlConn);
                    sqlRd6 = sqlCmd.ExecuteReader();

                    while (sqlRd6.Read())
                    {

                    }

                    season = Int32.Parse(sqlRd6[0].ToString());
                    sqlRd6.Close();

                    sqlConn.Close();

                    Stats.Add(new SeasonalStatsTeam(season_id, current_api_id, wins, losses, ppg, appg, season));
                }
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                sqlConn.Close();
            }
        }

        [RelayCommand]
        void AddPlayer()
        {
            Shell.Current.GoToAsync(nameof(NewPlayerPage));
        }

        [RelayCommand]
        async Task OpenMap()
        {
            try
            {
                await Map.Default.OpenAsync(TeamArena.Latitude, TeamArena.Longitude, new MapLaunchOptions
                {
                    Name = TeamArena.Name,
                    NavigationMode = NavigationMode.None
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to launch maps: {ex.Message}");
                await Shell.Current.DisplayAlert("Error, no Maps app!", ex.Message, "OK");
            }
        }

        [RelayCommand]
        async Task DeleteTeam()
        {
            try
            {
                bool answer = await Shell.Current.DisplayAlert("Attention", "Are you sure you want to delete this team?", "Yes", "No");

                if (answer)
                {
                    sqlConn.Open();

                    String sql_delete = "DELETE FROM `sampled_teams` WHERE `team_id`=" + current_team_id + "; ";

                    sqlCmd = new MySqlCommand(sql_delete, sqlConn);

                    sqlRd2 = sqlCmd.ExecuteReader();

                    while (sqlRd2.Read())
                    {

                    }

                    sqlRd2.Close();

                    sqlConn.Close();

                    Application.Current.MainPage.DisplayAlert("", "The team has been succesfully deleted!", "OK");

                    await Shell.Current.GoToAsync(nameof(TeamsPage));
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
            string result = await Shell.Current.DisplayPromptAsync("Sample Team Season", "Please provide a year from which you would like to import this team's stats");

            if (Int32.Parse(result) > 1980 && Int32.Parse(result) < 2024)
            {

                WebClient client = new WebClient();
                //String stats = client.DownloadString("https://free-nba.p.rapidapi.com/stats?seasons[]=2021&player_ids[]=237&rapidapi-key=ffe8de403amshdbfef1479d9fdafp10e8a0jsna7708bdc0688");
                String search_string = "https://free-nba.p.rapidapi.com/games?seasons[]=" + Int32.Parse(result) + "&team_ids[]=" + current_api_id + "&per_page=100&rapidapi-key=ffe8de403amshdbfef1479d9fdafp10e8a0jsna7708bdc0688";
                String stats = client.DownloadString(search_string);
                dynamic data = JObject.Parse(stats);
                int cr = 1;

                List<GameStatSample> list = new List<GameStatSample>();

                foreach (var member in data["data"])
                {
                    int game_id = (int)member["id"];
                    int home_id = (int)member["home_team"]["id"];
                    int away_id = (int)member["visitor_team"]["id"];
                    int home_score = (int)member["home_team_score"];
                    int away_score = (int)member["visitor_team_score"];
                    string date = (string)member["date"];
                    string postseason = (string)member["postseason"];

                    int year = Int32.Parse(result);

                    bool post;

                    if (postseason == "true") post = true;
                    else post = false;

                    GameStatSample stat = new(game_id, home_id, away_id, home_score, away_score, year, date, post);

                    list.Add(stat);
                    cr++;
                }

                if (list == null)
                {
                    Application.Current.MainPage.DisplayAlert("", "Your team has no available games!", "OK");
                    return;
                }

                double wins = 0;
                double losses = 0;
                double ppg = 0;
                double appg = 0;

                //List<PlayerStatSample> list = Matches.ToList();

                foreach (GameStatSample match in list)
                {
                    if(match.HomeId == current_api_id)
                    {
                        ppg += match.HomeScore;
                        appg += match.AwayScore;
                        if (match.HomeScore < match.AwayScore) losses += 1;
                        else if (match.HomeScore > match.AwayScore) wins += 1;
                    }
                    else if(match.AwayId == current_api_id)
                    {
                        ppg += match.AwayScore;
                        appg += match.HomeScore;
                        if (match.AwayScore < match.HomeScore) losses += 1;
                        else if (match.AwayScore > match.HomeScore) wins += 1;
                    }
                }

                ppg /= list.Count;
                appg /= list.Count;

                bool answer = await Shell.Current.DisplayAlert("Are you sure you want to import this season?",
                                                          "Season: " + Int32.Parse(result) + "\n" +
                                                          "Wins: " + wins + "\n" +
                                                          "Losses: " + losses + "\n" +
                                                          "Points/Game: " + Math.Round(ppg, 1) + "\n" +
                                                          "Opp. Points/Game: " + Math.Round(appg, 1) + "\n" +
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

                        foreach (GameStatSample match in list)
                        {

                            //String sql_id = "SELECT COUNT(*)+1 FROM `sampled_player_stats` ORDER BY stat_id DESC LIMIT 0,1; ";

                            //sqlCmd = new MySqlCommand(sql_id, sqlConn);
                            //sqlRd = sqlCmd.ExecuteReader();

                            //while (sqlRd.Read())
                            //{

                            //}

                            //int stat_id = Int32.Parse(sqlRd[0].ToString());
                            //sqlRd.Close();

                            // ELLENORZES!!!! //

                            String sql_check = "SELECT COUNT(*) FROM `sampled_games` WHERE `game_id` = " + match.Id + "; ";

                            sqlCmd = new MySqlCommand(sql_check, sqlConn);
                            sqlRd = sqlCmd.ExecuteReader();

                            while (sqlRd.Read())
                            {

                            }

                            int check = Int32.Parse(sqlRd[0].ToString());
                            sqlRd.Close();

                            if (check == 0)
                            {
                                int postS;

                                if (match.Postseason == true) postS = 1;
                                else postS = 0;

                                String sql_ins = "INSERT INTO `sampled_games` (`game_id`, `home_id`, `away_id`, `home_score`, `away_score`, `season`, `date`, `postseason`) " +
                                "VALUES (" + match.Id + ", '" + match.HomeId + "', '" + match.AwayId + "', '" + match.HomeScore + "', '" + match.AwayScore + "', '" + match.Season + "', '" + match.Date + "', '" + postS + "');";

                                sqlCmd = new MySqlCommand(sql_ins, sqlConn);
                                sqlRd = sqlCmd.ExecuteReader();

                                while (sqlRd.Read())
                                {

                                }

                                sqlRd.Close();
                            }

                        }

                        //String sql = "SELECT COUNT(*)+1 FROM `seasonal_stats` ORDER BY season_id DESC LIMIT 0,1; ";

                        //sqlCmd = new MySqlCommand(sql, sqlConn);
                        //sqlRd = sqlCmd.ExecuteReader();

                        //while (sqlRd.Read())
                        //{

                        //}

                        //int season_id = Int32.Parse(sqlRd[0].ToString());
                        //sqlRd.Close();

                        String sql2 = "INSERT INTO `seasonal_stats_teams` (`team_id`, `wins`, `losses`, `ppg`, `appg`, `season`) " +
                            "VALUES (" + current_api_id + ", '" + wins + "', '" + losses + "', '" + ppg + "', '" + appg + "', '" + Int32.Parse(result) + "');";

                        sqlCmd = new MySqlCommand(sql2, sqlConn);

                        sqlRd2 = sqlCmd.ExecuteReader();

                        while (sqlRd2.Read())
                        {

                        }

                        sqlRd2.Close();

                        sqlConn.Close();

                        Application.Current.MainPage.DisplayAlert("", "Your team's season has been imported!", "OK");

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

    }
}
