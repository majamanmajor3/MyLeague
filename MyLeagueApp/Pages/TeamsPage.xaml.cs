using System.Data;
using MyLeagueApp.Classes;
using MyLeagueApp.Converters;
using MyLeagueApp.Pages;
using MyLeagueApp.ViewModels;
using MySql.Data.MySqlClient;

namespace MyLeagueApp;

public partial class TeamsPage : ContentPage
{
    public TeamsPage()
	{
        InitializeComponent();
        BindingContext = new TeamsViewModel();
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
        Team item = args.SelectedItem as Team;
        int id = item.Id;
        TeamOverviewPage page = new TeamOverviewPage(item.Id);
        Navigation.PushAsync(page);
    }

}