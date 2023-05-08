using MyLeagueApp.Classes;
using MyLeagueApp.Classes.Samples;
using MyLeagueApp.ViewModels;

namespace MyLeagueApp.Pages;

public partial class SampledTeamsPage : ContentPage
{
	public SampledTeamsPage()
	{
		InitializeComponent();
        BindingContext = new SampledTeamsViewModel();
    }

    private void ViewCell_Tapped(object sender, System.EventArgs e)
    {
        var viewCell = (ViewCell)sender;
        viewCell.View.BackgroundColor = Color.FromArgb("#f0f8ff");
    }

    private void NewTeamClicked(object sender, EventArgs e)
    {
        NewTeamPage page = new NewTeamPage();
        Navigation.PushAsync(page);
    }

    void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
    {
        TeamSample item = args.SelectedItem as TeamSample;
        int id = item.Id;
        //SampledTeamOverviewPage page = new SampledTeamOverviewPage(item.Id);
        //Navigation.PushAsync(page);
    }
}