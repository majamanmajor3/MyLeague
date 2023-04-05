using MyLeagueApp.ViewModels;

namespace MyLeagueApp.Pages;

public partial class TeamOverviewPage : ContentPage
{
    public int current_team_id;
	public TeamOverviewPage(int team_id)
	{
        InitializeComponent();
        current_team_id = team_id;
        BindingContext = new TeamOverviewViewModel(team_id);
    }

    private void NewPlayerClicked(object sender, EventArgs e)
    {
        NewPlayerPage page = new NewPlayerPage(current_team_id);
        Navigation.PushAsync(page);
    }
}