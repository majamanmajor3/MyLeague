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

}

