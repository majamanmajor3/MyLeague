using CommunityToolkit.Mvvm.ComponentModel;
using MyLeagueApp.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeagueApp.ViewModels
{
    partial class StatisticsViewModel : ObservableObject
    {
        MySqlConnection sqlConn = new MySqlConnection();
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
        MySqlDataReader sqlRdDF;
        MySqlDataReader sqlRdDate;

        ViewCell lastCell;

        string current_league_name;

        String server = "localhost";
        String username = "root";
        String password = "";
        String database = "myleague";

        [ObservableProperty]
        ObservableCollection<Team> teams;

        [ObservableProperty]
        ObservableCollection<TeamStat> teamstats;

        public StatisticsViewModel(string league_name)
        {
            Teams = new ObservableCollection<Team>();
            Teamstats = new ObservableCollection<TeamStat>();
            current_league_name = league_name;
            loadTeams();
            loadStats();

            List<TeamStat> list = Teamstats.ToList();
            list.Sort(CompareTeams);
            Teamstats = new ObservableCollection<TeamStat>(list);
        }

        private static int CompareTeams(TeamStat b, TeamStat a)
        {
            if (a.GD > b.GD) return 1;
            else
            {
                if(a.GD < b.GD) return -1;
                else
                {
                    if (a.GD == b.GD)
                    {
                        if (a.Wins > b.Wins) return 1;
                        else
                        {
                            if (a.Wins < b.Wins) return -1;
                            else
                            {
                                if (a.Wins == b.Wins)
                                {
                                    if (a.PPG > b.PPG) return 1;
                                    else
                                    {
                                        if (a.PPG < b.PPG) return -1;
                                        else return 0;
                                    }
                                }
                                else return 0;
                            }
                        }
                    }
                    else return 0;
                }
            }
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

                sqlConn.Close();

                for (int i = 0; i < match_count; i++)
                {

                    sqlConn.Open();

                    String sql_h = "SELECT home_team FROM `" + current_league_name + "` WHERE 1 LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_h, sqlConn);
                    sqlRd = sqlCmd.ExecuteReader();

                    while (sqlRd.Read())
                    {

                    }

                    home_id = Int32.Parse(sqlRd[0].ToString());
                    sqlRd.Close();

                    sqlConn.Close();

                    if (check[home_id] == 0)
                    {
                        check[home_id]++;
                        teams_id[cr] = home_id;
                        cr++;
                    }
                    else continue;

                    sqlConn.Open();

                    String sql_a = "SELECT away_team FROM `" + current_league_name + "` WHERE 1 LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_a, sqlConn);
                    sqlRdA = sqlCmd.ExecuteReader();

                    while (sqlRdA.Read())
                    {

                    }

                    away_id = Int32.Parse(sqlRdA[0].ToString());
                    sqlRdA.Close();

                    sqlConn.Close();

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
                    sqlConn.Open();

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

                    sqlConn.Close();

                    teams.Add(new Team(teams_id[i], team_name, team_city, team_logo));
                }

            }
            catch (Exception ex)
            {
                //DisplayAlert("", ex.Message, "OK");
            }

        }

        private void loadStats()
        {
            try
            {

                int team_id;
                int current_match_id;
                string team_name;
                string team_logo;
                string team_city;
                int wins;
                int losses;
                int ppg;
                int home_id;
                int away_id;
                int appg;
                int homescore;
                int awayscore;

                int cr = 0;
                int match_count;

                //sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                //                            ";password=" + password +
                //                            ";database=" + database +
                //                            ";convert zero datetime=True";

                sqlConn.Close();
                sqlConn.Open();

                for (int i = 0; i < teams.Count(); i++)
                {

                    team_id = teams[i].Id;
                    team_name = teams[i].Name;
                    team_city = teams[i].City;
                    team_logo = teams[i].Logo;

                    wins = 0;
                    losses = 0;
                    ppg = 0;
                    appg = 0;

                    String sql = "SELECT COUNT(*) FROM `" + current_league_name + "` WHERE `home_team` = " + team_id + " OR `away_team` = " + team_id + "; ";

                    sqlCmd = new MySqlCommand(sql, sqlConn);
                    sqlRdC1 = sqlCmd.ExecuteReader();

                    while (sqlRdC1.Read())
                    {

                    }

                    match_count = Int32.Parse(sqlRdC1[0].ToString());
                    sqlRdC1.Close();

                    //sqlConn.Close();

                    //sqlConn.Open();

                    for (int j = 0; j < match_count; j++)
                    {

                        String sql_match = "SELECT m_id FROM `" + current_league_name + "` WHERE `home_team` = " + team_id + " OR `away_team` = " + team_id + " LIMIT " + j + ",1; ";

                        sqlCmd = new MySqlCommand(sql_match, sqlConn);
                        sqlRdC2 = sqlCmd.ExecuteReader();

                        while (sqlRdC2.Read())
                        {

                        }

                        current_match_id = Int32.Parse(sqlRdC2[0].ToString());
                        sqlRdC2.Close();

                        String sql_current = "SELECT home_team FROM `" + current_league_name + "` WHERE `m_id` = " + current_match_id + ";";

                        sqlCmd = new MySqlCommand(sql_current, sqlConn);
                        sqlRdC3 = sqlCmd.ExecuteReader();

                        while (sqlRdC3.Read())
                        {

                        }

                        home_id = Int32.Parse(sqlRdC3[0].ToString());
                        sqlRdC3.Close();

                        String sql_currentopp = "SELECT away_team FROM `" + current_league_name + "` WHERE `m_id` = " + current_match_id + ";";

                        sqlCmd = new MySqlCommand(sql_currentopp, sqlConn);
                        sqlRdC4 = sqlCmd.ExecuteReader();

                        while (sqlRdC4.Read())
                        {

                        }

                        away_id = Int32.Parse(sqlRdC4[0].ToString());
                        sqlRdC4.Close();


                        if (team_id == home_id)
                        {
                            String sql_homes = "SELECT home_score FROM `" + current_league_name + "` WHERE `m_id` = " + current_match_id + ";";

                            sqlCmd = new MySqlCommand(sql_homes, sqlConn);
                            sqlRdC5 = sqlCmd.ExecuteReader();

                            while (sqlRdC5.Read())
                            {

                            }

                            homescore = Int32.Parse(sqlRdC5[0].ToString());
                            sqlRdC5.Close();

                            if (homescore == 0) continue;

                            String sql_aways = "SELECT away_score FROM `" + current_league_name + "` WHERE `m_id` = " + current_match_id + ";";

                            sqlCmd = new MySqlCommand(sql_aways, sqlConn);
                            sqlRdC6 = sqlCmd.ExecuteReader();

                            while (sqlRdC6.Read())
                            {

                            }

                            awayscore = Int32.Parse(sqlRdC6[0].ToString());
                            sqlRdC6.Close();

                            if (homescore > awayscore)
                            {
                                wins += 1;
                                ppg += homescore;
                                appg += awayscore;
                            }
                            else
                            {
                                losses += 1;
                                ppg += homescore;
                                appg += awayscore;
                            }
                        }

                        if (team_id == away_id)
                        {
                            String sql_homes = "SELECT home_score FROM `" + current_league_name + "` WHERE `m_id` = " + current_match_id + ";";

                            sqlCmd = new MySqlCommand(sql_homes, sqlConn);
                            sqlRdC5 = sqlCmd.ExecuteReader();

                            while (sqlRdC5.Read())
                            {

                            }

                            homescore = Int32.Parse(sqlRdC5[0].ToString());
                            sqlRdC5.Close();

                            if (homescore == 0) continue;

                            String sql_aways = "SELECT away_score FROM `" + current_league_name + "` WHERE `m_id` = " + current_match_id + ";";

                            sqlCmd = new MySqlCommand(sql_aways, sqlConn);
                            sqlRdC6 = sqlCmd.ExecuteReader();

                            while (sqlRdC6.Read())
                            {

                            }

                            awayscore = Int32.Parse(sqlRdC6[0].ToString());
                            sqlRdC6.Close();

                            if (homescore > awayscore)
                            {
                                losses += 1;
                                ppg += awayscore;
                                appg += homescore;
                            }
                            else
                            {
                                wins += 1;
                                ppg += awayscore;
                                appg += homescore;
                            }
                        }

                    }

                    //sqlConn.Close();

                    double gamediff = 0.5 * wins + (-0.5) * losses;

                    teamstats.Add(new TeamStat(team_id, team_name, team_city, team_logo, wins, losses, ppg, appg, gamediff));

                }

                sqlConn.Close();

            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }

        }
    }
}
