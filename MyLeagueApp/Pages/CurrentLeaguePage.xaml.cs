using MyLeagueApp.Classes;
using MyLeagueApp.Pages;
using MyLeagueApp.ViewModels;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.IO;
using System.Data;

namespace MyLeagueApp;

public partial class CurrentLeaguePage : ContentPage
{

    String current_league_name;
    public CurrentLeaguePage(string league_name)
	{
		InitializeComponent();
        current_league_name = league_name;
        BindingContext = new CurrentLeagueViewModel(league_name);
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

    private void LeadersClicked(object sender, EventArgs e)
    {
        LeagueLeadersPage page = new LeagueLeadersPage(current_league_name);
        Navigation.PushAsync(page);
    }

    void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
    {
        Match item = args.SelectedItem as Match;
        MatchOverviewPage page = new MatchOverviewPage(item.Id, current_league_name);
        Navigation.PushAsync(page);
    }
}