using MyLeagueApp.Classes;
using MyLeagueApp.ViewModels;
using MySql.Data.MySqlClient;
using System.Data;

namespace MyLeagueApp;

public partial class NewMatchPage : ContentPage
{
    String current_league_name;
    public NewMatchPage(string league_name)
    {
        InitializeComponent();
        current_league_name = league_name;
        BindingContext = new NewMatchViewModel(league_name);
    }

}