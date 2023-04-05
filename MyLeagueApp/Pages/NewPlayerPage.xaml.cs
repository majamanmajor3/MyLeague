using MyLeagueApp.ViewModels;

namespace MyLeagueApp.Pages;

public partial class NewPlayerPage : ContentPage
{
    int current_team_id;
    public NewPlayerPage(int team_id)
	{
        InitializeComponent();
        current_team_id = team_id;
        BindingContext = new NewPlayerViewModel(current_team_id);
    }

}