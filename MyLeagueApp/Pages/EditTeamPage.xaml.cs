using MyLeagueApp.ViewModels;

namespace MyLeagueApp.Pages;

public partial class EditTeamPage : ContentPage
{
    public int current_team_id;
    public EditTeamPage(int team_id)
    {
        InitializeComponent();
        current_team_id = team_id;
        BindingContext = new EditTeamViewModel(team_id);
    }
}