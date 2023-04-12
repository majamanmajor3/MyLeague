using MyLeagueApp.ViewModels;

namespace MyLeagueApp.Pages;

public partial class LeagueLeadersPage : ContentPage
{
	public LeagueLeadersPage(string league_name)
	{
        InitializeComponent();
        BindingContext = new LeagueLeadersViewModel(league_name);
    }
}