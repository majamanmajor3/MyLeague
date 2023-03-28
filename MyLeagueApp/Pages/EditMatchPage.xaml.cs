using MyLeagueApp.Classes;
using MyLeagueApp.ViewModels;
using MySql.Data.MySqlClient;
using System.Data;

namespace MyLeagueApp;

public partial class EditMatchPage : ContentPage
{
    MySqlConnection sqlConn2 = new MySqlConnection();
    MySqlCommand sqlCmd = new MySqlCommand();
    DataTable sqlDt = new DataTable();
    String sqlQuery;
    MySqlDataAdapter DtA = new MySqlDataAdapter();
    MySqlDataReader sqlRd;
    MySqlDataReader sqlRdC;
    MySqlDataReader sqlRd2;
    MySqlDataReader sqlRd3;
    MySqlDataReader sqlRd4;
    MySqlDataReader sqlRd7;
    MySqlDataReader sqlRd8;
    MySqlDataReader sqlRdDF;
    MySqlDataReader sqlRdDate;
    DataSet ds = new DataSet();

    String current_league_name;
    int current_match_id;
    string date;
    int hometeam;
    int awayteam;

    String server = "localhost";
    String username = "root";
    String password = "";
    String database = "myleague";

    List<Team> teams;
    public EditMatchPage(string league_name, int match_id)
    {
        InitializeComponent();
        current_league_name = league_name;
        current_match_id = match_id;
        //teams = new List<Team>();
        BindingContext = new EditMatchViewModel(league_name, match_id);
        ////loadTeams();
        ////home_picker.ItemsSource = teams;
        ////away_picker.ItemsSource = teams;
        //loadSelections();
        ////game_date.Date = DateTime.Now;
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

            sqlConn2.ConnectionString = "server=" + server + ";user id=" + username +
                                        ";password=" + password +
                                        ";database=" + database +
                                        ";convert zero datetime=True";

            sqlConn2.Open();

            String sqlc = "SELECT COUNT(*) FROM `teams`; ";

            sqlCmd = new MySqlCommand(sqlc, sqlConn2);
            sqlRdC = sqlCmd.ExecuteReader();

            while (sqlRdC.Read())
            {

            }

            team_count = Int32.Parse(sqlRdC[0].ToString());
            sqlRdC.Close();

            sqlConn2.Close();

            for (int i = 0; i < team_count; i++)
            {

                sqlConn2.Open();

                String sql = "SELECT team_id FROM `teams` WHERE 1 LIMIT " + i + ",1; ";

                sqlCmd = new MySqlCommand(sql, sqlConn2);
                sqlRd = sqlCmd.ExecuteReader();

                while (sqlRd.Read())
                {

                }

                team_id = sqlRd[0].ToString();
                sqlRd.Close();

                String sql2 = "SELECT name FROM `teams` WHERE 1 LIMIT " + i + ",1; ";

                sqlCmd = new MySqlCommand(sql2, sqlConn2);
                sqlRd2 = sqlCmd.ExecuteReader();

                while (sqlRd2.Read())
                {

                }

                team_name = sqlRd2[0].ToString();
                sqlRd2.Close();

                String df_sql = "SELECT city FROM `teams` WHERE 1 LIMIT " + i + ",1; ";

                sqlCmd = new MySqlCommand(df_sql, sqlConn2);
                sqlRdDF = sqlCmd.ExecuteReader();

                while (sqlRdDF.Read())
                {

                }

                team_city = sqlRdDF[0].ToString();

                sqlRdDF.Close();

                String sqlLogo = "SELECT logo FROM `teams` WHERE 1 LIMIT " + i + ",1; ";

                sqlCmd = new MySqlCommand(sqlLogo, sqlConn2);
                sqlRd3 = sqlCmd.ExecuteReader();

                while (sqlRd3.Read())
                {

                }

                team_logo = sqlRd3[0].ToString();

                sqlRd3.Close();

                sqlConn2.Close();

                teams.Add(new Team(Int32.Parse(team_id), team_name, team_city, team_logo));

            }

        }
        catch (Exception ex)
        {
            //DisplayAlert("", ex.Message, "OK");
        }

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
            sqlRd.Close();

            String sql2 = "SELECT away_team FROM `" + current_league_name + "` WHERE `m_id` = " + current_match_id + "; ";

            sqlCmd = new MySqlCommand(sql2, sqlConn2);
            sqlRd2 = sqlCmd.ExecuteReader();

            while (sqlRd2.Read())
            {

            }

            away_team = Int32.Parse(sqlRd2[0].ToString());
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
            homename.Text = "Set " + home + " final score"; ;
            sqlRd7.Close();

            String sql_a_name = "SELECT t.name FROM `" + current_league_name + "` l JOIN teams t ON (l.away_team = t.team_id) WHERE l.`m_id` = " + current_match_id + "; ";

            sqlCmd = new MySqlCommand(sql_a_name, sqlConn2);
            sqlRd8 = sqlCmd.ExecuteReader();

            while (sqlRd8.Read())
            {

            }

            string away = sqlRd8[0].ToString();
            awayname.Text = "Set " + away + " final score";
            sqlRd8.Close();

            sqlConn2.Close();

            //home_picker.SelectedItem = teams[home_team];
            //away_picker.SelectedItem = teams[away_team];
            hometeam = home_team;
            awayteam = away_team;
            homescore.Text = home_score.ToString();
            awayscore.Text = away_score.ToString();


        }
        catch (Exception ex)
        {
            DisplayAlert("", ex.Message, "OK");
        }

    }

    private void ConfirmMatchClicked(object sender, EventArgs e)
    {
        if (
            //home_picker == null || away_picker == null || 
            homescore.Text == null || awayscore.Text == null)
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

                String sql2 = "UPDATE `" + current_league_name + "` SET `home_score` = '" + homescore.Text + "', `away_score` = '" + awayscore.Text + "' WHERE `" + current_league_name + "`.`m_id` = " + current_match_id + ";";

                sqlCmd = new MySqlCommand(sql2, sqlConn2);

                sqlRd2 = sqlCmd.ExecuteReader();

                while (sqlRd2.Read())
                {

                }

                sqlRd2.Close();

                sqlConn2.Close();

                Navigation.PushAsync(new CurrentLeaguePage(current_league_name));

            }
            catch (Exception ex)
            {
                DisplayAlert("", ex.Message, "OK");
            }
        }
    }
}