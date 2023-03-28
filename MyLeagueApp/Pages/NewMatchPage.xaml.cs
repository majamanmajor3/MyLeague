using MyLeagueApp.Classes;
using MyLeagueApp.ViewModels;
using MySql.Data.MySqlClient;
using System.Data;

namespace MyLeagueApp;

public partial class NewMatchPage : ContentPage
{
    MySqlConnection sqlConn = new MySqlConnection();
    MySqlCommand sqlCmd = new MySqlCommand();
    DataTable sqlDt = new DataTable();
    String sqlQuery;
    MySqlDataAdapter DtA = new MySqlDataAdapter();
    MySqlDataReader sqlRd;
    MySqlDataReader sqlRdC;
    MySqlDataReader sqlRd2;
    MySqlDataReader sqlRd3;
    MySqlDataReader sqlRdDF;
    MySqlDataReader sqlRdDate;
    DataSet ds = new DataSet();

    String current_league_name;

    String server = "localhost";
    String username = "root";
    String password = "";
    String database = "myleague";

    List<Team> teams;
    public NewMatchPage(string league_name)
    {
        InitializeComponent();
        current_league_name = league_name;
        BindingContext = new NewMatchViewModel(league_name);
        //teams = new List<Team>();
        //loadTeams();
        //home_picker.ItemsSource = teams;
        //away_picker.ItemsSource = teams;
        //game_date.Date = DateTime.Now;
    }

    private void loadTeams()
    {
        try
        {

            int team_count;
            string team_id;
            string team_name;
            string team_city;
            string team_logo;

            sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                        ";password=" + password +
                                        ";database=" + database +
                                        ";convert zero datetime=True";

            sqlConn.Open();

            String sqlc = "SELECT COUNT(*) FROM `teams`; ";

            sqlCmd = new MySqlCommand(sqlc, sqlConn);
            sqlRdC = sqlCmd.ExecuteReader();

            while (sqlRdC.Read())
            {

            }

            team_count = Int32.Parse(sqlRdC[0].ToString());
            sqlRdC.Close();

            sqlConn.Close();

            for (int i = 0; i < team_count; i++)
            {

                sqlConn.Open();

                String sql = "SELECT team_id FROM `teams` WHERE 1 LIMIT " + i + ",1; ";

                sqlCmd = new MySqlCommand(sql, sqlConn);
                sqlRd = sqlCmd.ExecuteReader();

                while (sqlRd.Read())
                {

                }

                team_id = sqlRd[0].ToString();
                sqlRd.Close();

                String sql2 = "SELECT name FROM `teams` WHERE 1 LIMIT " + i + ",1; ";

                sqlCmd = new MySqlCommand(sql2, sqlConn);
                sqlRd2 = sqlCmd.ExecuteReader();

                while (sqlRd2.Read())
                {

                }

                team_name = sqlRd2[0].ToString();
                sqlRd2.Close();

                String df_sql = "SELECT city FROM `teams` WHERE 1 LIMIT " + i + ",1; ";

                sqlCmd = new MySqlCommand(df_sql, sqlConn);
                sqlRdDF = sqlCmd.ExecuteReader();

                while (sqlRdDF.Read())
                {

                }

                team_city = sqlRdDF[0].ToString();

                sqlRdDF.Close();

                String sqlLogo = "SELECT logo FROM `teams` WHERE 1 LIMIT " + i + ",1; ";

                sqlCmd = new MySqlCommand(sqlLogo, sqlConn);
                sqlRd3 = sqlCmd.ExecuteReader();

                while (sqlRd3.Read())
                {

                }

                team_logo = sqlRd3[0].ToString();

                sqlRd3.Close();

                sqlConn.Close();

                teams.Add(new Team(Int32.Parse(team_id), team_name, team_city, team_logo));

            }

        }
        catch (Exception ex)
        {
            //DisplayAlert("", ex.Message, "OK");
        }

    }

    private void AddMatchClicked(object sender, EventArgs e)
    {
        if (home_picker == null || away_picker == null || game_date == null)
        {
            DisplayAlert("ATTENTION", "You haven't picked all data for the match yet!", "OK");
        }
        else
        {
            try
            {

                int match_id;
                int home_team;
                int away_team;
                DateTime date;

                sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                            ";password=" + password +
                                            ";database=" + database +
                                            ";convert zero datetime=True";

                sqlConn.Open();

                String sql = "SELECT COUNT(*)+1 FROM `" + current_league_name + "`; ";

                sqlCmd = new MySqlCommand(sql, sqlConn);
                sqlRd = sqlCmd.ExecuteReader();

                while (sqlRd.Read())
                {

                }

                match_id = Int32.Parse(sqlRd[0].ToString());
                sqlRd.Close();

                int home_index = home_picker.SelectedIndex;
                home_team = teams[home_index].Id;

                int away_index = away_picker.SelectedIndex;
                away_team = teams[away_index].Id;

                date = game_date.Date;

                string selected_date = date.ToString("yyyy-MM-dd");

                String sql2 = "INSERT INTO `" + current_league_name + "` (`m_id`, `home_team`, `away_team`, `home_score`, `away_score`, `date`) " +
                    "VALUES (" + match_id + ", '" + home_team + "', '" + away_team + "', '0', '0', '" + selected_date + "');";

                sqlCmd = new MySqlCommand(sql2, sqlConn);

                sqlRd2 = sqlCmd.ExecuteReader();

                while (sqlRd2.Read())
                {

                }

                sqlRd2.Close();

                sqlConn.Close();

                Navigation.PushAsync(new CurrentLeaguePage(current_league_name));

            }
            catch (Exception ex)
            {
                DisplayAlert("", ex.Message, "OK");
            }
        }
    }
}