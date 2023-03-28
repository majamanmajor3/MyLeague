using System.Data;
using System.Data.SqlTypes;
using System.Net;
using Microsoft.Maui.Controls;
using Newtonsoft.Json.Linq;
using MySql.Data.MySqlClient;
using MyLeagueApp.ViewModels;

namespace MyLeagueApp;

public partial class MainPage : ContentPage
{

    public MainPage()
	{
        InitializeComponent();
        BindingContext = new MainViewModel();
        //WebClient client = new WebClient();
        //String player = client.DownloadString("https://free-nba.p.rapidapi.com/players/237?rapidapi-key=ffe8de403amshdbfef1479d9fdafp10e8a0jsna7708bdc0688");
        //dynamic data = JObject.Parse(player);
        ////textBox1.Text = player;
        //team.Text = data.team.full_name;
        //String stats = client.DownloadString("https://free-nba.p.rapidapi.com/stats?seasons[]=2021&player_ids[]=237&rapidapi-key=ffe8de403amshdbfef1479d9fdafp10e8a0jsna7708bdc0688");
        //dynamic data2 = JObject.Parse(stats);
        //float ppg = 0;
        //int cr = 0;
        //foreach (var member in data2["data"])
        //{
        //    ppg += (float)member["pts"];
        //    cr += 1;
        //}
        ////points.Text = (ppg / cr).ToString().Remove(4, 1) + " PPG";

    }

    private void TeamsClicked(object sender, EventArgs e)
    {
        TeamsPage page = new TeamsPage();
        Navigation.PushAsync(page);
    }

    private void LeaguesClicked(object sender, EventArgs e)
    {
        LeaguesPage page = new LeaguesPage();
        Navigation.PushAsync(page);
    }

    //private void loadData()
    //{
    //    try
    //    {

    //        string car_count;
    //        string daily_fee;

    //        sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
    //                                    ";password=" + password +
    //                                    ";database=" + database +
    //                                    ";convert zero datetime=True";

    //        sqlConn.Open();

    //        String sql = "SELECT COUNT(*) FROM `players` WHERE 1; ";

    //        sqlCmd = new MySqlCommand(sql, sqlConn);
    //        sqlRd = sqlCmd.ExecuteReader();

    //        while (sqlRd.Read())
    //        {

    //        }

    //        car_count = sqlRd[0].ToString();
    //        sqlRd.Close();

    //        String df_sql = "SELECT `last_name` FROM `players` WHERE `first_name` = 'Joel'; ";

    //        sqlCmd = new MySqlCommand(df_sql, sqlConn);
    //        sqlRdDF = sqlCmd.ExecuteReader();

    //        while (sqlRdDF.Read())
    //        {

    //        }

    //        daily_fee = sqlRdDF[0].ToString();

    //        sqlRdDF.Close();

    //        sqlConn.Close();

    //        //team.Text = daily_fee;
    //        //points.Text = car_count;

    //    }
    //    catch (Exception ex)
    //    {
    //        DisplayAlert("", ex.Message, "OK");
    //    }

    //}

}

