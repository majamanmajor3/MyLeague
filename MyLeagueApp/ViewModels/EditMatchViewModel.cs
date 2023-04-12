using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyLeagueApp.Classes;
using MyLeagueApp.Classes.Stats;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeagueApp.ViewModels
{
    partial class EditMatchViewModel : ObservableObject
    {
        MySqlConnection sqlConn2 = new MySqlConnection();
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
        MySqlDataReader sqlRdDate;

        String current_league_name;
        int current_match_id;
        string date;
        int hometeam;
        int awayteam;

        String server = "localhost";
        String username = "root";
        String password = "";
        String database = "myleague";

        [ObservableProperty]
        ObservableCollection<PlayerStat> playerStats;

        [ObservableProperty]
        ObservableCollection<Player> players;

        //[ObservableProperty]
        //private string fullName;

        //[ObservableProperty]
        //private int points;

        //[ObservableProperty]
        //private int rebounds;

        //[ObservableProperty]
        //private int assists;

        //[ObservableProperty]
        //private int steals;

        //[ObservableProperty]
        //private int blocks;

        //[ObservableProperty]
        //private int threesmade;

        //[ObservableProperty]
        //private int threesattempted;

        private int homeScore;
        private int awayScore;
        private string homename;
        private string awayname;

        public int HomeScore
        {
            get { return homeScore; }
            set { homeScore = value; }
        }

        public int AwayScore
        {
            get { return awayScore; }
            set { awayScore = value; }
        }

        public string Homename
        {
            get { return homename; }
            set { homename = value; }
        }

        public string Awayname
        {
            get { return awayname; }
            set { awayname = value; }
        }


        public EditMatchViewModel(string league_name, int match_id)
        {
            Players = new ObservableCollection<Player>();
            PlayerStats = new ObservableCollection<PlayerStat>();
            current_match_id = match_id;
            current_league_name = league_name;
            loadSelections();
            loadPlayers(hometeam);
            loadPlayers(awayteam);
        }

        private void loadSelections()
        {
            try
            {

                int home_team;
                int away_team;
                int home_score;
                int away_score;

                sqlConn2.ConnectionString = "server=" + server + ";user id=" + username +
                                            ";password=" + password +
                                            ";database=" + database +
                                            ";convert zero datetime=True";

                sqlConn2.Open();

                String sql1 = "SELECT home_team FROM `" + current_league_name + "` WHERE `m_id` = " + current_match_id + "; ";

                sqlCmd = new MySqlCommand(sql1, sqlConn2);
                sqlRd = sqlCmd.ExecuteReader();

                while (sqlRd.Read())
                {

                }

                home_team = Int32.Parse(sqlRd[0].ToString());
                hometeam = home_team;
                sqlRd.Close();

                String sql2 = "SELECT away_team FROM `" + current_league_name + "` WHERE `m_id` = " + current_match_id + "; ";

                sqlCmd = new MySqlCommand(sql2, sqlConn2);
                sqlRd2 = sqlCmd.ExecuteReader();

                while (sqlRd2.Read())
                {

                }

                away_team = Int32.Parse(sqlRd2[0].ToString());
                awayteam = away_team;
                sqlRd2.Close();

                String sql3 = "SELECT home_score FROM `" + current_league_name + "` WHERE `m_id` = " + current_match_id + "; ";

                sqlCmd = new MySqlCommand(sql3, sqlConn2);
                sqlRd3 = sqlCmd.ExecuteReader();

                while (sqlRd3.Read())
                {

                }

                home_score = Int32.Parse(sqlRd3[0].ToString());
                sqlRd3.Close();

                String sql4 = "SELECT away_score FROM `" + current_league_name + "` WHERE `m_id` = " + current_match_id + "; ";

                sqlCmd = new MySqlCommand(sql4, sqlConn2);
                sqlRd4 = sqlCmd.ExecuteReader();

                while (sqlRd4.Read())
                {

                }

                away_score = Int32.Parse(sqlRd4[0].ToString());
                sqlRd4.Close();

                String sql5 = "SELECT date FROM `" + current_league_name + "` WHERE `m_id` = " + current_match_id + "; ";

                sqlCmd = new MySqlCommand(sql5, sqlConn2);
                sqlRdDate = sqlCmd.ExecuteReader();

                while (sqlRdDate.Read())
                {

                }

                date = sqlRdDate[0].ToString();
                sqlRdDate.Close();

                String sql_h_name = "SELECT t.name FROM `" + current_league_name + "` l JOIN teams t ON (l.home_team = t.team_id) WHERE l.`m_id` = " + current_match_id + "; ";

                sqlCmd = new MySqlCommand(sql_h_name, sqlConn2);
                sqlRd7 = sqlCmd.ExecuteReader();

                while (sqlRd7.Read())
                {

                }

                string home = sqlRd7[0].ToString();
                homename = "Set " + home + " final score"; ;
                sqlRd7.Close();

                String sql_a_name = "SELECT t.name FROM `" + current_league_name + "` l JOIN teams t ON (l.away_team = t.team_id) WHERE l.`m_id` = " + current_match_id + "; ";

                sqlCmd = new MySqlCommand(sql_a_name, sqlConn2);
                sqlRd8 = sqlCmd.ExecuteReader();

                while (sqlRd8.Read())
                {

                }

                string away = sqlRd8[0].ToString();
                awayname = "Set " + away + " final score";
                sqlRd8.Close();

                sqlConn2.Close();

                //home_picker.SelectedItem = teams[home_team];
                //away_picker.SelectedItem = teams[away_team];
                hometeam = home_team;
                awayteam = away_team;
                homeScore = home_score;
                awayScore = away_score;


            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                sqlConn2.Close();
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
                string photo;
                int position;

                sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                            ";password=" + password +
                                            ";database=" + database +
                                            ";convert zero datetime=True";

                sqlConn.Open();

                String sql = "SELECT COUNT(*) FROM `players` WHERE `team`=" + team_id + "; ";

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

                    String sql_m_id = "SELECT player_id FROM players WHERE `team`=" + team_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_m_id, sqlConn);
                    sqlRd = sqlCmd.ExecuteReader();

                    while (sqlRd.Read())
                    {

                    }

                    player_id = Int32.Parse(sqlRd[0].ToString());
                    sqlRd.Close();

                    String sql_h_id = "SELECT first_name FROM players WHERE `team`=" + team_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_h_id, sqlConn);
                    sqlRd2 = sqlCmd.ExecuteReader();

                    while (sqlRd2.Read())
                    {

                    }

                    first_name = sqlRd2[0].ToString();
                    sqlRd2.Close();

                    String sql_a_id = "SELECT last_name FROM players WHERE `team`=" + team_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_a_id, sqlConn);
                    sqlRd4 = sqlCmd.ExecuteReader();

                    while (sqlRd4.Read())
                    {

                    }

                    last_name = sqlRd4[0].ToString();
                    sqlRd4.Close();

                    String sql_h_score = "SELECT photo FROM players WHERE `team`=" + team_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_h_score, sqlConn);
                    sqlRd5 = sqlCmd.ExecuteReader();

                    while (sqlRd5.Read())
                    {

                    }

                    photo = sqlRd5[0].ToString();
                    sqlRd5.Close();

                    String sql_pos = "SELECT position FROM players WHERE `team`=" + team_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_pos, sqlConn);
                    sqlRd6 = sqlCmd.ExecuteReader();

                    while (sqlRd6.Read())
                    {

                    }

                    position = Int32.Parse(sqlRd6[0].ToString());
                    sqlRd6.Close();

                    String sql_valami = "SELECT COUNT(*) FROM `" + current_league_name + "_stats` WHERE player_id=" + player_id + " AND match_id=" + current_match_id + "; ";

                    sqlCmd = new MySqlCommand(sql_valami, sqlConn);
                    sqlRd = sqlCmd.ExecuteReader();

                    while (sqlRd.Read())
                    {

                    }

                    int statCheck = Int32.Parse(sqlRd[0].ToString());
                    sqlRd.Close();

                    sqlConn.Close();

                    string full_name = first_name + " " + last_name;

                    string pos_name = "NA";

                    if (position == 1) pos_name = "Point Guard";
                    if (position == 2) pos_name = "Shooting Guard";
                    if (position == 3) pos_name = "Small Forward";
                    if (position == 4) pos_name = "Power Forward";
                    if (position == 5) pos_name = "Center";

                    Players.Add(new Player(player_id, first_name, last_name, team_id, photo, position, pos_name, full_name));

                    if (statCheck == 0)
                    {
                        PlayerStats.Add(new PlayerStat(player_id, current_match_id, first_name, last_name, 0, 0, 0, 0, 0, 0, 0));
                    }
                    else
                    {
                        sqlConn.Open();

                        String sql_points = "SELECT points FROM `" + current_league_name + "_stats` WHERE player_id=" + player_id + " AND match_id=" + current_match_id + "; ";

                        sqlCmd = new MySqlCommand(sql_points, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        int points = Int32.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_rebounds = "SELECT rebounds FROM `" + current_league_name + "_stats` WHERE player_id=" + player_id + " AND match_id=" + current_match_id + "; ";

                        sqlCmd = new MySqlCommand(sql_rebounds, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        int rebounds = Int32.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_assists = "SELECT assists FROM `" + current_league_name + "_stats` WHERE player_id=" + player_id + " AND match_id=" + current_match_id + "; ";

                        sqlCmd = new MySqlCommand(sql_assists, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        int assists = Int32.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_steals = "SELECT steals FROM `" + current_league_name + "_stats` WHERE player_id=" + player_id + " AND match_id=" + current_match_id + "; ";

                        sqlCmd = new MySqlCommand(sql_steals, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        int steals = Int32.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_blocks = "SELECT blocks FROM `" + current_league_name + "_stats` WHERE player_id=" + player_id + " AND match_id=" + current_match_id + "; ";

                        sqlCmd = new MySqlCommand(sql_blocks, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        int blocks = Int32.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_3smade = "SELECT threesmade FROM `" + current_league_name + "_stats` WHERE player_id=" + player_id + " AND match_id=" + current_match_id + "; ";

                        sqlCmd = new MySqlCommand(sql_3smade, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        int threesMade = Int32.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_3satt = "SELECT threesattempted FROM `" + current_league_name + "_stats` WHERE player_id=" + player_id + " AND match_id=" + current_match_id + "; ";

                        sqlCmd = new MySqlCommand(sql_3satt, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        int threesAttempted = Int32.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        sqlConn.Close();

                        PlayerStats.Add(new PlayerStat(player_id, current_match_id, first_name, last_name, points, rebounds, assists, steals, blocks, threesMade, threesAttempted));
                    }

                }
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }
        }

        [RelayCommand]
        private void ConfirmMatchClicked()
        {
            //if (
            //    //home_picker == null || away_picker == null || 
            //    homeScore == null || awayScore == null)
            //{
            //    DisplayAlert("ATTENTION", "You haven't picked all data for the match yet!", "OK");
            //}
            //else
            //{
            try
            {

                int match_id;
                int home_team;
                int away_team;

                sqlConn2.Close();

                sqlConn2.ConnectionString = "server=" + server + ";user id=" + username +
                                            ";password=" + password +
                                            ";database=" + database +
                                            ";convert zero datetime=True";

                sqlConn2.Open();

                String sql = "SELECT COUNT(*)+1 FROM `" + current_league_name + "`; ";

                sqlCmd = new MySqlCommand(sql, sqlConn2);
                sqlRd = sqlCmd.ExecuteReader();

                while (sqlRd.Read())
                {

                }

                match_id = Int32.Parse(sqlRd[0].ToString());
                sqlRd.Close();

                //int home_index = home_picker.SelectedIndex;
                //home_team = teams[home_index].Id;

                //int away_index = away_picker.SelectedIndex;
                //away_team = teams[away_index].Id;

                home_team = hometeam;
                away_team = awayteam;

                String sql2 = "UPDATE `" + current_league_name + "` SET `home_score` = '" + homeScore + "', `away_score` = '" + awayScore + "' WHERE `" + current_league_name + "`.`m_id` = " + current_match_id + ";";

                sqlCmd = new MySqlCommand(sql2, sqlConn2);

                sqlRd2 = sqlCmd.ExecuteReader();

                while (sqlRd2.Read())
                {

                }

                sqlRd2.Close();

                sqlConn2.Close();

                sqlConn2.Open();

                for(int i = 0; i < Players.Count; i++)
                {

                    Player player = Players[i];
                    PlayerStat stat = PlayerStats[i];

                    String sql_valami = "SELECT COUNT(*) FROM `" + current_league_name + "_stats` WHERE player_id=" + player.Id + " AND match_id=" + current_match_id + "; ";

                    sqlCmd = new MySqlCommand(sql_valami, sqlConn2);
                    sqlRd = sqlCmd.ExecuteReader();

                    while (sqlRd.Read())
                    {

                    }

                    int statCheck = Int32.Parse(sqlRd[0].ToString());
                    sqlRd.Close();

                    if(statCheck == 0)
                    {


                        String sql_id = "SELECT COUNT(*)+1 FROM `" + current_league_name + "_stats`; ";

                        sqlCmd = new MySqlCommand(sql_id, sqlConn2);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        int stat_id = Int32.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        //FullName = player.FullName;

                        String sql_insert = "INSERT INTO `" + current_league_name + "_stats` (`stat_id`, `player_id`, `match_id`, `first_name`, `last_name`, `points`, `rebounds`, `steals`, `blocks`, `threesmade`, `threesattempted`, `assists`) " +
                            "VALUES (" + stat_id + ", '" + player.Id + "', '" + current_match_id + "', '" + player.FirstName + "', '" + player.LastName + "', '" + stat.Points + "', '" + stat.Rebounds + "', '" + stat.Steals + "', '" + stat.Blocks + "', '" + stat.ThreesMade + "', '" + stat.ThreesAttempted + "', '" + stat.Assists + "');";

                        sqlCmd = new MySqlCommand(sql_insert, sqlConn2);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        statCheck = Int32.Parse(sqlRd[0].ToString());
                        sqlRd.Close();
                    }
                    else
                    {

                        String sqlupdate = "UPDATE `" + current_league_name + "_stats` SET `points` = '" + stat.Points + 
                            "', `rebounds` = '" + stat.Rebounds + 
                            "', `assists` = '" + stat.Assists + 
                            "', `steals` = '" + stat.Steals + 
                            "', `blocks` = '" + stat.Blocks + 
                            "', `threesmade` = '" + stat.ThreesMade + 
                            "', `threesattempted` = '" + stat.ThreesAttempted + 
                            "' WHERE `" + current_league_name + "_stats`.`player_id` = " + player.Id +
                            " AND `" + current_league_name + "_stats`.`match_id` = " + current_match_id + ";";

                        sqlCmd = new MySqlCommand(sqlupdate, sqlConn2);

                        sqlRd2 = sqlCmd.ExecuteReader();

                        while (sqlRd2.Read())
                        {

                        }

                        sqlRd2.Close();

                    }

                }

                sqlConn.Close();

                Application.Current.MainPage.DisplayAlert("", "Your match has been edited succesfully!", "OK");

                Shell.Current.GoToAsync(nameof(LeaguesPage));
                // oldd meg szebben !!!

            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                sqlConn2.Close();
            }
        }
    }
}
