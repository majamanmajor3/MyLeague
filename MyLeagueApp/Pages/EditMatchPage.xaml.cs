using MyLeagueApp.Classes;
using MyLeagueApp.ViewModels;
using MySql.Data.MySqlClient;
using System.Data;

namespace MyLeagueApp;

public partial class EditMatchPage : ContentPage
{
    String current_league_name;
    int current_match_id;
    public EditMatchPage(string league_name, int match_id)
    {
        InitializeComponent();
        current_league_name = league_name;
        current_match_id = match_id;
        BindingContext = new EditMatchViewModel(league_name, match_id);
    }
}