using Microsoft.Maui.Controls;
using MyLeagueApp.Classes;
using MyLeagueApp.ViewModels;
using MySql.Data.MySqlClient;
using System.Data;

namespace MyLeagueApp;

public partial class LeaguesPage : ContentPage
{
    public LeaguesPage()
    {
        InitializeComponent();
        BindingContext = new LeaguesViewModel();
    }

    void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
    {
        League item = args.SelectedItem as League;
        string name = item.Name.ToString();
        CurrentLeaguePage page = new CurrentLeaguePage(name);
        Navigation.PushAsync(page);
    }

    private void ViewCell_Tapped(object sender, System.EventArgs e)
    {
        var viewCell = (ViewCell)sender;
        viewCell.View.BackgroundColor = Color.FromArgb("#f0f8ff");
    }

    private void NewLeagueClicked(object sender, EventArgs e)
    {
        NewLeaguePage page = new NewLeaguePage();
        Navigation.PushAsync(page);
    }
}