using MyLeagueApp.Classes;
using MyLeagueApp.Classes.Samples;
using MyLeagueApp.ViewModels;

namespace MyLeagueApp.Pages;

public partial class SampledTeamOverviewPage : ContentPage
{
    public int current_team_id;
    public SampledTeamOverviewPage(int team_id)
    {
        InitializeComponent();
        current_team_id = team_id;
        BindingContext = new SampledTeamOverviewViewModel(team_id);
    }

    private void NewPlayerClicked(object sender, EventArgs e)
    {
        NewPlayerPage page = new NewPlayerPage(current_team_id);
        Navigation.PushAsync(page);
    }

    private void EditTeamClicked(object sender, EventArgs e)
    {
        EditTeamPage page = new EditTeamPage(current_team_id);
        Navigation.PushAsync(page);
    }

    private void ViewCell_Tapped(object sender, System.EventArgs e)
    {
        var viewCell = (ViewCell)sender;
        viewCell.View.BackgroundColor = Color.FromArgb("#f0f8ff");
    }

    void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
    {
        PlayerSample item = args.SelectedItem as PlayerSample;
        int player_id = item.Id;
        //DisplayAlert("", name, "OK");
        SampledPlayerOverviewPage page = new SampledPlayerOverviewPage(player_id);
        Navigation.PushAsync(page);
    }
}