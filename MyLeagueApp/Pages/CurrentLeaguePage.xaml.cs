using MyLeagueApp.Classes;
using MyLeagueApp.ViewModels;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.IO;
using System.Data;

namespace MyLeagueApp;

public partial class CurrentLeaguePage : ContentPage
{
    MySqlConnection sqlConn = new MySqlConnection();
    MySqlCommand sqlCmd = new MySqlCommand();
    DataTable sqlDt = new DataTable();
    String sqlQuery;
    MySqlDataAdapter DtA = new MySqlDataAdapter();
    MySqlDataReader sqlRd;
    MySqlDataReader sqlRd2;
    MySqlDataReader sqlRd3;
    MySqlDataReader sqlRd4;
    MySqlDataReader sqlRd5;
    MySqlDataReader sqlRd6;
    MySqlDataReader sqlRd7;
    MySqlDataReader sqlRd8;
    MySqlDataReader sqlRd9;
    MySqlDataReader sqlRd10;
    MySqlDataReader sqlRd11;
    MySqlDataReader sqlRd12;
    MySqlDataReader sqlRdDate;
    DataSet ds = new DataSet();

    String current_league_name;

    String server = "localhost";
    String username = "root";
    String password = "";
    String database = "myleague";

    List<Match> matches;
    public CurrentLeaguePage(string league_name)
	{
		InitializeComponent();
        current_league_name = league_name;
        BindingContext = new CurrentLeagueViewModel(league_name);
        //matches = new List<Match>();
        //loadMatches(league_name);
        //cv.ItemsSource = matches;
    }

    private void loadMatches(string league_name)
    {
        try
        {
            string match_count = "0";

            string match_id;
            int home_id;
            int away_id;
            int home_score;
            int away_score;
            string date;

            string home_name;
            string away_name;
            string home_city;
            string away_city;
            string home_logo;
            string away_logo;

            sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                        ";password=" + password +
                                        ";database=" + database +
                                        ";convert zero datetime=True";

            sqlConn.Open();

            String sql = "SELECT COUNT(*) FROM `" + league_name + "`; ";

            sqlCmd = new MySqlCommand(sql, sqlConn);
            sqlRd3 = sqlCmd.ExecuteReader();

            while (sqlRd3.Read())
            {

            }

            match_count = sqlRd3[0].ToString();
            sqlRd3.Close();
            sqlConn.Close();

            int cr = Int32.Parse(match_count);

            if (cr == 0)
            {
                label.IsVisible = true;
            }

            for (int i = 0; i < cr; i++)
            {
                sqlConn.Open();

                String sql_m_id = "SELECT m_id FROM `" + league_name + "` WHERE 1 LIMIT " + i + ",1; ";

                sqlCmd = new MySqlCommand(sql_m_id, sqlConn);
                sqlRd = sqlCmd.ExecuteReader();

                while (sqlRd.Read())
                {

                }

                match_id = sqlRd[0].ToString();
                sqlRd.Close();

                String sql_h_id = "SELECT home_team FROM `" + league_name + "` WHERE 1 LIMIT " + i + ",1; ";

                sqlCmd = new MySqlCommand(sql_h_id, sqlConn);
                sqlRd2 = sqlCmd.ExecuteReader();

                while (sqlRd2.Read())
                {

                }

                home_id = Int32.Parse(sqlRd2[0].ToString());
                sqlRd2.Close();

                String sql_a_id = "SELECT away_team FROM `" + league_name + "` WHERE 1 LIMIT " + i + ",1; ";

                sqlCmd = new MySqlCommand(sql_a_id, sqlConn);
                sqlRd4 = sqlCmd.ExecuteReader();

                while (sqlRd4.Read())
                {

                }

                away_id = Int32.Parse(sqlRd4[0].ToString());
                sqlRd4.Close();

                String sql_h_score = "SELECT home_score FROM `" + league_name + "` WHERE 1 LIMIT " + i + ",1; ";

                sqlCmd = new MySqlCommand(sql_h_score, sqlConn);
                sqlRd5 = sqlCmd.ExecuteReader();

                while (sqlRd5.Read())
                {

                }

                home_score = Int32.Parse(sqlRd5[0].ToString());
                sqlRd5.Close();

                String sql_a_score = "SELECT away_score FROM `" + league_name + "` WHERE 1 LIMIT " + i + ",1; ";

                sqlCmd = new MySqlCommand(sql_a_score, sqlConn);
                sqlRd6 = sqlCmd.ExecuteReader();

                while (sqlRd6.Read())
                {

                }

                away_score = Int32.Parse(sqlRd6[0].ToString());
                sqlRd6.Close();

                String sql_date = "SELECT date FROM `" + league_name + "` WHERE 1 LIMIT " + i + ",1; ";

                sqlCmd = new MySqlCommand(sql_date, sqlConn);
                sqlRdDate = sqlCmd.ExecuteReader();

                while (sqlRdDate.Read())
                {

                }

                date = sqlRdDate[0].ToString();
                sqlRdDate.Close();

                String sql_h_name = "SELECT t.name FROM `" + league_name + "`l JOIN teams t ON (l.home_team = t.team_id) WHERE 1 LIMIT " + i + ",1; ";

                sqlCmd = new MySqlCommand(sql_h_name, sqlConn);
                sqlRd7 = sqlCmd.ExecuteReader();

                while (sqlRd7.Read())
                {

                }

                home_name = sqlRd7[0].ToString();
                sqlRd7.Close();

                String sql_a_name = "SELECT t.name FROM `" + league_name + "`l JOIN teams t ON (l.away_team = t.team_id) WHERE 1 LIMIT " + i + ",1; ";

                sqlCmd = new MySqlCommand(sql_a_name, sqlConn);
                sqlRd8 = sqlCmd.ExecuteReader();

                while (sqlRd8.Read())
                {

                }

                away_name = sqlRd8[0].ToString();
                sqlRd8.Close();

                String sql_h_city = "SELECT t.city FROM `" + league_name + "`l JOIN teams t ON (l.home_team = t.team_id) WHERE 1 LIMIT " + i + ",1; ";

                sqlCmd = new MySqlCommand(sql_h_city, sqlConn);
                sqlRd9 = sqlCmd.ExecuteReader();

                while (sqlRd9.Read())
                {

                }

                home_city = sqlRd9[0].ToString();
                sqlRd9.Close();

                String sql_a_city = "SELECT t.city FROM `" + league_name + "`l JOIN teams t ON (l.away_team = t.team_id) WHERE 1 LIMIT " + i + ",1; ";

                sqlCmd = new MySqlCommand(sql_a_city, sqlConn);
                sqlRd10 = sqlCmd.ExecuteReader();

                while (sqlRd10.Read())
                {

                }

                away_city = sqlRd10[0].ToString();
                sqlRd10.Close();

                String sql_h_logo = "SELECT t.logo FROM `" + league_name + "`l JOIN teams t ON (l.home_team = t.team_id) WHERE 1 LIMIT " + i + ",1; ";

                sqlCmd = new MySqlCommand(sql_h_logo, sqlConn);
                sqlRd11 = sqlCmd.ExecuteReader();

                while (sqlRd11.Read())
                {

                }

                home_logo = sqlRd11[0].ToString();
                sqlRd11.Close();

                String sql_a_logo = "SELECT t.logo FROM `" + league_name + "`l JOIN teams t ON (l.away_team = t.team_id) WHERE 1 LIMIT " + i + ",1; ";

                sqlCmd = new MySqlCommand(sql_a_logo, sqlConn);
                sqlRd12 = sqlCmd.ExecuteReader();

                while (sqlRd12.Read())
                {

                }

                away_logo = sqlRd12[0].ToString();
                sqlRd12.Close();

                sqlConn.Close();

                matches.Add(new Match(Int32.Parse(match_id), home_name, away_name, home_city, away_city, home_score, away_score, home_logo, away_logo, date));
            }
        }
        catch (Exception ex)
        {
            //DisplayAlert("", ex.Message, "OK");
        }
    }
    private void NewMatchClicked(object sender, EventArgs e)
    {
        NewMatchPage page = new NewMatchPage(current_league_name);
        Navigation.PushAsync(page);
    }

    private void StatsClicked(object sender, EventArgs e)
    {
        Statistics page = new Statistics(current_league_name);
        Navigation.PushAsync(page);
    }

    void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
    {
        Match item = args.SelectedItem as Match;
        EditMatchPage page = new EditMatchPage(current_league_name, item.Id);
        Navigation.PushAsync(page);
    }
}