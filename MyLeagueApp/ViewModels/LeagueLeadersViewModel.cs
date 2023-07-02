using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyLeagueApp.Classes;
using MyLeagueApp.Classes.Stats;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.IO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeagueApp.ViewModels
{
    partial class LeagueLeadersViewModel : ObservableObject
    {
        MySqlConnection sqlConn = new MySqlConnection();
        MySqlConnection sqlConn2 = new MySqlConnection();
        MySqlConnection sqlConn3 = new MySqlConnection();
        MySqlCommand sqlCmd = new MySqlCommand();
        String sqlQuery;
        MySqlDataAdapter DtA = new MySqlDataAdapter();
        MySqlDataReader sqlRd;
        MySqlDataReader sqlRdA;
        MySqlDataReader sqlRdC;
        MySqlDataReader sqlRdC1;
        MySqlDataReader sqlRdC2;
        MySqlDataReader sqlRdC3;
        MySqlDataReader sqlRdC4;
        MySqlDataReader sqlRdC5;
        MySqlDataReader sqlRdC6;
        MySqlDataReader sqlRd2;
        MySqlDataReader sqlRd3;
        MySqlDataReader sqlRd4;
        MySqlDataReader sqlRd5;
        MySqlDataReader sqlRd6;
        MySqlDataReader sqlRdHS1;
        MySqlDataReader sqlRdDF;
        MySqlDataReader sqlRdDate;

        string current_league_name;

        String server = "localhost";
        String username = "root";
        String password = "";
        String database = "myleague";

        [ObservableProperty]
        ObservableCollection<Player> players;

        [ObservableProperty]
        ObservableCollection<PlayerStatLeader> playerstats;

        [ObservableProperty]
        ObservableCollection<Team> teams;

        public LeagueLeadersViewModel(string league_name)
        {
            Teams = new ObservableCollection<Team>();
            Players = new ObservableCollection<Player>();
            Playerstats = new ObservableCollection<PlayerStatLeader>();
            current_league_name = league_name;
            loadTeams();
            loadPlayers();
            loadStats();

            List<PlayerStatLeader> list = Playerstats.ToList();
            list.Sort(CompareTeamsPoints);
            Playerstats = new ObservableCollection<PlayerStatLeader>(list);
        }

        private void loadTeams()
        {
            try
            {

                int match_count;
                int home_id;
                int away_id;
                int[] check = new int[200];
                int[] teams_id = new int[200];
                int cr = 0;
                string team_id;
                string team_name;
                string team_city;
                string team_logo;

                sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                            ";password=" + password +
                                            ";database=" + database +
                                            ";convert zero datetime=True";

                sqlConn.Open();

                String sqlc = "SELECT COUNT(*) FROM `" + current_league_name + "`; ";

                sqlCmd = new MySqlCommand(sqlc, sqlConn);
                sqlRdC = sqlCmd.ExecuteReader();

                while (sqlRdC.Read())
                {

                }

                match_count = Int32.Parse(sqlRdC[0].ToString());
                sqlRdC.Close();

                for (int i = 0; i < match_count; i++)
                {

                    String sql_h = "SELECT home_team FROM `" + current_league_name + "` WHERE 1 LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_h, sqlConn);
                    sqlRd = sqlCmd.ExecuteReader();

                    while (sqlRd.Read())
                    {

                    }

                    home_id = Int32.Parse(sqlRd[0].ToString());
                    sqlRd.Close();

                    if (check[home_id] == 0)
                    {
                        check[home_id]++;
                        teams_id[cr] = home_id;
                        cr++;
                    }
                    else continue;

                    String sql_a = "SELECT away_team FROM `" + current_league_name + "` WHERE 1 LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_a, sqlConn);
                    sqlRdA = sqlCmd.ExecuteReader();

                    while (sqlRdA.Read())
                    {

                    }

                    away_id = Int32.Parse(sqlRdA[0].ToString());
                    sqlRdA.Close();

                    if (check[away_id] == 0)
                    {
                        check[away_id]++;
                        teams_id[cr] = away_id;
                        cr++;
                    }
                    else continue;

                }

                for (int i = 0; i < teams_id.Length; i++)
                {

                    String sql2 = "SELECT name FROM `teams` WHERE `team_id` = " + teams_id[i] + "; ";

                    sqlCmd = new MySqlCommand(sql2, sqlConn);
                    sqlRd2 = sqlCmd.ExecuteReader();

                    while (sqlRd2.Read())
                    {

                    }

                    team_name = sqlRd2[0].ToString();
                    sqlRd2.Close();

                    String df_sql = "SELECT city FROM `teams` WHERE `team_id` = " + teams_id[i] + "; ";

                    sqlCmd = new MySqlCommand(df_sql, sqlConn);
                    sqlRdDF = sqlCmd.ExecuteReader();

                    while (sqlRdDF.Read())
                    {

                    }

                    team_city = sqlRdDF[0].ToString();

                    sqlRdDF.Close();

                    String sqlLogo = "SELECT logo FROM `teams` WHERE `team_id` = " + teams_id[i] + "; ";

                    sqlCmd = new MySqlCommand(sqlLogo, sqlConn);
                    sqlRd3 = sqlCmd.ExecuteReader();

                    while (sqlRd3.Read())
                    {

                    }

                    team_logo = sqlRd3[0].ToString();

                    sqlRd3.Close();

                    Teams.Add(new Team(teams_id[i], team_name, team_city, team_logo));
                }

                sqlConn.Close();

            }
            catch (Exception ex)
            {
                //Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }

        }

        private void loadPlayers()
        {
            try
            {

                int player_count;
                int home_id;
                int away_id;
                int[] check = new int[200];
                int[] teams_id = new int[200];
                int cr = 0;
                int team_id;
                string team_name;
                string team_city;
                string team_logo;

                sqlConn2.ConnectionString = "server=" + server + ";user id=" + username +
                                            ";password=" + password +
                                            ";database=" + database +
                                            ";convert zero datetime=True";

                String sqlc;

                sqlConn2.Open();

                int count = Teams.Count;

                for (int i = 0; i < Teams.Count; i++)
                {

                    team_id = Teams[i].Id;

                    sqlc = "SELECT COUNT(*) FROM `players` WHERE `team`=" + team_id + "; ";

                    sqlCmd = new MySqlCommand(sqlc, sqlConn2);
                    sqlRdC = sqlCmd.ExecuteReader();

                    while (sqlRdC.Read())
                    {

                    }

                    player_count = Int32.Parse(sqlRdC[0].ToString());
                    sqlRdC.Close();

                    int player_id;
                    string first_name;
                    string last_name;
                    string photo;
                    int position;

                    for (int j = 0; j < player_count; j++)
                    {

                        String sql_m_id = "SELECT player_id FROM players WHERE `team`=" + team_id + " LIMIT " + j + ",1; ";

                        sqlCmd = new MySqlCommand(sql_m_id, sqlConn2);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        player_id = Int32.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_h_id = "SELECT first_name FROM players WHERE `team`=" + team_id + " LIMIT " + j + ",1; ";

                        sqlCmd = new MySqlCommand(sql_h_id, sqlConn2);
                        sqlRd2 = sqlCmd.ExecuteReader();

                        while (sqlRd2.Read())
                        {

                        }

                        first_name = sqlRd2[0].ToString();
                        sqlRd2.Close();

                        String sql_a_id = "SELECT last_name FROM players WHERE `team`=" + team_id + " LIMIT " + j + ",1; ";

                        sqlCmd = new MySqlCommand(sql_a_id, sqlConn2);
                        sqlRd4 = sqlCmd.ExecuteReader();

                        while (sqlRd4.Read())
                        {

                        }

                        last_name = sqlRd4[0].ToString();
                        sqlRd4.Close();

                        String sql_h_score = "SELECT photo FROM players WHERE `team`=" + team_id + " LIMIT " + j + ",1; ";

                        sqlCmd = new MySqlCommand(sql_h_score, sqlConn2);
                        sqlRd5 = sqlCmd.ExecuteReader();

                        while (sqlRd5.Read())
                        {

                        }

                        photo = sqlRd5[0].ToString();
                        sqlRd5.Close();

                        String sql_pos = "SELECT position FROM players WHERE `team`=" + team_id + " LIMIT " + j + ",1; ";

                        sqlCmd = new MySqlCommand(sql_pos, sqlConn2);
                        sqlRd6 = sqlCmd.ExecuteReader();

                        while (sqlRd6.Read())
                        {

                        }

                        position = Int32.Parse(sqlRd6[0].ToString());
                        sqlRd6.Close();

                        string full_name = first_name + " " + last_name;

                        string pos_name = "NA";

                        if (position == 1) pos_name = "Point Guard";
                        if (position == 2) pos_name = "Shooting Guard";
                        if (position == 3) pos_name = "Small Forward";
                        if (position == 4) pos_name = "Power Forward";
                        if (position == 5) pos_name = "Center";

                        Players.Add(new Player(player_id, first_name, last_name, team_id, photo, position, pos_name, full_name));

                    }

                }

                sqlConn2.Close();

            }
            catch (Exception ex)
            {
                //Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }

        }

        private void loadStats()
        {
            try
            {

                int player_id;
                int team_id;
                string team_name;
                int match_id = 0;
                string first_name;
                string last_name;
                string photo;
                int position;

                int home_gamecount;
                int away_gamecount;
                int total_points = 0;
                double average_points;
                int total_rebounds = 0;
                double average_rebounds;
                int total_assists = 0;
                double average_assists;
                int total_steals = 0;
                double average_steals;
                int total_blocks = 0;
                double average_blocks;
                int total_threesmade = 0;
                double average_threesmade;
                int total_threesatt = 0;
                double average_threesatt;

                int gamecount = 0;

                sqlConn3.ConnectionString = "server=" + server + ";user id=" + username +
                                            ";password=" + password +
                                            ";database=" + database +
                                            ";convert zero datetime=True";

                sqlConn3.Open();

                for (int i = 0; i < Players.Count; i++)
                {

                    player_id = Players[i].Id;

                    String sql_m_id = "SELECT COUNT(*) FROM `" + current_league_name + "_stats` WHERE `player_id`=" + player_id + "; ";

                    sqlCmd = new MySqlCommand(sql_m_id, sqlConn3);
                    sqlRdHS1 = sqlCmd.ExecuteReader();

                    while (sqlRdHS1.Read())
                    {

                    }

                    gamecount = Int32.Parse(sqlRdHS1[0].ToString());
                    sqlRdHS1.Close();

                    String sql_photo = "SELECT `photo` FROM `players` WHERE `player_id`=" + player_id + "; ";

                    sqlCmd = new MySqlCommand(sql_photo, sqlConn3);
                    sqlRdHS1 = sqlCmd.ExecuteReader();

                    while (sqlRdHS1.Read())
                    {

                    }

                    photo = sqlRdHS1[0].ToString();
                    sqlRdHS1.Close();

                    String sql_team = "SELECT `team` FROM `players` WHERE `player_id`=" + player_id + "; ";

                    sqlCmd = new MySqlCommand(sql_team, sqlConn3);
                    sqlRdHS1 = sqlCmd.ExecuteReader();

                    while (sqlRdHS1.Read())
                    {

                    }

                    team_id = Int32.Parse(sqlRdHS1[0].ToString());
                    sqlRdHS1.Close();

                    String sql_teamname = "SELECT `name` FROM `teams` WHERE `team_id`=" + team_id + "; ";

                    sqlCmd = new MySqlCommand(sql_teamname, sqlConn3);
                    sqlRdHS1 = sqlCmd.ExecuteReader();

                    while (sqlRdHS1.Read())
                    {

                    }

                    team_name = sqlRdHS1[0].ToString();
                    sqlRdHS1.Close();

                    if (gamecount == 0) continue;

                    for (int j = 0; j < gamecount; j++)
                    {

                        String sql_match = "SELECT match_id FROM `" + current_league_name + "_stats` WHERE `player_id`=" + player_id + " LIMIT " + j + ",1; ";

                        sqlCmd = new MySqlCommand(sql_match, sqlConn3);
                        sqlRdHS1 = sqlCmd.ExecuteReader();

                        while (sqlRdHS1.Read())
                        {

                        }

                        match_id = Int32.Parse(sqlRdHS1[0].ToString());
                        sqlRdHS1.Close();

                        String sql_points = "SELECT points FROM `" + current_league_name + "_stats` WHERE `player_id`=" + player_id + " LIMIT " + j + ",1; ";

                        sqlCmd = new MySqlCommand(sql_points, sqlConn3);
                        sqlRdHS1 = sqlCmd.ExecuteReader();

                        while (sqlRdHS1.Read())
                        {

                        }

                        total_points = total_points + Int32.Parse(sqlRdHS1[0].ToString());
                        sqlRdHS1.Close();

                        String sql_rebounds = "SELECT rebounds FROM `" + current_league_name + "_stats` WHERE `player_id`=" + player_id + " LIMIT " + j + ",1; ";

                        sqlCmd = new MySqlCommand(sql_rebounds, sqlConn3);
                        sqlRdHS1 = sqlCmd.ExecuteReader();

                        while (sqlRdHS1.Read())
                        {

                        }

                        total_rebounds = total_rebounds + Int32.Parse(sqlRdHS1[0].ToString());
                        sqlRdHS1.Close();

                        String sql_assists = "SELECT assists FROM `" + current_league_name + "_stats` WHERE `player_id`=" + player_id + " LIMIT " + j + ",1; ";

                        sqlCmd = new MySqlCommand(sql_assists, sqlConn3);
                        sqlRdHS1 = sqlCmd.ExecuteReader();

                        while (sqlRdHS1.Read())
                        {

                        }

                        total_assists = total_assists + Int32.Parse(sqlRdHS1[0].ToString());
                        sqlRdHS1.Close();

                        String sql_steals = "SELECT steals FROM `" + current_league_name + "_stats` WHERE `player_id`=" + player_id + " LIMIT " + j + ",1; ";

                        sqlCmd = new MySqlCommand(sql_steals, sqlConn3);
                        sqlRdHS1 = sqlCmd.ExecuteReader();

                        while (sqlRdHS1.Read())
                        {

                        }

                        total_steals = total_steals + Int32.Parse(sqlRdHS1[0].ToString());
                        sqlRdHS1.Close();

                        String sql_blocks = "SELECT blocks FROM `" + current_league_name + "_stats` WHERE `player_id`=" + player_id + " LIMIT " + j + ",1; ";

                        sqlCmd = new MySqlCommand(sql_blocks, sqlConn3);
                        sqlRdHS1 = sqlCmd.ExecuteReader();

                        while (sqlRdHS1.Read())
                        {

                        }

                        total_blocks = total_blocks + Int32.Parse(sqlRdHS1[0].ToString());
                        sqlRdHS1.Close();

                        String sql_tmade = "SELECT threesmade FROM `" + current_league_name + "_stats` WHERE `player_id`=" + player_id + " LIMIT " + j + ",1; ";

                        sqlCmd = new MySqlCommand(sql_tmade, sqlConn3);
                        sqlRdHS1 = sqlCmd.ExecuteReader();

                        while (sqlRdHS1.Read())
                        {

                        }

                        total_threesmade = total_threesmade + Int32.Parse(sqlRdHS1[0].ToString());
                        sqlRdHS1.Close();

                        String sql_tatt = "SELECT threesattempted FROM `" + current_league_name + "_stats` WHERE `player_id`=" + player_id + " LIMIT " + j + ",1; ";

                        sqlCmd = new MySqlCommand(sql_tatt, sqlConn3);
                        sqlRdHS1 = sqlCmd.ExecuteReader();

                        while (sqlRdHS1.Read())
                        {

                        }

                        total_threesatt = total_threesatt + Int32.Parse(sqlRdHS1[0].ToString());
                        sqlRdHS1.Close();

                    }

                    average_points = total_points / gamecount;
                    average_rebounds = total_rebounds / gamecount;
                    average_assists = total_assists / gamecount;
                    average_steals = total_steals / gamecount;
                    average_blocks = total_blocks / gamecount;
                    average_threesmade = total_threesmade / gamecount;
                    average_threesatt = total_threesatt / gamecount;

                    double threes_perc;

                    if (average_threesatt == 0) threes_perc = 0;
                    else threes_perc = (average_threesmade * 100) / average_threesatt;

                    Playerstats.Add(new PlayerStatLeader(player_id, match_id, Players[i].FirstName, Players[i].LastName, average_points, average_rebounds, average_assists, average_steals, average_blocks, average_threesmade, average_threesatt, Math.Round(threes_perc, 2), photo, team_name));


                    total_points = 0;
                    average_points = 0;
                    total_rebounds = 0;
                    average_rebounds = 0;
                    total_assists = 0;
                    average_assists = 0;
                    total_steals = 0;
                    average_steals = 0;
                    total_blocks = 0;
                    average_blocks = 0;
                    total_threesmade = 0;
                    average_threesmade = 0;
                    total_threesatt = 0;
                    average_threesatt = 0;
                    threes_perc = 0;

                }

                sqlConn3.Close();

            }
            catch (Exception ex)
            {
                //Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }

        }

        [RelayCommand]
        Task OrderByName()
        {
            List<PlayerStatLeader> list = Playerstats.ToList();
            list.Sort(CompareTeamsName);
            Playerstats = new ObservableCollection<PlayerStatLeader>(list);
            return Task.CompletedTask;
        }

        [RelayCommand]
        Task OrderByPoints()
        {
            List<PlayerStatLeader> list = Playerstats.ToList();
            list.Sort(CompareTeamsPoints);
            Playerstats = new ObservableCollection<PlayerStatLeader>(list);
            return Task.CompletedTask;
        }

        [RelayCommand]
        Task OrderByAssists()
        {
            List<PlayerStatLeader> list = Playerstats.ToList();
            list.Sort(CompareTeamsAssists);
            Playerstats = new ObservableCollection<PlayerStatLeader>(list);
            return Task.CompletedTask;
        }

        [RelayCommand]
        Task OrderByRebounds()
        {
            List<PlayerStatLeader> list = Playerstats.ToList();
            list.Sort(CompareTeamsRebounds);
            Playerstats = new ObservableCollection<PlayerStatLeader>(list);
            return Task.CompletedTask;
        }
        [RelayCommand]
        Task OrderBySteals()
        {
            List<PlayerStatLeader> list = Playerstats.ToList();
            list.Sort(CompareTeamsSteals);
            Playerstats = new ObservableCollection<PlayerStatLeader>(list);
            return Task.CompletedTask;
        }

        [RelayCommand]
        Task OrderByBlocks()
        {
            List<PlayerStatLeader> list = Playerstats.ToList();
            list.Sort(CompareTeamsBlocks);
            Playerstats = new ObservableCollection<PlayerStatLeader>(list);
            return Task.CompletedTask;
        }

        [RelayCommand]
        Task OrderByThrees()
        {
            List<PlayerStatLeader> list = Playerstats.ToList();
            list.Sort(CompareTeamsThrees);
            Playerstats = new ObservableCollection<PlayerStatLeader>(list);
            return Task.CompletedTask;
        }

        private static int CompareTeamsName(PlayerStatLeader b, PlayerStatLeader a)
        {
            if (a.LastName.CompareTo(b.LastName)<0) return 1;
            else return -1;
        }

        private static int CompareTeamsPoints(PlayerStatLeader b, PlayerStatLeader a)
        {
            if (a.Points > b.Points) return 1;
            else return -1;
        }

        private static int CompareTeamsAssists(PlayerStatLeader b, PlayerStatLeader a)
        {
            if (a.Assists > b.Assists) return 1;
            else return -1;
        }

        private static int CompareTeamsRebounds(PlayerStatLeader b, PlayerStatLeader a)
        {
            if (a.Rebounds > b.Rebounds) return 1;
            else return -1;
        }

        private static int CompareTeamsSteals(PlayerStatLeader b, PlayerStatLeader a)
        {
            if (a.Steals > b.Steals) return 1;
            else return -1;
        }

        private static int CompareTeamsBlocks(PlayerStatLeader b, PlayerStatLeader a)
        {
            if (a.Blocks > b.Blocks) return 1;
            else return -1;
        }

        private static int CompareTeamsThrees(PlayerStatLeader b, PlayerStatLeader a)
        {
            if (a.ThreesPercentage > b.ThreesPercentage) return 1;
            else return -1;
        }

    }
}
