using System.Data;
using MyLeagueApp.Classes;
using MyLeagueApp.Converters;
using MyLeagueApp.ViewModels;
using MySql.Data.MySqlClient;

namespace MyLeagueApp;

public partial class TeamsPage : ContentPage
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

    ViewCell lastCell;

    String server = "localhost";
    String username = "root";
    String password = "";
    String database = "myleague";

    List<Team> teams;
    public TeamsPage()
	{
        InitializeComponent();
        BindingContext = new TeamsViewModel();
        //teams = new List<Team>();
        //loadTeams();
        //cv.ItemsSource = teams;
        //ListView listView = new ListView();
        //listView.SetBinding(ItemsView.ItemsSourceProperty, "teams");
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

    void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
    {
        Team item = args.SelectedItem as Team;
    }

    private void ViewCell_Tapped(object sender, System.EventArgs e)
    {
        var viewCell = (ViewCell)sender;
        viewCell.View.BackgroundColor = Color.FromArgb("#f0f8ff");
    }

    private void NewTeamClicked(object sender, EventArgs e)
    {
        NewTeamPage page = new NewTeamPage();
        Navigation.PushAsync(page);
    }

}