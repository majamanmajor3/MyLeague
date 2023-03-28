using Microsoft.Maui.Controls;
using MyLeagueApp.Classes;
using MyLeagueApp.ViewModels;
using MySql.Data.MySqlClient;
using System.Data;

namespace MyLeagueApp;

public partial class LeaguesPage : ContentPage
{
    MySqlConnection sqlConn = new MySqlConnection();
    MySqlCommand sqlCmd = new MySqlCommand();
    DataTable sqlDt = new DataTable();
    String sqlQuery;
    MySqlDataAdapter DtA = new MySqlDataAdapter();
    MySqlDataReader sqlRd;
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

    List<League> leagues;
    public LeaguesPage()
    {
        InitializeComponent();
        BindingContext = new LeaguesViewModel();
        //leagues = new List<League>();
        //loadLeagues();
        //cv.ItemsSource = leagues;
    }

    private void loadLeagues()
    {

        try
        {
            string league_count = "0";
            string league_id;
            string league_name;
            string league_date;

            sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                        ";password=" + password +
                                        ";database=" + database +
                                        ";convert zero datetime=True";

            sqlConn.Open();

            String sql = "SELECT COUNT(*) FROM `leagues`; ";

            sqlCmd = new MySqlCommand(sql, sqlConn);
            sqlRd3 = sqlCmd.ExecuteReader();

            while (sqlRd3.Read())
            {

            }

            league_count = sqlRd3[0].ToString();
            sqlRd3.Close();
            sqlConn.Close();

            int cr = Int32.Parse(league_count);

            if (cr == 0)
            {
                label.IsVisible = true;
            }

            for (int i = 0; i < cr; i++)
            {

                sqlConn.Open();

                String sql2 = "SELECT league_id FROM `leagues` WHERE 1 LIMIT " + i + ",1; ";

                sqlCmd = new MySqlCommand(sql, sqlConn);
                sqlRd = sqlCmd.ExecuteReader();

                while (sqlRd.Read())
                {

                }

                league_id = sqlRd[0].ToString();
                sqlRd.Close();

                String sqlName = "SELECT name FROM `leagues` WHERE 1 LIMIT " + i + ",1; ";

                sqlCmd = new MySqlCommand(sqlName, sqlConn);
                sqlRd2 = sqlCmd.ExecuteReader();

                while (sqlRd2.Read())
                {

                }

                league_name = sqlRd2[0].ToString();
                sqlRd2.Close();

                String df_sql = "SELECT created_date FROM `leagues` WHERE 1 LIMIT " + i + ",1; ";

                sqlCmd = new MySqlCommand(df_sql, sqlConn);
                sqlRdDF = sqlCmd.ExecuteReader();

                while (sqlRdDF.Read())
                {

                }

                league_date = sqlRdDF[0].ToString().Substring(0, 10);

                sqlRdDF.Close();

                sqlConn.Close();

                leagues.Add(new League(Int32.Parse(league_id), league_name, league_date));

            }

        }
        catch (Exception ex)
        {
            DisplayAlert("", ex.Message, "OK");
        }

    }

    void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
    {
        League item = args.SelectedItem as League;
        string name = item.Name.ToString();
        //DisplayAlert("", name, "OK");
        CurrentLeaguePage page = new CurrentLeaguePage(name);
        Navigation.PushAsync(page);
    }

    private void ViewCell_Tapped(object sender, System.EventArgs e)
    {
        var viewCell = (ViewCell)sender;
        viewCell.View.BackgroundColor = Color.FromArgb("#f0f8ff");
        //HomePage page = new HomePage();
        //Navigation.PushAsync(page);
    }

    private void NewLeagueClicked(object sender, EventArgs e)
    {
        NewLeaguePage page = new NewLeaguePage();
        Navigation.PushAsync(page);
    }
}