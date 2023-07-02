using MyLeagueApp.ViewModels;
using MySql.Data.MySqlClient;

namespace MyLeagueApp;

public partial class NewLeaguePage : ContentPage
{
    public NewLeaguePage()
    {
        InitializeComponent();
        BindingContext = new NewLeagueViewModel();
    }

}