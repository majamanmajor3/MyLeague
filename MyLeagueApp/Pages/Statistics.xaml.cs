using MyLeagueApp.Classes;
using MyLeagueApp.ViewModels;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.IO;
using System.Data;

namespace MyLeagueApp;

public partial class Statistics : ContentPage
{
    MySqlConnection sqlConn = new MySqlConnection();
    MySqlCommand sqlCmd = new MySqlCommand();
    DataTable sqlDt = new DataTable();
    String sqlQuery;
    MySqlDataAdapter DtA = new MySqlDataAdapter();
    MySqlDataReader sqlRd;
    MySqlDataReader sqlRdA;
    MySqlDataReader sqlRdC;
    MySqlDataReader sqlRdC1;
    MySqlDataReader sqlRdC2;
    MySqlDataReader sqlRdC3;
    MySqlDataReader sqlRdC4;
    MySqlDataReader sqlRd2;
    MySqlDataReader sqlRd3;
    MySqlDataReader sqlRdDF;
    MySqlDataReader sqlRdDate;
    DataSet ds = new DataSet();

    ViewCell lastCell;

    string current_league_name;

    String server = "localhost";
    String username = "root";
    String password = "";
    String database = "myleague";
    List<Team> teams;
    public Statistics(string league_name)
    {
		InitializeComponent();
        BindingContext = new StatisticsViewModel(league_name);
        //teams = new List<Team>();
        //current_league_name = league_name;
        //loadTeams();
        //loadStats();
        //cv.ItemsSource = teams;

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

            for(int i=0; i<teams_id.Length; i++)
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
            string team_name;
            string team_logo;
            string team_city;
            int wins;
            int losses;
            int ppg;
            int appg;
            int homescore;
            int awayscore;

            int cr = 0;
            int match_count;

            sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                        ";password=" + password +
                                        ";database=" + database +
                                        ";convert zero datetime=True";

            sqlConn.Open();

            for(int i=0; i<teams.Count(); i++)
            {

                team_id = teams[i].Id;
                team_name = teams[i].Name;
                team_city = teams[i].City;
                team_logo = teams[i].Logo;

                String sql = "SELECT COUNT(*) FROM `" + current_league_name + " WHERE `home_team` = " + team_id + " OR `away_team` = " + team_id + "; ";

                sqlCmd = new MySqlCommand(sql, sqlConn);
                sqlRdC1 = sqlCmd.ExecuteReader();

                while (sqlRdC1.Read())
                {

                }

                match_count = Int32.Parse(sqlRdC1[0].ToString());
                sqlRdC1.Close();

                sqlConn.Close();

                sqlConn.Open();

                for (int j=0; j<match_count; j++)
                {

                    String sql_homescore = "SELECT home_score FROM `" + current_league_name + " WHERE `home_team` = " + team_id + " OR `away_team` = " + team_id + " LIMIT " + j + ",1; ";

                    sqlCmd = new MySqlCommand(sql_homescore, sqlConn);
                    sqlRdC2 = sqlCmd.ExecuteReader();

                    while (sqlRdC2.Read())
                    {

                    }

                    homescore = Int32.Parse(sqlRdC2[0].ToString());
                    sqlRdC2.Close();

                    String sql_awayscore = "SELECT away_score FROM `" + current_league_name + " WHERE `home_team` = " + team_id + " OR `away_team` = " + team_id + " LIMIT " + j + ",1; ";

                    sqlCmd = new MySqlCommand(sql_awayscore, sqlConn);
                    sqlRdC3 = sqlCmd.ExecuteReader();

                    while (sqlRdC3.Read())
                    {

                    }

                    awayscore = Int32.Parse(sqlRdC3[0].ToString());
                    sqlRdC3.Close();

                }

            }

        }
        catch (Exception ex)
        {
            //DisplayAlert("", ex.Message, "OK");
        }

    }
}