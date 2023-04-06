using MyLeagueApp.Classes;
using MyLeagueApp.ViewModels;

namespace MyLeagueApp.Pages;

public partial class MatchOverviewPage : ContentPage
{
    string current_league_name;
    int current_match_id;
	public MatchOverviewPage(int match_id, string current_league)
	{
		InitializeComponent();
        current_league_name = current_league;
        current_match_id = match_id;
		BindingContext = new MatchOverviewViewModel(current_league, match_id);
	}

    private void EditScoreButton(object sender, EventArgs e)
    {
        EditMatchPage page = new EditMatchPage(current_league_name, current_match_id);
        Navigation.PushAsync(page);
    }

}