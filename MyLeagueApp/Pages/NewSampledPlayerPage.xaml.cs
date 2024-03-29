using Microsoft.Maui.Controls;
using MyLeagueApp.Classes;
using MyLeagueApp.Classes.Samples;
using MyLeagueApp.ViewModels;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Utilities.IO;
using System.Data.SqlTypes;
using System.Net;

namespace MyLeagueApp.Pages;

public partial class NewSampledPlayerPage : ContentPage
{
    public string current_searched_player;

    MySqlConnection sqlConn = new MySqlConnection();
    MySqlCommand sqlCmd = new MySqlCommand();
    String sqlQuery;
    MySqlDataAdapter DtA = new MySqlDataAdapter();
    MySqlDataReader sqlRd;
    MySqlDataReader sqlRdC;
    MySqlDataReader sqlRd2;

    String server = "localhost";
    String username = "root";
    String password = "";
    String database = "myleague";

    public NewSampledPlayerPage(string player_name)
    {
        InitializeComponent();
        current_searched_player = player_name;
        BindingContext = new NewSampledPlayerViewModel(current_searched_player);
    }

    async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
    {
        PlayerSample item = args.SelectedItem as PlayerSample;
        int id = item.Id;

        WebClient client = new WebClient();
        String teams = client.DownloadString("https://free-nba.p.rapidapi.com/teams/" + item.Team + "?rapidapi-key=ffe8de403amshdbfef1479d9fdafp10e8a0jsna7708bdc0688");
        dynamic data = JObject.Parse(teams);
        string team_name = (string)data["full_name"];

        bool answer = await Shell.Current.DisplayAlert("Are you sure you want to sample this player?",
                                                          "Full Name: " + item.FullName + "\n" +
                                                          "Team: " + team_name + "\n" +
                                                          "Position Name: " + item.PositionName + "\n" +
                                                          "Height: " + item.HeightFeet + "'" + item.HeightInches + "\n" +
                                                          "Weight: " + item.Weight + "\n"
        , "Yes", "No");

        if (answer)
        {
            try
            {

                int player_id;

                sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                            ";password=" + password +
                                            ";database=" + database +
                                            ";convert zero datetime=True";

                sqlConn.Open();

                String sql = "SELECT COUNT(*)+1 FROM `sampled_players` ORDER BY player_id DESC; ";

                sqlCmd = new MySqlCommand(sql, sqlConn);
                sqlRd = sqlCmd.ExecuteReader();

                while (sqlRd.Read())
                {

                }

                player_id = Int32.Parse(sqlRd[0].ToString());
                sqlRd.Close();

                String sql2 = "INSERT INTO `sampled_players` (`first_name`, `last_name`, `team`, `position`, `height_feet`, `height_inches`, `weight_pounds`, `api_player_id`) " +
                    "VALUES ('" + item.FirstName + "', '" + item.LastName + "', '" + item.Team + "', '" + item.PositionLetter + "', '" + item.HeightFeet + "', '" + item.HeightInches + "', '" + item.Weight + "', '" + item.Id + "');";

                sqlCmd = new MySqlCommand(sql2, sqlConn);

                sqlRd2 = sqlCmd.ExecuteReader();

                while (sqlRd2.Read())
                {

                }

                sqlRd2.Close();

                String sql_id = "SELECT `team_id` FROM `sampled_teams` WHERE `api_id`=" + item.Team + "; ";

                sqlCmd = new MySqlCommand(sql_id, sqlConn);
                sqlRd = sqlCmd.ExecuteReader();

                while (sqlRd.Read())
                {

                }

                int team_db_id = Int32.Parse(sqlRd[0].ToString());
                sqlRd.Close();

                sqlConn.Close();

                Application.Current.MainPage.DisplayAlert("", "Your new sample player has been created!", "OK");

                SampledTeamOverviewPage page = new SampledTeamOverviewPage(team_db_id);
                await Application.Current.MainPage.Navigation.PushAsync(page);

                Task.CompletedTask.Dispose();

            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                sqlConn.Close();
            }
        }

    }

    private void ViewCell_Tapped(object sender, System.EventArgs e)
    {
        var viewCell = (ViewCell)sender;
        viewCell.View.BackgroundColor = Color.FromArgb("#f0f8ff");
    }
}